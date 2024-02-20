using backEnd_EM.Models;
using backEnd_EM.Properties.Models;
using backEnd_EM.Interfaces;
using Microsoft.EntityFrameworkCore;
using backEnd_EM.Dtos.Analyse;
using backEnd_EM.Dtos.Analytics;

namespace backEnd_EM.Repository
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly AppDBContext _context;
        public AnalyticsRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Analytics>> AllAnalyticsSorted()
        {
            var Analytics = await _context.Analytics.OrderBy(s => s.AmountOfTimesLogin).ToListAsync();
            return Analytics;
        }

        public async Task<Analytics?> CreateAnalytics(Analytics analytic)
        {
            await _context.Analytics.AddAsync(analytic);
            await _context.SaveChangesAsync();
            return analytic;
        }

        public async Task<Analytics?> GetAnalyticsByAthleteId(int id)
        {
            var Analytic = await _context.Analytics.FirstOrDefaultAsync(a => a.AthletesId == id);
            if (Analytic == null)
            {
                return null;
            }
            return Analytic;
        }

        public async Task<Analytics?> IncreamentWhenLogin(int AthleteId)
        {
            var AnalyticToUpdate = await _context.Analytics.FirstOrDefaultAsync(a => a.AthletesId == AthleteId);


            if (AnalyticToUpdate.RecentLoginDate.Date == DateTime.Now.Date)
            {
                return null;
            }

            AnalyticToUpdate.AmountOfTimesLogin += 1;
            AnalyticToUpdate.RecentLoginDate = DateTime.UtcNow;
            //discuss ranks

            await _context.SaveChangesAsync();

            return AnalyticToUpdate;
        }

        public Task<int> ResetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Analytics?> UpdateRank(UpdateAnalyticsDto analyticsModle, int AthleteId)
        {
            var AnalyticToUpdate = await _context.Analytics.FirstOrDefaultAsync(a => a.AthletesId == AthleteId);

            if (AnalyticToUpdate == null)
            {
                return null;
            }

            AnalyticToUpdate.AmountOfTimesLogin = analyticsModle.AmountOfTimesLogin;
            //right here we need to discuss ranks 
            await _context.SaveChangesAsync();
            return AnalyticToUpdate;
        }
    }
}