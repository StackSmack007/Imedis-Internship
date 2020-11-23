using DTO;
using AutoMapper;
using System.Linq;
using NHibernate.Linq;
using Services.Contracts;
using Infrastructure.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Services
{
    public class CompaniesService : BaseService, ICompaniesService
    {
        public CompaniesService(IMapper mapper) : base(mapper)
        { }


        public async Task<ICollection<CompanyListItemDTOout>> GetAllCompaniesAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var dtos = (await session.Query<Company>()
                    .ToListAsync())
                    .Select(x => mapper.Map<CompanyListItemDTOout>(x))
                    .ToList();

                return dtos;
            }
        }
        public async Task<Company> DeleteAsync(string id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var companyFd = await session.Query<Company>().FirstOrDefaultAsync(x => x.Id.ToLower() == id.ToLower());
                if (companyFd is null)
                {
                    throw new InvalidOperationException($"Company not found!");
                }
                if (companyFd.CompanyAddresses.Any())
                {
                    throw new InvalidOperationException($"{companyFd.Name} can't be deleted because it has {companyFd.CompanyAddresses.Count()} branches registered!");
                }
                using (var transaction = session.BeginTransaction())
                {
                    await session.DeleteAsync(companyFd);
                    await transaction.CommitAsync();
                    return companyFd;
                }
            }
        }

        public async Task<Company> CreateAsync(CompanyDTOin dto)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    bool nameTaken = await session.Query<Company>().AnyAsync(x => x.Name.ToLower() == dto.Name.ToLower());
                    if (nameTaken)
                    {
                        throw new InvalidOperationException($"Name {dto.Name} is already used, choose another!");
                    }
                    bool webPageTaken = await session.Query<Company>().AnyAsync(x => x.WebPage.ToLower() == dto.WebPage.ToLower());
                    if (nameTaken)
                    {
                        throw new InvalidOperationException($"Web Address is already used, choose another!");
                    }
                    Town town = await session.Query<Town>().FirstOrDefaultAsync(x => x.Id == dto.TownId);
                    Company newCompany = new Company
                    {
                        Name = dto.Name,
                        WebPage = dto.WebPage
                    };
                    CompanyAddress newAddress = new CompanyAddress
                    {
                        Town = town,
                        Address = dto.Address,
                        Phone = dto.Phone,
                        Email = dto.Email,
                        Company = newCompany,
                        IsPrimary = true
                    };
                    await session.SaveAsync(newCompany);
                    await session.SaveAsync(newAddress);
                    await transaction.CommitAsync();
                    return newCompany;
                }
            }
        }

        public async Task<CompanyAddress> DeleteAddressAsync(int id)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var addressFd = await session.Query<CompanyAddress>().FirstOrDefaultAsync(x => x.Id == id);
                    if (addressFd is null)
                    {
                        throw new InvalidOperationException("Address was not found!");
                    }
                    if (addressFd.UserJobs.Count() > 0)
                    {
                        throw new InvalidOperationException($"Address in town {addressFd.Town.Name} of {addressFd.Company.Name} company, contained workers and can not be deleted!");
                    }
                    await session.DeleteAsync(addressFd);
                    await transaction.CommitAsync();
                    return addressFd;
                }
            }
        }

        public async Task<ICollection<CompanyOfficeOptionDTOout>> GetAllCompanyOfficeOptionsAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return (await session.Query<Company>().ToListAsync()).Select(x => mapper.Map<CompanyOfficeOptionDTOout>(x)).ToArray();
            }
        }
    }
}
