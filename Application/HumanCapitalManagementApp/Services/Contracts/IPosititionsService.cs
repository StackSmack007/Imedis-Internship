using DTO;
using Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IPosititionsService
    {
        Task<ICollection<PositionMiniDTOout>> GetAllMiniAsync(bool showActive);
        Task<UserJob> CreateAsync(PositionDTOin dto);
        Task<UserJob> DeleteAsync(int id);
        Task<UserJob> CancelAsync(int id);
        Task<PositionEditDTOin> GetPositionForEditAsync(int id);
        Task<UserJob> EditAsync(PositionEditDTOin dto);
    }
}
