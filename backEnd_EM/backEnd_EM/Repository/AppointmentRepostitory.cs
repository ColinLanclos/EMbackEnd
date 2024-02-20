using backEnd_EM.Models;
using backEnd_EM.Properties.Models;
using backEnd_EM.Interfaces;
using Microsoft.EntityFrameworkCore;
using backEnd_EM.Mapper;
using backEnd_EM.Dtos.Athletes;

namespace backEnd_EM.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public Task<Appointments> CreateAppointment(Appointments appointmentsModel)
        {
            throw new NotImplementedException();
        }

        public Task<Appointments?> DeleteAppointmentsByAthleteIdAndDate(int id, DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<Appointments?> DeleteAppointmentsByDate(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAllAppointments()
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsByDate(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointments>> GetAppointmentsById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Appointments?> UpdateAppointmentByIdAndDate(int id, DateOnly date)
        {
            throw new NotImplementedException();
        }
    }
}