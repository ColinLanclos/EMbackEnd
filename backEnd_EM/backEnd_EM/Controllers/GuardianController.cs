using Microsoft.AspNetCore.Mvc;
using backEnd_EM.Interfaces;
using backEnd_EM.Properties.Models;
using backEnd_EM.Models;
using backEnd_EM.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using backEnd_EM.Dtos.Athletes;
using backEnd_EM.Dtos;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Microsoft.AspNetCore.Authorization;


namespace backEnd_EM.Controllers
{
    [Route("api/guardian")]
    [ApiController]
    public class GuardianController : ControllerBase
    {

        private readonly IGuardianRepository _guardianRepo;
        private readonly IAthleteRepository _athleteRepo;

        public GuardianController(IGuardianRepository guardianRepo, IAthleteRepository athleteRepo)
        {
            _guardianRepo = guardianRepo;
            _athleteRepo = athleteRepo;
        }

        [HttpGet]
        [Route("GetAllGuradians")]
        public async Task<IActionResult> GetAllGuradians()
        {
            var Guardians = await _guardianRepo.GetGuardians();

            if (Guardians == null)
            {
                return BadRequest("No Guardians");
            }
            return Ok(Guardians);
        }

        [HttpGet]
        [Route("GetGuardianById/{id}"), Authorize]
        public async Task<IActionResult> GetGuardianById([FromRoute] int id)
        {
            var Guardian = await _guardianRepo.GetGuardianById(id);

            if (Guardian == null)
            {
                return BadRequest("No Guardian");
            }
            return Ok(Guardian);
        }

        [HttpPut]
        [Route("CreateGaurdian/{athletId}")]
        public async Task<IActionResult> CreateGaurdian([FromRoute] int athletId, [FromBody] CreateGuardianDto guardianDto)
        {
            var guardian = guardianDto.CreateRequestToGaurdian(athletId);

            var model = await _guardianRepo.CreateGuardian(guardian);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(model);
        }

        [HttpGet]
        [Route("GetGuardiansByAthleteId/{athleteId}"), Authorize]
        public async Task<IActionResult> GetGuardiansByAthleteId([FromRoute] int athleteId)
        {
            var guardian = await _guardianRepo.GetGuardiansByAthleteId(athleteId);

            if (guardian == null)
            {
                return BadRequest("No Guadian found with that Athlete");
            }
            return Ok(guardian);
        }


        [HttpGet, Authorize]
        [Route("GetGuardianByAthletePhone/{phone}")]
        public async Task<IActionResult> GetGuardianByAthletePhone([FromRoute] long phone)
        {
            var AthleteId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthleteId == null)
            {
                return BadRequest("Phone not found");
            }
            var guardian = await _guardianRepo.GetGuardiansByAthleteId((int)AthleteId);
            if (guardian == null)
            {
                return BadRequest("No Guadian found with that Athlete");
            }
            return Ok(guardian);
        }

        [HttpPut, Authorize]
        [Route("UpdateGuardianInfo/{athleteId}/{phone}")]
        public async Task<IActionResult> UpdateGuardianInfo([FromRoute] int athleteId, [FromRoute] long phone, [FromBody] UpdateGuardianDto guardianDto)
        {
            var UpdateGuardian = await _guardianRepo.UpdateGuardian(athleteId, phone, guardianDto);

            if (UpdateGuardian == null)
            {
                return BadRequest("Phone to update is wrong");
            }

            return Ok("Succesefull Update");
        }
    }
}