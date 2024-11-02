using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure you use the correct EF Core namespace
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.Models;


namespace PRMS_BackendAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class MedicalRecordController : ControllerBase
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public MedicalRecordController(PRMS_DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<ActionResult<List<MedicalRecordDTO>>> GetMedicalRecords()
        {

            if (_dbContext.MedicalRecords == null)
            {
                return NotFound();
            }

            var MEDICALRECORDS = _mapper.Map<List<MedicalRecordDTO>>(_dbContext.MedicalRecords);
            return Ok(MEDICALRECORDS);
        }

        [HttpPost]
        public async Task<ActionResult<MedicalRecordDTO>> PostMedicalRecord(MedicalRecordDTO medicalRecordDTO)
        {
            if (_dbContext.MedicalRecords == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.MedicalRecords' is null.");
            }


            var MEDICALRECORDS = _mapper.Map<MedicalRecord>(medicalRecordDTO);


            _dbContext.MedicalRecords.Add(MEDICALRECORDS);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<MedicalRecordDTO>(MEDICALRECORDS);


            return CreatedAtAction(nameof(GetMedicalRecords), new { id = MEDICALRECORDS.MedicalRecordId }, SavedDTO);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalRecord(int id, MedicalRecordDTO medicalRecordDTO)
        {
            if (_dbContext.MedicalRecords == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.MedicalRecords' is null.");
            }


            var exsitingMedicalRecords = await _dbContext.MedicalRecords.FindAsync(id);

            if (exsitingMedicalRecords == null)
            {
                return NotFound();
            }


            _mapper.Map(medicalRecordDTO, exsitingMedicalRecords);

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
        public async Task<IActionResult> DeleteMedicalRecords(int id)
        {
            var medicalrecords = await _dbContext.MedicalRecords.FindAsync(id);
            if (medicalrecords == null)
            {
                return NotFound();
            }
            _dbContext.MedicalRecords.Remove(medicalrecords);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
