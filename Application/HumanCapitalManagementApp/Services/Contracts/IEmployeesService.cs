using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IEmployeesService
    {
        Task<ICollection<EmployeeMiniDTOout>> GetEmployeesMiniAsync(bool asigned);
        Task<ICollection<EmployeeMiniDTOout>> GetSearchedEmployeesAsync(EmployeeSearchQuery dto);
    }
}
