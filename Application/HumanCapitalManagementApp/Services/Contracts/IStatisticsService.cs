using DTO;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IStatisticsService
    {
        void ClearStatistics();
        Task<AppStatisticsDTOout> GetStatisticsAsync();
    }
}
