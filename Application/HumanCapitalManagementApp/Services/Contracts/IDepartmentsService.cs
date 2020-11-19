using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IDepartmentsService
    {
        Task<ICollection<DepartmentDTOout>> GetDepartmentsAsync();
    }
}
