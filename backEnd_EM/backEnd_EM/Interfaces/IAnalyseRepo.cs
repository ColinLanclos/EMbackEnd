using backEnd_EM.Dtos.Analyse;
using backEnd_EM.Properties.Models;

namespace backEnd_EM.Interfaces
{
    public interface IAnalyseRepo
    {
        //done
        Task<List<Analyse>> GetAnalyses();

        //done
        Task<List<Analyse>> GetAnalysesByAthleteId(int id);

        //done
        Task<Analyse?> UpdateAnalyseByDayAndId(string day, int id, AnalyseUpdateDto analyseUpdateDto);

        //Done
        Task<Analyse?> DeleteAnalyseByDayandId(string day, int id);

        //done
        Task<List<Analyse>> GetListOfDays(int id);

        //done
        Task<Analyse> CreateAnalyse(Analyse analyseModel);
    }
}