using backEnd_EM.Properties.Models;

namespace backEnd_EM.Interfaces
{
    public interface IProgressTrackerRepository
    {

        Task<List<ProgressTracker>> GetAllProgress();

        //done
        Task<List<ProgressTracker>> GetProgressByAthleteId(int id);

        //done
        Task<ProgressTracker?> UpdateProgressTrackerByDayAndId(DateOnly day, int id, string workOut, UpdateProgressTrackerDto updateModel);

        //done
        Task<ProgressTracker?> DeleteProgressTrackerByDayandId(DateOnly day, int id, string workOut);

        //done
        Task<ProgressTracker> CreateProgressTracker(ProgressTracker progressTrackerModel);
    }
}