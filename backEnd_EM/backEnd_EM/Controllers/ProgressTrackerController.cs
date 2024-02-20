using Microsoft.AspNetCore.Mvc;
using backEnd_EM.Interfaces;
using backEnd_EM.Properties.Models;
using backEnd_EM.Models;
using backEnd_EM.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using backEnd_EM.Dtos.Athletes;
using backEnd_EM.Dtos;
using Microsoft.EntityFrameworkCore;


namespace backEnd_EM.Controllers
{
    [Route("api/progrestracker")]
    [ApiController]
    public class ProgresTrackerController : ControllerBase
    {

        private readonly IProgressTrackerRepository _progressTrackerRepository;
        private readonly IAthleteRepository _athleteRepo;

        public ProgresTrackerController(IProgressTrackerRepository progressTrackerRepo, IAthleteRepository athleteRepo)
        {
            _progressTrackerRepository = progressTrackerRepo;
            _athleteRepo = athleteRepo;
        }

        [HttpGet]
        public async Task<IActionResult> allProgress()
        {
            var Progress = await _progressTrackerRepository.GetAllProgress();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Progress);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var ProgressById = await _progressTrackerRepository.GetProgressByAthleteId(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(ProgressById);
        }

        [HttpDelete("deleteProgress/{phone}/{workOut}/{date}")]
        public async Task<IActionResult> deleteProgress([FromRoute] long phone, [FromRoute] string workOut, [FromRoute] string date)
        {
            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Phone not found");
            }

            var progressModel = await _progressTrackerRepository.DeleteProgressTrackerByDayandId(DateOnly.Parse(date), (int)AthletesId, workOut);

            if (progressModel == null)
            {
                return BadRequest("Could not find Workout");
            }
            return Ok(progressModel);
        }

        [HttpPost("addProgress/{phone}")]
        public async Task<IActionResult> addProgress([FromRoute] long phone, [FromBody] CreateProgressDto progressModel)
        {
            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Phone not found");
            }

            var ProgressTrackerModel = progressModel.CreateProgressTrackerFromDto((int)AthletesId);

            var Model = await _progressTrackerRepository.CreateProgressTracker(ProgressTrackerModel);

            if (!ModelState.IsValid)
            {
                return BadRequest("Model went Wrong");
            }
            return Ok(Model);
        }

        [HttpPut("updateTracker/{phone}/{date}/{workOut}")]
        public async Task<IActionResult> UpdateProgresTracker([FromRoute] long phone, [FromRoute] string date, [FromRoute] string workOut, [FromBody] UpdateProgressTrackerDto progressTrackerModel)
        {
            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Phone not found");
            }

            var progressModel = await _progressTrackerRepository.UpdateProgressTrackerByDayAndId(DateOnly.Parse(date), (int)AthletesId, workOut, progressTrackerModel);

            if (progressModel == null)
            {
                return BadRequest("Progress Tracker Not found");
            }
            return Ok("Everything Updated");
        }
    }
}
