
using backEnd_EM.Dtos.Athletes;
using backEnd_EM.Migrations;
using backEnd_EM.Properties.Models;

namespace backEnd_EM.Mapper
{
    public static class ProgressMapper
    {
        public static ProgressTracker CreateProgressTrackerFromDto(this CreateProgressDto progressDto, int athleteId)
        {
            return new ProgressTracker
            {
                Date = DateOnly.Parse(progressDto.Date),
                WorkOut = progressDto.WorkOut,
                Value_Dor_Date = progressDto.Value_Dor_Date,
                AthletesId = athleteId
            };
        }
    }
}
