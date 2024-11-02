using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure you use the correct EF Core namespace
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.Models;

namespace PRMS_BackendAPI.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class SpecializationController : Controller
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public SpecializationController(PRMS_DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]

        [HttpGet]

        public async Task<ActionResult<List<SpecializationDTO>>> Getspecialization()
        {

            if (_dbContext.Specializations == null)
            {
                return NotFound();
            }

            var specailzation = _mapper.Map<List<SpecializationDTO>>(_dbContext.Specializations);
            return Ok(specailzation);
        }

        [HttpPost]
        public async Task<ActionResult<SpecializationDTO>> PostSpecialization(SpecializationDTO specializationDTO)
        {
            if (_dbContext.Specializations == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Specialization' is null.");
            }


            var specialization = _mapper.Map<Specialization>(specializationDTO);


            _dbContext.Specializations.Add(specialization);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<Specialization>(specialization);


            return CreatedAtAction(nameof(Getspecialization), new { id = specialization.SpecializationId }, SavedDTO);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialization(int id, SpecializationDTO specializationDTO)
        {
            if (_dbContext.Specializations == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Specialization' is null.");
            }


            var exsitingSpecialization = await _dbContext.Specializations.FindAsync(id);

            if (exsitingSpecialization == null)
            {
                return NotFound();
            }


            _mapper.Map(specializationDTO, exsitingSpecialization);

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
        public async Task<IActionResult> DeleteSpecialization(int id)
        {
            var specialization = await _dbContext.Specializations.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }
            _dbContext.Specializations.Remove(specialization);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
