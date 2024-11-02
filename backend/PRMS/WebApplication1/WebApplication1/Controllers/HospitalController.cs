using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure you use the correct EF Core namespace
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.Identity.IdentityInterface;
using PRMS_BackendAPI.Identity.Identitys;
using PRMS_BackendAPI.Identity.Infra_Identitiy;
using PRMS_BackendAPI.Identity.Infra_Identitiy.Models;
using PRMS_BackendAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using PRMS_BackendAPI.Identity;

namespace PRMS_BackendAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class HospitalController : Controller
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly PRMS_IdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public HospitalController(PRMS_DatabaseContext dbContext, 
            IMapper mapper,
            PRMS_IdentityDbContext identityDbContext,
            IAuthService authenticationService,
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _identityDbContext = identityDbContext;
            _authenticationService = authenticationService;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }


        [HttpGet]

        public async Task<ActionResult<List<HospitalDTO>>> GetHospital()
        {

            if (_dbContext.Hospitals == null)
            {
                return NotFound();
            }

            var hospitaldto = _mapper.Map<List<HospitalDTO>>(_dbContext.Hospitals);
            return Ok(hospitaldto);
        }

        [HttpPost]
        public async Task<ActionResult<HospitalDTO>> PostDoctor(HospitalDTO hospitaldto)
        {
            if (_dbContext.Hospitals == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.hospitals' is null.");
            }


            var hospital = _mapper.Map<Hospital>(hospitaldto);


            _dbContext.Hospitals.Add(hospital);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<HospitalDTO>(hospital);


            return CreatedAtAction(nameof(GetHospital), new { id = hospitaldto.HospitalId }, SavedDTO);
        }

        [HttpPost("withtoken")]
        public async Task<ActionResult<HospitalDTO>> PostHospital(HospitalDTO hospitalDTO)
        {
            if (_dbContext.Hospitals == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Hospitals' is null.");
            }

            // Step 1: Map the Hospital DTO to the Hospital entity
            var hospital = _mapper.Map<Hospital>(hospitalDTO);

            // Step 2: Add Hospital to Database
            _dbContext.Hospitals.Add(hospital);
            await _dbContext.SaveChangesAsync();

            // Step 3: Generate email and password for the hospital
            var generatedEmail = $"{hospital.HospitalEmail.ToLower().Replace(" ", "")}";
            var generatedPassword = GenerateRandomPassword();



            var newUser = new ApplicationUser
            {
                Email = generatedEmail,
                UserName = generatedEmail,
                EmailConfirmed = true,
                FirstName = hospital.HospitalName,
                LastName = $"{hospital.HospitalName}s",
                Role = "HospitalAdmin"  // Assign a role to the hospital admin
            };


            var result = await _userManager.CreateAsync(newUser, generatedPassword);


            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var newUserEntity = new User
            {
                Password = generatedPassword,
                Role = "HospitalAdmin",
                HospitalId = hospital.HospitalId,
                ModifiedDate = DateTime.UtcNow,
                UserName = newUser.FirstName,
                CreatedBy = newUser.Role,
            };

            _dbContext.Users.Add(newUserEntity);
            await _dbContext.SaveChangesAsync();

            // Step 6: Map the saved Clinic to ClinicDTO
            var savedHospital = _mapper.Map<HospitalDTO>(hospital);

            // Step 7: Return the saved clinic, email, and password for further processing
            return Ok(new { clinic = savedHospital, email = generatedEmail, password = generatedPassword });

        }


        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            // Check if the user has a specific role and set the Role claim accordingly
            string roleClaimValue = roles.Contains("HospitalAdmin") ? "HospitalAdmin" : "User";

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id),
        new Claim("Role", roleClaimValue) // Set the role claim based on the user's roles
    };

            // Add user claims if there are any
            claims.AddRange(userClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutees),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }













        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospital(int id, HospitalDTO hospitalDTO)
        {
            if (_dbContext.Hospitals == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.hospital' is null.");
            }


            var exsitingHospital = await _dbContext.Hospitals.FindAsync(id);

            if (exsitingHospital == null)
            {
                return NotFound();
            }


            _mapper.Map(hospitalDTO, exsitingHospital);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHospital(int id)
        {
            var hospital = await _dbContext.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            _dbContext.Hospitals.Remove(hospital);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }





        private string GenerateRandomPassword(int length = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            var random = new Random();
            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(result);
        }

    }
}













