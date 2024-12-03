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
using System.Numerics;

namespace PRMS_BackendAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class DoctorController : ControllerBase
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly PRMS_IdentityDbContext _identityDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authenticationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;
 


        public DoctorController(PRMS_DatabaseContext dbContext,
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
        public async Task<ActionResult<List<DoctorDTO>>> GetDoctor()
        {
            if (_dbContext.Doctors == null)
            {
                return NotFound();
            }

            var doctors = await _dbContext.Doctors
                .Include(d => d.Hospital)
                .Include(d => d.Clinic)
                .ToListAsync();

            var doctorDTOs = doctors.Select(doctor => {
                var dto = _mapper.Map<DoctorDTO>(doctor);
                dto.HospitalName = doctor.Hospital?.HospitalName;
                dto.ClinicName = doctor.Clinic?.ClinicName;
                return dto;
            }).ToList();

            return Ok(doctorDTOs);
        }


        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> PostDoctor(DoctorDTO doctorDTO)
        {
            if (_dbContext.Doctors == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.clinicStaff' is null.");
            }


            var doctor = _mapper.Map<Doctor>(doctorDTO);


            _dbContext.Doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<DoctorDTO>(doctor);


            return CreatedAtAction(nameof(GetDoctor), new { id = doctor.DoctorId }, SavedDTO);
        }



        [HttpPost("withtoken")]
        public async Task<ActionResult<DoctorDTO>> PostDoctors(DoctorDTO doctorPostDto)
        {
            if (_dbContext.Doctors == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Doctors' is null.");
            }

            // Step 1: Map the Doctor DTO to the Doctor entity
            var doctor = _mapper.Map<Doctor>(doctorPostDto);

            // Step 2: Add Doctor to Database
            _dbContext.Doctors.Add(doctor);
            await _dbContext.SaveChangesAsync();

            // Step 3: Generate email and password for the doctor
            var generatedEmail = $"{doctor.DoctorEmail.ToLower().Replace(" ", "")}";
            var generatedPassword = GenerateRandomPassword();

            var newUser = new ApplicationUser
            {
                Email = generatedEmail,
                UserName = generatedEmail,
                EmailConfirmed = true,
                FirstName = doctor.DoctorFirstName,
                LastName = doctor.DoctorLastName,
                Role = "Doctor"  // Assign a role to the doctor
            };

            var result = await _userManager.CreateAsync(newUser, generatedPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var hospital = await _dbContext.Hospitals.FindAsync(doctor.HospitalId);
            var clinic = await _dbContext.Clinics.FindAsync(doctor.ClinicId);

            string hospitalName = null;
            string clinicName = null;

            if (hospital != null)
            {
                hospitalName = hospital.HospitalName;
            }

            if (clinic != null)
            {
                clinicName = clinic.ClinicName;
            }

            var newUserEntity = new User
            {
                Password = generatedPassword,
                Role = "Doctor",
                ModifiedDate = DateTime.UtcNow,
                UserName = newUser.FirstName,
                CreatedBy = "superAdmin",
                DoctorId= doctor.DoctorId,
                HospitalId = doctor.HospitalId,
                ClinicId = doctor.ClinicId,
                HopitalName = hospitalName,
                ClinicName = clinicName,

            };

            _dbContext.Users.Add(newUserEntity);
            await _dbContext.SaveChangesAsync();
           
            
            
            var savedDoctorDTO = _mapper.Map<DoctorDTO>(doctor);

            // Step 7: Return the saved doctor, email, and password for further processing
            return Ok(new { doctor = savedDoctorDTO, email = generatedEmail, password = generatedPassword, hospitalName , clinicName });
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> PutDoctor(int id, DoctorDTO doctorDTO)
        {
            if (_dbContext.Doctors == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.doc' is null.");
            }


            var exsitingDoctor = await _dbContext.Doctors.FindAsync(id);

            if (exsitingDoctor == null)
            {
                return NotFound();
            }


            _mapper.Map(doctorDTO, exsitingDoctor);

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
        public async Task<IActionResult> DeleteDoctor(int id)
        {



            var doctor = await _dbContext.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }


            _dbContext.Doctors.Remove(doctor);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }






        private string GenerateRandomPassword(int length = 12)
        {
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()";

            var random = new Random();
            var password = new StringBuilder();

            // Ensure at least one of each required character type
            password.Append(upperCase[random.Next(upperCase.Length)]);  // Upper case
            password.Append(lowerCase[random.Next(lowerCase.Length)]);  // Lower case
            password.Append(digits[random.Next(digits.Length)]);        // Digit
            password.Append(special[random.Next(special.Length)]);      // Special char

            // Fill the rest with random characters
            var allChars = upperCase + lowerCase + digits + special;
            for (int i = password.Length; i < length; i++)
            {
                password.Append(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the password
            return new string(password.ToString().ToCharArray().OrderBy(x => random.Next()).ToArray());
        }

    }
}
