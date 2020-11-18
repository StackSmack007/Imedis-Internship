using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ITownService
    {
        Task<ICollection<TownOptionDTOout>> GetAllTownsAsync();
    }
}