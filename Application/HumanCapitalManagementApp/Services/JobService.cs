using AutoMapper;
using AutoMapper.QueryableExtensions;
using DTO;
using Infrastructure.Data;
using NHibernate.Linq;
using Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        public async Task<ICollection<Currency>> GetAllCurrenciesAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return await session.Query<Currency>().ToListAsync();
            }
        }
    }
}
