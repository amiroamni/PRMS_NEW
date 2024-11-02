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


namespace PRMS_BackendAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class HospitalStaffController : ControllerBase
    {
       
            private readonly PRMS_DatabaseContext _dbContext;
            private readonly PRMS_IdentityDbContext _identityDbContext;
            private readonly IMapper _mapper;
            private readonly IAuthService _authenticationService;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly JwtSettings _jwtSettings;

            public HospitalStaffController (PRMS_DatabaseContext dbContext,
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

        public async Task<ActionResult<List<HospitalStaffDTO>>> GetHospitalStaff()
        {

            if (_dbContext.HospitalStaffs == null)
            {
                return NotFound();
            }

            var hospitalstaffdto = _mapper.Map<List<HospitalStaffDTO>>(_dbContext.HospitalStaffs);
            return Ok(hospitalstaffdto);
        }

        [HttpPost]
        public async Task<ActionResult<HospitalStaffDTO>> PostDoctor(HospitalStaffDTO hospitalstaffdto)
        {
            if (_dbContext.HospitalStaffs == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.hospitalsstaff' is null.");
            }


            var hospitalstaff = _mapper.Map<HospitalStaff>(hospitalstaffdto);


            _dbContext.HospitalStaffs.Add(hospitalstaff);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<HospitalStaffDTO>(hospitalstaff);


            return CreatedAtAction(nameof(GetHospitalStaff), new { id = hospitalstaff.HospitalStaffId }, SavedDTO);
        }


        [HttpPost("withtoken")]
        public async Task<ActionResult<HospitalStaffDTO>> PutClinicstaff(HospitalStaffDTO hopitalStaffDTO)
        {
            if (_dbContext.HospitalStaffs == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.HospitalStaff' is null.");
            }

            // Step 1: Map the Doctor DTO to the Doctor entity
            var hospitalstaff = _mapper.Map<HospitalStaff>(hopitalStaffDTO);

            // Step 2: Add Doctor to Database
            _dbContext.HospitalStaffs.Add(hospitalstaff);
            await _dbContext.SaveChangesAsync();

            // Step 3: Generate email and password for the doctor
            var generatedEmail = $"{hospitalstaff.EmailAddress.ToLower().Replace(" ", "")}";
            var generatedPassword = GenerateRandomPassword();

            var newUser = new ApplicationUser
            {
                Email = generatedEmail,
                UserName = generatedEmail,
                EmailConfirmed = true,
                FirstName = hospitalstaff.FirstName,
                LastName = hospitalstaff.LastName,
                Role = "Hopitalstaff" 
                // Assign a role to the doctor
            };

            var result = await _userManager.CreateAsync(newUser, generatedPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var newUserEntity = new User
            {
                Password = generatedPassword,
                Role = "HospitalStaff",
                ModifiedDate = DateTime.UtcNow,
                UserName = newUser.FirstName,
                CreatedBy = newUser.Role,
                HospitalId = hospitalstaff.HospitalId,
            };

            _dbContext.Users.Add(newUserEntity);
            await _dbContext.SaveChangesAsync();

            // Step 6: Map the saved Doctor to DoctorDTO
            var savedCSDTO = _mapper.Map<HospitalStaffDTO>(hospitalstaff);

            // Step 7: Return the saved doctor, email, and password for further processing
            return Ok(new { doctor = savedCSDTO, email = generatedEmail, password = generatedPassword });
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutHospitalstaff(int id, HospitalStaffDTO hospitalstaffDTO)
        {
            if (_dbContext.HospitalStaffs == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.HospitalStaff' is null.");
            }


            var exsitingHospitalStaff = await _dbContext.HospitalStaffs.FindAsync(id);

            if (exsitingHospitalStaff == null)
            {
                return NotFound();
            }


            _mapper.Map(hospitalstaffDTO, exsitingHospitalStaff);

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
        public async Task<IActionResult> DeleteHospitalstaff(int id)
        {
            var hospitalstaff = await _dbContext.HospitalStaffs.FindAsync(id);
            if (hospitalstaff == null)
            {
                return NotFound();
            }
            _dbContext.HospitalStaffs.Remove(hospitalstaff);
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