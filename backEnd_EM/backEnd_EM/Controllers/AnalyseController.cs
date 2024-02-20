using Microsoft.AspNetCore.Mvc;
using backEnd_EM.Interfaces;
using backEnd_EM.Properties.Models;
using backEnd_EM.Models;
using backEnd_EM.Mapper;
using backEnd_EM.Dtos.Analyse;


namespace backEnd_EM.Controllers
{
    [Route("api/analyse")]
    [ApiController]
    public class AnalyseControllers : ControllerBase
    {

        private readonly IAnalyseRepo _analyseRepo;
        private readonly AppDBContext _context;

        private readonly IAthleteRepository _athleteRepo;


        public AnalyseControllers(IAnalyseRepo analyseRepo, AppDBContext context, IAthleteRepository athleteRepo)
        {
            _analyseRepo = analyseRepo;
            _context = context;
            _athleteRepo = athleteRepo;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<Analyse>))]
        public async Task<IActionResult> GetAnalyse()
        {
            var Analyses = await _analyseRepo.GetAnalyses();
            var AnalysesDto = Analyses.Select(a => a.ToAnalyseDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(AnalysesDto);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetByAthleteId([FromRoute] int id)
        {
            var Analyses = await _analyseRepo.GetAnalysesByAthleteId(id);
            var AnalyseDto = Analyses.Select(a => a.ToAnalyseDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Analyses);
        }

        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetAthleteByPhone([FromRoute] long phone)
        {
            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Phone not found");
            }

            var Analyses = await _analyseRepo.GetAnalysesByAthleteId((int)AthletesId);
            var AnalyseDto = Analyses.Select(a => a.ToAnalyseDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Analyses);
        }

        [HttpPost("makeAnalyseByPhone/{phone}")]
        public async Task<IActionResult> MakeAnalyseByPhone([FromRoute] long phone, CreateAnalyseDto analyseDto)
        {

            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Athlete not found");
            }

            var analyseModel = analyseDto.ToAnalyseFromCreate((int)AthletesId);

            await _analyseRepo.CreateAnalyse(analyseModel);
            return CreatedAtAction(nameof(GetByAthleteId), new { id = analyseModel }, analyseModel.ToAnalyseDto());
        }
        [HttpGet("listOfDays/{phone}")]
        public async Task<IActionResult> GetListOfDaysPerClient([FromRoute] long phone)
        {
            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Athlete not found");
            }

            var AthleteDayList = await _analyseRepo.GetListOfDays((int)AthletesId);

            if (AthleteDayList == null)
            {
                return BadRequest("No Analyses Found");
            }

            var DayListDto = AthleteDayList.Select(a => a.ToDayList());

            return Ok(DayListDto);
        }

        [HttpDelete("deleteAnalyseByPhone/{phone}/{day}")]
        public async Task<IActionResult> DeleteAnalyseByPhone([FromRoute] long phone, [FromRoute] string day)
        {
            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Athlete not found");
            }

            var AnalyseModel = await _analyseRepo.DeleteAnalyseByDayandId(day, (int)AthletesId);
            if (AnalyseModel == null)
            {
                return BadRequest("Analyse Not Found");
            }

            return Ok(AnalyseModel);
        }

        [HttpPut("updateAnalyse/{phone}/{day}")]
        public async Task<IActionResult> UpdateAnalyse([FromRoute] long phone, [FromRoute] string day, [FromBody] AnalyseUpdateDto analyseUpdateDto)
        {
            var AthletesId = await _athleteRepo.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Athlete not found");
            }

            var AnalyseModel = await _analyseRepo.UpdateAnalyseByDayAndId(day, (int)AthletesId, analyseUpdateDto);

            if (AnalyseModel == null)
            {
                return BadRequest("Update Failed");
            }

            return Ok(AnalyseModel.ToAnalyseDto());

        }
    }
}
