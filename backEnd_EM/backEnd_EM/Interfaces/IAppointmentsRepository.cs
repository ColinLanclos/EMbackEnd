using backEnd_EM.Properties.Models;

namespace backEnd_EM.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointments>> GetAllAppointments();

        Task<List<Appointments>> GetAppointmentsById(int id);

        Task<Appointments?> DeleteAppointmentsByDate(DateOnly date);

        Task<Appointments?> DeleteAppointmentsByAthleteIdAndDate(int id, DateOnly date);

        Task<List<Appointments>> GetAppointmentsByDate(DateOnly date);

        //need to add Dto
        Task<Appointments?> UpdateAppointmentByIdAndDate(int id, DateOnly date);

        Task<Appointments> CreateAppointment(Appointments appointmentsModel);

    }
}