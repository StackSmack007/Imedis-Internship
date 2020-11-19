using DTO;
using AutoMapper;
using NHibernate.Linq;
using Services.Contracts;
using Infrastructure.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class DepartmentsService : BaseService, IDepartmentsService
    {
        public DepartmentsService(IMapper mapper) : base(mapper)
        { }
        public async Task<ICollection<DepartmentDTOout>> GetDepartmentsAsync()
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                return (await session.Query<Department>().ToListAsync()).Select(x => mapper.Map<DepartmentDTOout>(x)).ToArray();
            }
        }
    }
}
