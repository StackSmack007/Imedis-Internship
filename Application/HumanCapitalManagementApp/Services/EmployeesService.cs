using AutoMapper;
using DTO;
using Infrastructure.Data;
using NHibernate.Linq;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeesService : BaseService, IEmployeesService
    {
        public EmployeesService(IMapper mapper) : base(mapper)
        { }

        public async Task<ICollection<EmployeeMiniDTOout>> GetEmployeesMiniAsync(bool assigned)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                ICollection<EmployeeMiniDTOout> data = null;
                if (assigned)
                {
                    data = (await session.Query<User>().Where(x => x.UserJobs.Count(uj => uj.EndDate == null) > 0).ToListAsync()).Select(x => mapper.Map<EmployeeMiniDTOout>(x)).ToArray();
                }
                else
                {
                    data = (await session.Query<User>().Where(x => x.UserJobs.Count(uj => uj.EndDate == null) == 0).ToListAsync()).Select(x => mapper.Map<EmployeeMiniDTOout>(x)).ToArray();
                }
                return data;
            }
        }

        public async Task<ICollection<EmployeeMiniDTOout>> GetSearchedEmployeesAsync(EmployeeSearchQuery dto)
        {
            var predicats = new Dictionary<string, Func<IQueryable<User>, IQueryable<User>>>()
            {
                ["username"] = (users) => users.Where(x => x.Username.ToLower().Contains(dto.Phrase)),
                ["with manager"] = (users) => users.Where(x => x.UserJobs.Any(j => j.Manager.Username.ToLower().Contains(dto.Phrase))),
                ["company"] = (users) => users.Where(x => x.UserJobs.Any(j => j.CompanyAddress.Company.Name.ToLower().Contains(dto.Phrase))),
            };

            using (var session = NhibernateHelper.OpenSession())
            {
                var data = new HashSet<EmployeeMiniDTOout>();
                foreach (string option in dto.Options.Select(x => x.ToLower()))
                {

                        var newResults = new HashSet<EmployeeMiniDTOout>(
                             (await predicats[option](session.Query<User>())
                             .ToListAsync())
                            .Select(x => mapper.Map<EmployeeMiniDTOout>(x))
                            );
                        data.UnionWith(newResults);
                }
                return data;
            }
        }
    }
}
