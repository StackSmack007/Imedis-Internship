using AutoMapper;
using DTO;
using Infrastructure.Data;
using NHibernate.Linq;
using Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class PositionsService : BaseService, IPosititionsService
    {
        public PositionsService(IMapper mapper) : base(mapper)
        { }

        public async Task<ICollection<PositionMiniDTOout>> GetPositionsAsync(bool showActive)
        {
            using (var session = NhibernateHelper.OpenSession())
            {

                ICollection<PositionMiniDTOout> result = null;
                if (showActive)
                {
                    result = (await session.Query<UserJob>().Where((x) => x.EndDate == null).Fetch(x => x.Currency).ToListAsync())
                        .Select(x => mapper.Map<PositionMiniDTOout>(x)).ToArray();
                }
                else
                {
                    result = (await session.Query<UserJob>().Where((x) => x.EndDate != null).Fetch(x => x.Currency).ToListAsync())
                        .Select(x => mapper.Map<PositionMiniDTOout>(x)).ToArray();
                }

                return result;
            }



            throw new System.NotImplementedException();
        }
    }
}
