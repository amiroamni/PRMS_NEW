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

    public class ClinicStaffController : ControllerBase
    {
       
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly PRMS_IdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;

        public ClinicStaffController(PRMS_DatabaseContext dbContext,
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
        public async Task<ActionResult<List<ClinicStaffDTO>>> GetClinicStaff()
        {
            if (_dbContext.ClinicStaffs == null)
            {
                return NotFound();
            }
            var clinicstaffdto = _mapper.Map<List<ClinicStaffDTO>>(_dbContext.ClinicStaffs);
            return Ok(clinicstaffdto);
        }

        [HttpPost]
        public async Task<ActionResult<ClinicStaffDTO>> PostClinics(ClinicStaffDTO clinicStaffDTO)
        {
            if (_dbContext.ClinicStaffs == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.clinicStaff' is null.");
            }


            var clincstaffs = _mapper.Map<ClinicStaff>(clinicStaffDTO);


            _dbContext.ClinicStaffs.Add(clincstaffs);
            await _dbContext.SaveChangesAsync();

            var savedClinicsDTO = _mapper.Map<ClinicStaffDTO>(clincstaffs);


            return CreatedAtAction(nameof(GetClinicStaff), new { id = clincstaffs.ClinicId }, savedClinicsDTO);
        }


        [HttpPost("withtoken")]
        public async Task<ActionResult<ClinicStaffDTO>> PutClinicstaff(ClinicStaffDTO clinicStaffDTO)
        {
            if (_dbContext.ClinicStaffs == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.ClinicStaff' is null.");
            }

            // Step 1: Map the Doctor DTO to the Doctor entity
            var clinic = _mapper.Map<ClinicStaff>(clinicStaffDTO);

            // Step 2: Add Doctor to Database
            _dbContext.ClinicStaffs.Add(clinic);
            await _dbContext.SaveChangesAsync();

            // Step 3: Generate email and password for the doctor
            var generatedEmail = $"{clinic.EmailAddress.ToLower().Replace(" ", "")}";
            var generatedPassword = GenerateRandomPassword();

            var newUser = new ApplicationUser
            {
                Email = generatedEmail,
                UserName = generatedEmail,
                EmailConfirmed = true,
                FirstName = clinic.FirstName,
                LastName = clinic.LastName,
                Role = "ClinicStaff"  // Assign a role to the doctor
            };

            var result = await _userManager.CreateAsync(newUser, generatedPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var newUserEntity = new User
            {
                Password = generatedPassword,
                Role = newUser.Role,
                ModifiedDate = DateTime.UtcNow,
                UserName = newUser.FirstName,
                CreatedBy = newUser.Role,
                ClinicId = clinic.ClinicId,
            };

            _dbContext.Users.Add(newUserEntity);
            await _dbContext.SaveChangesAsync();

            // Step 6: Map the saved Doctor to DoctorDTO
            var savedCSDTO = _mapper.Map<ClinicStaffDTO>(clinic);

            // Step 7: Return the saved doctor, email, and password for further processing
            return Ok(new { doctor = savedCSDTO, email = generatedEmail, password = generatedPassword });
        }








        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinicStaff(int id, ClinicStaffDTO clinicstaffDTO)
        {
            if (_dbContext.ClinicStaffs == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Appointments' is null.");
            }


            var existingClinicsStaffs = await _dbContext.ClinicStaffs.FindAsync(id);

            if (existingClinicsStaffs == null)
            {
                return NotFound();
            }


            _mapper.Map(clinicstaffDTO, existingClinicsStaffs);

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent(); // Return 204 on successful update
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinicStaffs(int id)
        {



            var clinicStaffs = await _dbContext.ClinicStaffs.FindAsync(id);

            if (clinicStaffs == null)
            {
                return NotFound();
            }


            _dbContext.ClinicStaffs.Remove(clinicStaffs);
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
