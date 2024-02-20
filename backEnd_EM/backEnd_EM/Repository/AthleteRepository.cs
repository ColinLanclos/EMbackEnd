using backEnd_EM.Models;
using backEnd_EM.Properties.Models;
using backEnd_EM.Interfaces;
using Microsoft.EntityFrameworkCore;
using backEnd_EM.Mapper;
using backEnd_EM.Dtos.Athletes;
using System.Security.Cryptography;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace backEnd_EM.Repository
{
    public class AthleteRepository : IAthleteRepository
    {
        private readonly AppDBContext _context;
        private readonly IConfiguration _configuration;
        public AthleteRepository(AppDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        public async Task<Athletes?> CreateAthlete(Athletes athleteModel)
        {
            if (await _context.Athletes.AnyAsync(u => u.Email == athleteModel.Email))
            {
                return null;
            }
            await _context.Athletes.AddAsync(athleteModel);
            await _context.SaveChangesAsync();
            return athleteModel;
        }

        //need to figure out how to cascade delete
        public async Task<Athletes?> DeleteAthlete(int id)
        {
            var athletesModel = _context.Athletes.FirstOrDefault(x => x.Id == id);

            if (athletesModel == null)
            {
                return null;
            }

            _context.Athletes.Remove(athletesModel);
            await _context.SaveChangesAsync();
            return athletesModel;
        }

        public async Task<Athletes?> GetAthleteById(int id)
        {
            var Athlete = await _context.Athletes.FindAsync(id);

            if (Athlete == null)
            {
                return null;
            }

            return Athlete;
        }

        public async Task<List<Athletes>> GetAthletes()
        {
            return await _context.Athletes.ToListAsync();
        }

        public async Task<Athletes?> GetAthletesByPhone(long Phone)
        {
            return await _context.Athletes.FirstOrDefaultAsync(a => a.Phone == Phone);
        }

        public async Task<int?> GetAthletesByPhoneForId(long phone)
        {
            var Athlete = await _context.Athletes.FirstOrDefaultAsync(a => a.Phone == phone);
            if (Athlete == null)
            {
                return null;
            }
            var id = Athlete.Id;
            return id;
        }

        public async Task<List<Athletes>> GetListOfAthletes()
        {
            return await _context.Athletes.ToListAsync();
        }

        public async Task<string> LoginAthelte(string Email, string PasswordAttempt)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(a => a.Email == Email);
            if (athlete == null)
            {
                return "No Email";
            }
            else if (ckeckPassword(PasswordAttempt, athlete.Password, athlete.PasswordSalt))
            {
                //string JwtToken = CreateToken(athlete);

                return athlete.Id.ToString();
            }
            else
            {
                return "Password Wrong";
            }
        }

        private string CreateToken(Athletes athlete)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.Name, athlete.Email)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine("**" + jwt + "****");
            return jwt;
        }

        private bool ckeckPassword(string passwordAttempt, byte[] password, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordAttempt));
                return hashPassword.SequenceEqual(password);
            }
        }

        public async Task<Athletes?> UpdateAthlete(int id, UpdateAthleteRequestDTO athleteDto)
        {
            var exsistingAthlete = await _context.Athletes.FirstOrDefaultAsync(x => x.Id == id);

            if (exsistingAthlete == null)
            {
                return null;
            }
            exsistingAthlete.Name = athleteDto.Name;
            exsistingAthlete.Guardian = athleteDto.Guardian;
            exsistingAthlete.Birthday = athleteDto.Birthday;
            exsistingAthlete.Classification = athleteDto.Classification;
            exsistingAthlete.Height = athleteDto.Height;
            exsistingAthlete.Weight = athleteDto.Weight;
            exsistingAthlete.School = athleteDto.School;
            exsistingAthlete.Phone = athleteDto.Phone;

            await _context.SaveChangesAsync();

            return exsistingAthlete;
        }

        public async Task<string?> ResetPasswordAttempt(string Email)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(a => a.Email == Email);
            if (athlete == null)
            {
                return null;
            }

            Random random = new Random();
            int randomNumber = random.Next(10000, 100000);

            athlete.PasswordResetToken = randomNumber.ToString();
            //var date = DateTime.Now.AddDays(1);
            //athlete.ResetTokenExperies = date;

            await _context.SaveChangesAsync();

            using var mail = new MimeMessage();

            mail.From.Add(new MailboxAddress("Colin Lanclos", "colinlanclos@gmail.com"));
            mail.To.Add(new MailboxAddress("Colin Lanclos", "colinlanclos@gmail.com"));

            mail.Subject = "Reset Password";
            mail.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = $"<b>{randomNumber}</b>"
            };
            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("colinlanclos@gmail.com", "akrm jtvb atyf qaln");

                smtp.Send(mail);
                smtp.Disconnect(true);
            }
            return "Email Sent";
        }

        public async Task<string> ResetPassword(PasswordResetDto passwordResetModel)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(a => a.Email == passwordResetModel.Email);

            if (athlete == null)
            {
                return "Something went wrong";
            }

            var ResetToken = athlete.PasswordResetToken;

            if (!ResetToken.Equals(passwordResetModel.PasswordResetToken))
            {
                return "Token is incorrect";
            }

            if (passwordResetModel.ConfirmPassword.Equals(passwordResetModel.NewPassword))
            {
                CreatePasswordHash(passwordResetModel.NewPassword, out byte[] HashPassword, out byte[] PasswordSalt);
                athlete.Password = HashPassword;
                athlete.PasswordResetToken = "";
                athlete.PasswordSalt = PasswordSalt;
                await _context.SaveChangesAsync();
                return "Password Succesfully reset";
            }
            return "Passwords did not match";
        }

        private void CreatePasswordHash(string password, out byte[] hashPassword, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                hashPassword = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<string?> UpdatePassword(int id, UpdatePasswordDto passwordUpdateModel)
        {
            var athlete = await _context.Athletes.FindAsync(id);

            if (athlete == null)
            {
                return null;
            }
            if (ckeckPassword(passwordUpdateModel.OldPassword, athlete.Password, athlete.PasswordSalt))
            {
                if (passwordUpdateModel.NewPassword.Equals(passwordUpdateModel.ConfirmPassword))
                {
                    CreatePasswordHash(passwordUpdateModel.NewPassword, out byte[] hashPassword, out byte[] passwordSalt);
                    athlete.Password = hashPassword;
                    athlete.PasswordSalt = passwordSalt;
                    await _context.SaveChangesAsync();
                    return "Password changed";
                }
                else
                {
                    return "Passwords do not match";
                }
            }
            return "Old password does not match";
        }

        public async Task<int?> GetAthletesByEmailForId(string email)
        {
            var Athlete = await _context.Athletes.FirstOrDefaultAsync(a => a.Email == email);
            if (Athlete == null)
            {
                return null;
            }
            var id = Athlete.Id;
            return id;
        }
    }
}