using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEmployeesService
    {
        Task<ICollection<EmployeeMiniDTOout>> GetAllMiniAsync(bool asigned);
        Task<ICollection<EmployeeMiniDTOout>> GetAllMiniByCriteriasAsync(EmployeeSearchQuery dto);
    }
}
