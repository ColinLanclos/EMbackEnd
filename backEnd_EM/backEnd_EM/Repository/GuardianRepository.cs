using backEnd_EM.Dtos;
using backEnd_EM.Interfaces;
using backEnd_EM.Models;
using backEnd_EM.Properties.Models;
using Microsoft.EntityFrameworkCore;

namespace backEnd_EM.Repository
{
    public class GuardianRepository : IGuardianRepository
    {

        private readonly AppDBContext _context;

        public GuardianRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Guardians> CreateGuardian(Guardians guardiansModel)
        {
            await _context.AddAsync(guardiansModel);
            await _context.SaveChangesAsync();
            return guardiansModel;
        }

        public async Task<Guardians?> GetGuardianById(int id)
        {
            var Guardian = await _context.Guardians.FirstOrDefaultAsync(a => a.Id == id);

            if (Guardian == null)
            {
                return null;
            }
            return Guardian;
        }

        public async Task<Guardians?> GetGuardianByPhone(long Phone)
        {
            var Guardian = await _context.Guardians.FirstOrDefaultAsync(a => a.Number == Phone);

            if (Guardian == null)
            {
                return null;
            }
            return Guardian;
        }

        public async Task<List<Guardians>> GetGuardians()
        {
            return await _context.Guardians.ToListAsync();
        }

        public async Task<List<Guardians?>?> GetGuardiansByAthleteId(int id)
        {
            var guardian = await _context.Guardians.Where(g => g.AthletesId == id).ToListAsync();

            if (guardian == null)
            {
                return null;
            }



            return guardian;
        }

        public async Task<List<Guardians>> GetListOfGuardian()
        {
            return await _context.Guardians.ToListAsync();
        }

        public async Task<Guardians?> UpdateGuardian(int id, long phone, UpdateGuardianDto updateGuardianModel)
        {
            var Guardian = await _context.Guardians.FirstOrDefaultAsync(a => a.AthletesId == id && a.Number == phone);

            if (Guardian == null)
            {
                return null;
            }

            Guardian.Email = updateGuardianModel.Email;
            Guardian.Name = updateGuardianModel.Name;
            Guardian.Number = updateGuardianModel.Number;

            await _context.SaveChangesAsync();

            return Guardian;
        }
    }
}