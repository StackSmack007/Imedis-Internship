using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IJobService
    {
        Task<ICollection<CurrencyOptionDTOout>> GetAllCurrencyOptionsAsync();
        Task<ICollection<JobOptionDTOout>> GetAllJobOptionsAsync();
    }
}