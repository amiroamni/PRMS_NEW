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

            var doctordto = _mapper.Map<List<DoctorDTO>>(_dbContext.Doctors);
            return Ok(doctordto);
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

            var newUserEntity = new User
            {
                Password = generatedPassword,
                Role = "Doctor",
                ModifiedDate = DateTime.UtcNow,
                UserName = newUser.FirstName,
                CreatedBy = "superAdmin",

            };

            _dbContext.Users.Add(newUserEntity);
            await _dbContext.SaveChangesAsync();
           
            var doctorWithRelations = await _dbContext.Doctors
              .Include(d => d.Hospital)
             .Include(d => d.Clinic)
              .FirstOrDefaultAsync(d => d.DoctorId == doctor.DoctorId);
            
            var savedDoctorDTO = _mapper.Map<DoctorDTO>(doctor);

            // Step 7: Return the saved doctor, email, and password for further processing
            return Ok(new { doctor = doctorWithRelations, email = generatedEmail, password = generatedPassword });
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
