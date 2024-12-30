using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.Models;

namespace WebApplication1.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public PatientController(PRMS_DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<ActionResult<List<patientDTO>>> GetPatient()
        {

            if (_dbContext.Pateients == null)
            {
                return NotFound();
            }

            var PatientDTO = _mapper.Map<List<patientDTO>>(_dbContext.Pateients);
            return Ok(PatientDTO);
        }


        [HttpGet("byPhoneNumber/{phoneNumber}")]
        public async Task<ActionResult<patientDTO>> GetPatient(string phoneNumber)
        {
            if (_dbContext.Pateients == null)
            {
                return NotFound();
            }

            var patient = await _dbContext.Pateients
                .FirstOrDefaultAsync(p => p.PatientPhoneNumeber == phoneNumber);

            if (patient == null)
            {
                return NotFound();
            }

            var patientDTO = _mapper.Map<patientDTO>(patient);
            return Ok(patientDTO);
        }



        [HttpPost]
        public async Task<ActionResult<patientDTO>> PostPatienr(patientDTO patientDTOs)
        {
            if (_dbContext.Pateients == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Pateients' is null.");
            }


            var patient = _mapper.Map<Pateient>(patientDTOs);


            _dbContext.Pateients.Add(patient);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<patientDTO>(patient);


            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, SavedDTO);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient(int id, patientDTO patientDTOs)
        {
            if (_dbContext.Pateients == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Pateients' is null.");
            }


            var exsitingPateients = await _dbContext.Pateients.FindAsync(id);

            if (exsitingPateients == null)
            {
                return NotFound();
            }


            _mapper.Map(patientDTOs, exsitingPateients);

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
        public async Task<IActionResult> DeletePateients(int id)
        {
            var Pateient = await _dbContext.Pateients.FindAsync(id);
            if (Pateient == null)
            {
                return NotFound();
            }
            _dbContext.Pateients.Remove(Pateient);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
