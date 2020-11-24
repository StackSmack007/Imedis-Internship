using DTO;
using AutoMapper;
using NHibernate.Linq;
using Services.Contracts;
using Infrastructure.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;

namespace Services
{
    public class JobService : BaseService, IJobService
    {
        public JobService(IMapper mapper) : base(mapper)
        { }

        public async Task<ICollection<JobOptionDTOout>> GetAllJobOptionsAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return await session.Query<Job>().ProjectTo<JobOptionDTOout>(mapper.ConfigurationProvider).ToListAsync();
            }
        }

        public async Task<ICollection<CurrencyOptionDTOout>> GetAllCurrencyOptionsAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return await session.Query<Currency>().ProjectTo<CurrencyOptionDTOout>(mapper.ConfigurationProvider).ToListAsync();
            }
        }
    }
}
