using backEnd_EM.Models;
using backEnd_EM.Properties.Models;
using backEnd_EM.Interfaces;
using Microsoft.EntityFrameworkCore;
using backEnd_EM.Dtos.Analyse;

namespace backEnd_EM.Repository
{
    public class AnalyseRepo : IAnalyseRepo
    {
        private readonly AppDBContext _context;
        public AnalyseRepo(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Analyse> CreateAnalyse(Analyse analyseModel)
        {
            await _context.Analyses.AddAsync(analyseModel);
            await _context.SaveChangesAsync();
            return analyseModel;
        }

        public async Task<Analyse?> DeleteAnalyseByDayandId(string day, int id)
        {
            var analyseModel = _context.Analyses.FirstOrDefault(x => x.AthletesId == id && x.Day == day);

            if (analyseModel == null)
            {
                return null;
            }

            _context.Analyses.Remove(analyseModel);
            await _context.SaveChangesAsync();
            return analyseModel;
        }

        public async Task<List<Analyse>> GetAnalyses()
        {
            return await _context.Analyses.ToListAsync();
        }

        public async Task<List<Analyse>> GetAnalysesByAthleteId(int id)
        {
            return await _context.Analyses.Where(a => a.AthletesId == id).ToListAsync();
        }

        public async Task<List<Analyse>> GetListOfDays(int id)
        {
            return await _context.Analyses.Where(a => a.AthletesId == id).ToListAsync();
        }

        public async Task<Analyse?> UpdateAnalyseByDayAndId(string day, int id, AnalyseUpdateDto updateDto)
        {
            var exsistingAnalyse = await _context.Analyses.FirstOrDefaultAsync(a => a.AthletesId == id && a.Day == day);

            if (exsistingAnalyse == null)
            {
                return null;
            }

            exsistingAnalyse.videoURL = updateDto.videoURL;
            exsistingAnalyse.Title = updateDto.Title;
            exsistingAnalyse.BreakDown = updateDto.BreakDown;
            exsistingAnalyse.Day = updateDto.Day;
            exsistingAnalyse.Date = DateOnly.Parse(updateDto.Date);

            await _context.SaveChangesAsync();

            return exsistingAnalyse;
        }
    }
}

