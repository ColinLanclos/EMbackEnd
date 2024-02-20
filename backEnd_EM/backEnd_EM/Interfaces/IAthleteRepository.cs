
using backEnd_EM.Dtos.Athletes;
using backEnd_EM.Properties.Models;

namespace backEnd_EM.Interfaces
{
    //All done besides Password stuff 
    public interface IAthleteRepository
    {
        //done
        Task<List<Athletes>> GetAthletes();

        //done
        Task<Athletes?> GetAthleteById(int id);

        //done
        Task<Athletes?> GetAthletesByPhone(long Phone);

        //done
        Task<int?> GetAthletesByPhoneForId(long phone);

        Task<int?> GetAthletesByEmailForId(string email);

        //done
        Task<List<Athletes>> GetListOfAthletes();

        //done
        Task<Athletes?> CreateAthlete(Athletes athleteModel);

        //done
        Task<Athletes?> UpdateAthlete(int id, UpdateAthleteRequestDTO athleteDto);

        //done
        Task<Athletes?> DeleteAthlete(int id);

        //done
        Task<string> LoginAthelte(string Email, string PasswordAttempt);

        //done
        Task<string?> ResetPasswordAttempt(string Email);

        //done
        Task<string?> ResetPassword(PasswordResetDto passwordResetModel);

        //done
        Task<string?> UpdatePassword(int id, UpdatePasswordDto passwordUpdateModel);
    }
}