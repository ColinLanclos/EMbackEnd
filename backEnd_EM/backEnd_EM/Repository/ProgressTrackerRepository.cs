using backEnd_EM.Models;
using backEnd_EM.Properties.Models;
using backEnd_EM.Interfaces;
using Microsoft.EntityFrameworkCore;
using backEnd_EM.Mapper;
using backEnd_EM.Dtos.Athletes;

namespace backEnd_EM.Repository
{
    public class ProgressTrackerRepository : IProgressTrackerRepository
    {
        private readonly AppDBContext _context;

        public ProgressTrackerRepository(AppDBContext context)
        {
            _context = context;
        }


        public async Task<ProgressTracker> CreateProgressTracker(ProgressTracker progressTracker)
        {
            await _context.ProgressTrackers.AddAsync(progressTracker);
            await _context.SaveChangesAsync();
            return progressTracker;
        }

        public async Task<ProgressTracker?> DeleteProgressTrackerByDayandId(DateOnly date, int id, string workOut)
        {
            var progrestrackerModel = await _context.ProgressTrackers.FirstOrDefaultAsync(x => x.AthletesId == id && x.Date == date && x.WorkOut == workOut);

            if (progrestrackerModel == null)
            {
                return null;
            }

            _context.ProgressTrackers.Remove(progrestrackerModel);
            await _context.SaveChangesAsync();
            return progrestrackerModel;
        }

        public async Task<List<ProgressTracker>> GetAllProgress()
        {
            return await _context.ProgressTrackers.ToListAsync();
        }

        public async Task<List<ProgressTracker>> GetProgressByAthleteId(int id)
        {
            return await _context.ProgressTrackers.Where(a => a.AthletesId == id).OrderBy(a => a.WorkOut).ThenBy(a => a.Date).ToListAsync();
        }

        public async Task<ProgressTracker?> UpdateProgressTrackerByDayAndId(DateOnly day, int id, string workOut, UpdateProgressTrackerDto updateModel)
        {
            var progressTracker = _context.ProgressTrackers.FirstOrDefault(a => a.Date == day && a.AthletesId == id && a.WorkOut == workOut);

            if (progressTracker == null)
            {
                return null;
            }

            progressTracker.Value_Dor_Date = updateModel.Value_Dor_Date;
            progressTracker.WorkOut = updateModel.WorkOut;

            await _context.SaveChangesAsync();
            return progressTracker;

        }
    }
}