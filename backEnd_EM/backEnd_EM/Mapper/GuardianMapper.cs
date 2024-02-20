using backEnd_EM.Dtos;
using backEnd_EM.Migrations;
using backEnd_EM.Properties.Models;
using Org.BouncyCastle.Bcpg;

namespace backEnd_EM.Mapper
{
    public static class GuardianMapper
    {
        public static GuardianDto GuardionToGuardianDto(Guardians guardianModel)
        {
            return new GuardianDto
            {
                Name = guardianModel.Name,
                Email = guardianModel.Email,
                Number = guardianModel.Number
            };
        }
        public static Guardians CreateRequestToGaurdian(this CreateGuardianDto createGuardianModel, int Id)
        {
            return new Guardians
            {
                Name = createGuardianModel.Name,
                Email = createGuardianModel.Email,
                Number = createGuardianModel.Number,
                AthletesId = Id
            };
        }
    }
}