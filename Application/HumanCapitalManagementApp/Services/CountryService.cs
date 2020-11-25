using DTO;
using AutoMapper;
using Infrastructure.Data;
using Services.Contracts;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Services
{
    public class CountryService : BaseService, ICountryService
    {
        public CountryService(IMapper mapper) : base(mapper)
        {        }

        public async Task<ICollection<CountryDTOout>> GetAllCountriesAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var result = (await session.Query<Country>()
                            .ToListAsync())
                            .Select(x => mapper.Map<CountryDTOout>(x))
                            .ToArray();
                return result;

            }
        }
        public async Task<Country> DeleteAsync(string countryId)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                Country countryFd = await session.Query<Country>().FirstOrDefaultAsync(x => x.Id == countryId);
                if (countryFd.Towns.Any())
                {
                    return null;
                }

                using (var transaction = session.BeginTransaction())
                {
                    await session.DeleteAsync(countryFd);
                    await transaction.CommitAsync();
                }
                return countryFd;

            }
        }

        public async Task CreateCountryAsync(CountryDTOin dto)
        {
            var newCountry = mapper.Map<Country>(dto);
            using (var session = NhibernateHelper.OpenSession())
            {
                if (await session.Query<Country>().AnyAsync(x => x.Id.ToLower() == dto.Id.ToLower()))
                {
                    throw new InvalidOperationException("Id is already used!");
                }

                if (await session.Query<Country>().AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower()))
                {
                    throw new InvalidOperationException("Name is already used");
                }

                using (var transaction = session.BeginTransaction())
                {
                    await session.SaveAsync(newCountry);
                    await transaction.CommitAsync();
                }
            }
        }
                public async Task<CountryEDITout> GetCountryForEditAsync(string id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var result = mapper.Map<CountryEDITout>(await session.Query<Country>().FirstOrDefaultAsync(x => x.Id.ToLower() == id.ToLower()));
                return result;
            }
        }
        public async Task<ICollection<CountryOptionDTOout>> GetAllCountryOptionsAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                IList<CountryOptionDTOout> result = await session.Query<Country>().ProjectTo<CountryOptionDTOout>(mapper.ConfigurationProvider).ToListAsync();
                return result;
            }
        }
        public async Task<Country> EditCountryAsync(CountryEDITout dto)
        {
           //Todo validation checks
            using (var session = NhibernateHelper.OpenSession())
            {
                var countryFd = await session.Query<Country>().FirstOrDefaultAsync(x => x.Id == dto.Id);

                using (var transaction = session.BeginTransaction())
                {
                    countryFd.Name = dto.Name;
                    foreach (var removedTown in dto.Towns.Where(x => x.Deleted))
                    {
                        var townFd = await session.Query<Town>().FirstOrDefaultAsync(x => x.Id == removedTown.Id);
                        countryFd.Towns.Remove(townFd);
                        await session.DeleteAsync(townFd);
                    }
                    dto.Towns = dto.Towns.Where(x => !x.Deleted).ToList();

                    foreach (var relocatedTown in dto.Towns.Where(x => countryFd.Towns.Any(ct => ct.Id == x.Id && ct.Country.Id != x.CountryId)))
                    {
                        var townFd = await session.Query<Town>().FirstOrDefaultAsync(x => x.Id == relocatedTown.Id);
                        townFd.Country = await session.Query<Country>().FirstOrDefaultAsync(x => x.Id == relocatedTown.CountryId);
                        await session.SaveOrUpdateAsync(townFd);
                    }
                    await session.SaveOrUpdateAsync(countryFd);
                    await transaction.CommitAsync();
                }

                return countryFd;
            }
        }
    }
}