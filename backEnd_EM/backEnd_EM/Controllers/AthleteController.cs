using Microsoft.AspNetCore.Mvc;
using backEnd_EM.Interfaces;
using backEnd_EM.Properties.Models;
using backEnd_EM.Models;
using backEnd_EM.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using backEnd_EM.Dtos.Athletes;
using backEnd_EM.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace backEnd_EM.Controllers
{
    [Route("api/athlete")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private AppDBContext _context;

        private readonly IConfiguration _configuration;

        private readonly IAthleteRepository _athleteRepository;

        private readonly IAnalyticsRepository _analyticsRepo;

        private readonly ITokenService _tokenService;



        public AthleteController(AppDBContext context, IAthleteRepository athleteRepo, IConfiguration configuration, IAnalyticsRepository analyticsRepo, ITokenService tokenService)
        {
            _context = context;
            _athleteRepository = athleteRepo;
            _configuration = configuration;
            _analyticsRepo = analyticsRepo;
            _tokenService = tokenService;
        }

        //Justing Getting Info (Just an example)
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAthletes()
        {
            var Athletes = await _athleteRepository.GetAthletes();

            var AthleteDto = Athletes.Select(a => a.ToAthletesDto());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Athletes);
        }

        //Display Athlete Basic infor for parents,Player, or coach at the time 
        [HttpGet("byId/{id}")]
        public IActionResult GetAthletesById([FromRoute] int id)
        {
            var Athletes = _context.Athletes.Find(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(Athletes.ToAthletesDto());
        }

        //returns athlete's id by phone number 
        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetAthletesByPhone([FromRoute] long phone)
        {
            var AthletesId = await _athleteRepository.GetAthletesByPhoneForId(phone);

            if (AthletesId == null)
            {
                return BadRequest("Athlete not found");
            }

            var Athlete = await _athleteRepository.GetAthleteById((int)AthletesId);

            if (Athlete == null)
            {
                return BadRequest("Athlete not found");
            }
            return Ok(Athlete.ToAthletesDto());
        }

        //returnsList of Athletes and numbers
        [HttpGet("listAthletes")]
        public async Task<IActionResult> GetListOfAthletes()
        {
            var listAthletes = await _athleteRepository.GetListOfAthletes();

            var listAthletesDto = listAthletes.Select(a => a.ToAthleteAthleteNumberDto());

            return Ok(listAthletesDto);
        }

        [HttpGet]
        [Route("CreateJWTTest")]
        public async Task<IActionResult> CreateJWTTest()
        {
            var athlete = await _athleteRepository.GetAthleteById(10);

            var token = _tokenService.CreateToken(athlete);
            return Ok(token);
        }
        //Creating new Athlete
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAthleteRequestDto AthletesDto)
        {
            CreatePasswordHash(AthletesDto.Password, out byte[] HashPassword, out byte[] PasswordSalt);

            var athletesModel = AthletesDto.ToAthleteFromCreateDTO(HashPassword, PasswordSalt);

            var athlete = await _athleteRepository.CreateAthlete(athletesModel);

            if (athlete == null)
            {
                BadRequest("Email already exists");
            }

            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAthletesById), new { id = athletesModel.Id }, athletesModel.ToAthletesDto());
        }

        private void CreatePasswordHash(string password, out byte[] hashPassword, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        //player to update there own profile 
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAthleteRequestDTO updateAthlete)
        {
            var athletesModel = await _athleteRepository.UpdateAthlete(id, updateAthlete);

            if (athletesModel == null)
            {
                return NotFound();
            }
            return Ok(athletesModel.ToAthletesDto());
        }

        //For Cam and them to delete player
        //need to figure out how to cascade delete 
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var athleteModel = await _athleteRepository.DeleteAthlete(id);

            if (athleteModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var LoginResponse = await _athleteRepository.LoginAthelte(loginDto.Email, loginDto.PasswordAttempt);
            if (LoginResponse == "No Email")
            {
                return BadRequest("Couldn't Find Email");
            }
            else if (LoginResponse == "Password Wrong")
            {
                return BadRequest("Password Attempt was Incorrect");
            }
            else
            {
                await _analyticsRepo.IncreamentWhenLogin((int)await _athleteRepository.GetAthletesByEmailForId(loginDto.Email));
                var athlete = await _athleteRepository.GetAthleteById(int.Parse(LoginResponse));
                var jwt = _tokenService.CreateToken(athlete!);
                var loginResponseModel = athlete.LoginResponseToDto(jwt);
                return Ok(loginResponseModel);
            }
        }

        [HttpGet]
        [Route("passwordResetRequest/{email}")]
        public async Task<IActionResult> PasswordResetRequest([FromRoute] string email)
        {
            var sendingMail = await _athleteRepository.ResetPasswordAttempt(email);
            if (sendingMail == null)
            {
                return BadRequest("Invalid Email");
            }
            return Ok(sendingMail);

        }
        [HttpPost]
        [Route("passwordReset")]
        public async Task<IActionResult> PasswordReset([FromBody] PasswordResetDto passwordDto)
        {
            var reasetPasswordResponse = await _athleteRepository.ResetPassword(passwordDto);

            if (reasetPasswordResponse == "Password Succesfully reset")
            {
                return Ok(reasetPasswordResponse);
            }
            return BadRequest(reasetPasswordResponse);
        }

        [HttpPost]
        [Route("updatePasswords/{id}")]
        public async Task<IActionResult> UpdatePassword([FromRoute] int id, [FromBody] UpdatePasswordDto updatePasswordModle)
        {
            var UpdatePasswordResponce = await _athleteRepository.UpdatePassword(id, updatePasswordModle);

            if (UpdatePasswordResponce == null)
            {
                return BadRequest("Internal Error");
            }
            if (UpdatePasswordResponce.Equals("Passwords do not match"))
            {
                return BadRequest(UpdatePasswordResponce);
            }
            if (UpdatePasswordResponce.Equals("Old password does not match"))
            {
                return BadRequest(UpdatePasswordResponce);
            }
            if (UpdatePasswordResponce.Equals("Password changed"))
            {
                return Ok(UpdatePasswordResponce);
            }
            return BadRequest(UpdatePasswordResponce);

        }
    }
}
