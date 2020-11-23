using DTO;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IJobService
    {
        Task<ICollection<Currency>> GetAllCurrenciesAsync();
        Task<ICollection<JobOptionDTOout>> GetAllJobOptionsAsync();
    }
}