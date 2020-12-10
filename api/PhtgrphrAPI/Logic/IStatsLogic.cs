using PhtgrphrAPI.Models;
using PhtgrphrAPI.Responses;

namespace PhtgrphrAPI.Logic
{
    public interface IStatsLogic
    {
        PhtgrphrResponse<StatsResponse> GetStats();
    }
}