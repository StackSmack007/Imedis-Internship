using DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IPosititionsService
    {
        Task<ICollection<PositionMiniDTOout>> GetPositionsAsync(bool showActive);
    }
}
