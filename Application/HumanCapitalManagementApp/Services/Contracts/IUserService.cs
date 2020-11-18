using DTO;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IUserService
    {
        Task<UserDataDTOout> LoginAsync(UserLoginDTOin data);
        Task<UserDataDTOout> RegisterAsync(UserRegisterDTOin data);
        Task<UserProfileInfoDTOout> GetProfileDataAsync(string username);
        Task<UserProfileInfoDTOin> GetMyProfileInfoForEditAsync();
        Task<bool> UpdateUserDataAsync(UserProfileInfoDTOin dto);
        bool IsLoggedIn { get; }
        UserDataDTOout User { get; }
        void Logout();
    }
}