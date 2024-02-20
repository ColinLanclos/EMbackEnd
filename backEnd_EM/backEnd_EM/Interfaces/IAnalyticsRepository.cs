using backEnd_EM.Dtos.Analytics;
using backEnd_EM.Models;


namespace backEnd_EM.Interfaces
{
    public interface IAnalyticsRepository
    {
        Task<List<Analytics>> AllAnalyticsSorted();

        Task<Analytics?> GetAnalyticsByAthleteId(int id);

        Task<Analytics?> CreateAnalytics(Analytics analytic);

        Task<Analytics?> UpdateRank(UpdateAnalyticsDto analyticsModle, int AthleteId);

        Task<int> ResetAll();

        Task<Analytics?> IncreamentWhenLogin(int AthleteId);
    }
}