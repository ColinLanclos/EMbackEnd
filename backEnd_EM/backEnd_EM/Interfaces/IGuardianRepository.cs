using backEnd_EM.Dtos;
using backEnd_EM.Properties.Models;

namespace backEnd_EM.Interfaces
{
    public interface IGuardianRepository
    {

        Task<List<Guardians>> GetGuardians();

        Task<Guardians?> GetGuardianById(int id);

        Task<Guardians?> GetGuardianByPhone(long Phone);

        Task<List<Guardians>> GetListOfGuardian();

        Task<Guardians> CreateGuardian(Guardians athleteModel);

        //Add dto
        Task<Guardians?> UpdateGuardian(int id, long phone, UpdateGuardianDto model);

        Task<List<Guardians?>?> GetGuardiansByAthleteId(int id);

    }
}
