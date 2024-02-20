using Microsoft.AspNetCore.Mvc;
using backEnd_EM.Interfaces;
using backEnd_EM.Properties.Models;
using backEnd_EM.Models;
using backEnd_EM.Mapper;
using backEnd_EM.Dtos.Analyse;
using backEnd_EM.Dtos.Analytics;
namespace backEnd_EM.Controllers

{
    [Route("api/analytics")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {

        private readonly IAthleteRepository _athleteRepository;

        private readonly IAnalyticsRepository _analyticsRepo;



        public AnalyticsController(IAthleteRepository athleteRepo, IAnalyticsRepository analyticsRepo)
        {
            _athleteRepository = athleteRepo;
            _analyticsRepo = analyticsRepo;
        }

        [HttpGet]
        [Route("GetAllAnalytics")]
        public async Task<IActionResult> GetAllAnalytics()
        {
            var Analytics = await _analyticsRepo.AllAnalyticsSorted();

            var analyticDto = Analytics.Select(a => a.ToAnalyticsDto());

            return Ok(analyticDto);
        }


        //Will be tested but implemented when Athlete is created
        [HttpPost]
        [Route("CreateAnalytis")]
        public async Task<IActionResult> CreateAnalytis([FromBody] CreateRequestAnalyticsDto analyticsDto)
        {
            var analyticModel = analyticsDto.CreateRequest();

            var analytic = await _analyticsRepo.CreateAnalytics(analyticModel);
            if (analytic == null)
            {
                return BadRequest("thing messed up");
            }
            return Ok(analytic);
        }

        [HttpGet]
        [Route("GetAnalytisById/{id}")]
        public async Task<IActionResult> GetAnalytisById([FromRoute] int id)
        {
            var analytic = await _analyticsRepo.GetAnalyticsByAthleteId(id);
            if (analytic == null)
            {
                return BadRequest("System Error");
            }
            return Ok(analytic.ToAnalyticsDto());
        }
        [HttpGet]
        [Route("GetAnalytisByPhone/{phone}")]
        public async Task<IActionResult> GetAnalytisByPhone([FromRoute] long phone)
        {
            var AthletesId = await _athleteRepository.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Phone not found");
            }

            var analytic = await _analyticsRepo.GetAnalyticsByAthleteId((int)AthletesId);
            if (analytic == null)
            {
                return BadRequest("System Error");
            }
            return Ok(analytic.ToAnalyticsDto());
        }

        [HttpPut]
        [Route("UpdateRank/{phone}")]
        public async Task<IActionResult> UpdateRank([FromRoute] long phone, [FromBody] UpdateAnalyticsDto updateDto)
        {
            var AthletesId = await _athleteRepository.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Phone not found");
            }
            var UpdateResponce = await _analyticsRepo.UpdateRank(updateDto, (int)AthletesId);
            if (UpdateResponce == null)
            {
                return BadRequest("System Error");
            }
            return Ok(UpdateResponce);
        }

    }
}
