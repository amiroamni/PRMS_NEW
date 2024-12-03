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
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class ClinicController : ControllerBase
    {

        private readonly PRMS_DatabaseContext _dbContext;
        private readonly PRMS_IdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public ClinicController(PRMS_DatabaseContext dbContext,
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


        // GET: api/centeral
        [HttpGet]
        public async Task<ActionResult<List<ClinicDTO>>> GetClinics()
        {
            if (_dbContext.Clinics == null)
            {
                return NotFound();
            }
            var clinicsDto = _mapper.Map<List<ClinicDTO>>(_dbContext.Clinics);
            return Ok(clinicsDto);
        }



        [HttpPost]
        public async Task<ActionResult<ClinicDTO>> PostClinics(ClinicDTO clinicDTO)
        {
            if (_dbContext.Clinics == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Cinincs' is null.");
            }


            var clincs = _mapper.Map<Clinic>(clinicDTO);


            _dbContext.Clinics.Add(clincs);
            await _dbContext.SaveChangesAsync();

            var savedClinicsDTO = _mapper.Map<ClinicDTO>(clincs);


            return CreatedAtAction(nameof(GetClinics), new { id = clincs.ClinicId }, savedClinicsDTO);
        }




        [HttpPost("withtoken")]
        public async Task<ActionResult<ClinicDTO>> PostClinic(ClinicDTO clinicDTO)
        {
            if (_dbContext.Clinics == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Clinics' is null.");
            }

            // Step 1: Map the Hospital DTO to the Hospital entity
            var clinic = _mapper.Map<Clinic>(clinicDTO);

            // Step 2: Add Hospital to Database
            _dbContext.Clinics.Add(clinic);
            await _dbContext.SaveChangesAsync();

            // Step 3: Generate email and password for the hospital
            var generatedEmail = $"{clinic.ClinicEmail.ToLower().Replace(" ", "")}";
            var generatedPassword = GenerateRandomPassword();



            var newUser = new ApplicationUser
            {
                Email = generatedEmail,
                UserName = generatedEmail,
                EmailConfirmed = true,
                FirstName = clinic.ClinicName,
                LastName = $"{clinic.ClinicName}s",
                Role = "ClinicAdmin"  // Assign a role to the hospital admin
            };


            var result = await _userManager.CreateAsync(newUser, generatedPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var newUserEntity = new User
            {
                Password = generatedPassword,
                Role = "ClinicAdmin",
                ClinicId = clinic.ClinicId, 
                ModifiedDate = DateTime.UtcNow,
                UserName = newUser.FirstName,
                CreatedBy = newUser.Role,
                ClinicName = clinic.ClinicName
            };

            _dbContext.Users.Add(newUserEntity);
            await _dbContext.SaveChangesAsync();

            // Step 6: Map the saved Clinic to ClinicDTO
            var savedClinicDTO = _mapper.Map<ClinicDTO>(clinic);

            // Step 7: Return the saved clinic, email, and password for further processing
            return Ok(new { clinic = savedClinicDTO, email = generatedEmail, password = generatedPassword });
        }



private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        // Fetch the user's role directly from AspNetUsers
        var userRole = await _identityDbContext.Users
            .Where(u => u.Id == user.Id)
            .Select(u => u.Role)
            .FirstOrDefaultAsync();

        if (string.IsNullOrEmpty(userRole))
        {
            throw new Exception("Role not found for the specified user.");
        }

        // Fetch the role ID from AspNetRoles based on role name
        var roleId = await _identityDbContext.Roles
            .Where(r => r.NormalizedName == userRole.ToUpper())
            .Select(r => r.Id)
            .FirstOrDefaultAsync();

        // Fetch associated claims from AspNetRoleClaims for the user's role
        var roleClaimsFromDb = await _identityDbContext.RoleClaims
            .Where(rc => rc.RoleId == roleId)
            .Select(rc => new Claim(rc.ClaimType, rc.ClaimValue))
            .ToListAsync();

        // Basic user claims
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id),
        new Claim("Role", userRole) // Adding role name directly
    };

        // Add the user's role claims to the token claims
        claims.AddRange(roleClaimsFromDb);

        // Retrieve any additional claims for the user
        var userClaims = await _userManager.GetClaimsAsync(user);
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
        public async Task<IActionResult> PutClincs(int id, ClinicDTO clinicDTO)
        {
            if (_dbContext.Clinics == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Appointments' is null.");
            }

            // Find the existing appointment by id
            var existingClinics = await _dbContext.Clinics.FindAsync(id);

            if (existingClinics == null)
            {
                return NotFound();
            }


            _mapper.Map(clinicDTO, existingClinics);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

                if (!ClinicsExisting(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Return 204 on successful update
        }

        private bool ClinicsExisting(int id)
        {
            return _dbContext.Clinics.Any(e => e.ClinicId == id);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinic(int id)
        {



            var clinics = await _dbContext.Clinics.FindAsync(id);

            if (clinics == null)
            {
                return NotFound();
            }


            _dbContext.Clinics.Remove(clinics);
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
