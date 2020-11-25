using DTO;
using AutoMapper;
using Helpers;
using Infrastructure.Data;
using Microsoft.Extensions.Caching.Memory;
using NHibernate.Linq;
using Services.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class StatisticsService : BaseService, IStatisticsService
    {
        private readonly IMemoryCache cache;

        public StatisticsService(IMemoryCache cache, IMapper mapper) : base(mapper)
        {
            this.cache = cache;
        }

        public async Task<AppStatisticsDTOout> GetStatisticsAsync()
        {
            if (cache.TryGetValue(GlobalConstants.StatisticsCasheName, out AppStatisticsDTOout dataFd))
            {
                return dataFd;
            }
            using (var session = NhibernateHelper.OpenSession())
            {
                AppStatisticsDTOout data = new AppStatisticsDTOout
                {
                    Users = await session.Query<User>().Select(x => x.Username).ToListAsync(),
                    Companies = await session.Query<Company>().Select(x => x.Name).ToListAsync(),
                    PositionsActive = await session.Query<UserJob>().CountAsync(x => x.EndDate == null),
                    PositionsClosed = await session.Query<UserJob>().CountAsync(x => x.EndDate != null),
                };

                cache.Set(GlobalConstants.StatisticsCasheName, data);
                return data;
            }
        }

        public void ClearStatistics() =>
            cache.Remove(GlobalConstants.StatisticsCasheName);
    }
}
