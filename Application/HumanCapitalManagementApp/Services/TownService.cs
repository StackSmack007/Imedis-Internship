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
    public class TownService : BaseService, ITownService
    {
        public TownService(IMapper mapper) : base(mapper)
        { }

        public async Task<ICollection<TownOptionDTOout>> GetAllTownsAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                var towns = await session.Query<Town>().ProjectTo<TownOptionDTOout>(mapper.ConfigurationProvider).ToListAsync();
                return towns;
            }
        }
    }
}
