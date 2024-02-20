using backEnd_EM.Dtos.Analyse;
using backEnd_EM.Dtos.Analytics;
using backEnd_EM.Dtos.Athletes;
using backEnd_EM.Models;
using backEnd_EM.Properties.Models;

namespace backEnd_EM.Mapper
{

    public static class AnalyticsMapper
    {
        public static Analytics CreateRequest(this CreateRequestAnalyticsDto analyticsModel)
        {
            return new Analytics
            {
                AmountOfTimesLogin = analyticsModel.AmountOfTimesLogin,
                Rank = analyticsModel.Rank,
                AthletesId = analyticsModel.AthleteId
            };
        }
        public static AnalyticsDto ToAnalyticsDto(this Analytics analyticsModel)
        {
            return new AnalyticsDto
            {
                Rank = analyticsModel.Rank,
                AmountOfTimesLogin = analyticsModel.AmountOfTimesLogin
            };
        }
    }
}