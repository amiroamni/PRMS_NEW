using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.DTO.CenteralCompany;
using PRMS_BackendAPI.Models;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CenteralCompanyController : ControllerBase
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public CenteralCompanyController(PRMS_DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        // GET: api/CenteralCompany
        [HttpGet]
        public async Task<ActionResult<List<CenteralCompanyDTO>>> GetCentralCompany()
        {
            if (_dbContext.CenteralCompanies == null)
            {
                return NotFound();
            }
            var centralCompanies = await _dbContext.CenteralCompanies.ToListAsync();
            var centralCompanyDTOs = _mapper.Map<List<CenteralCompanyDTO>>(centralCompanies);
            return Ok(centralCompanyDTOs);
        }

        [HttpGet("sp")]
        public async Task<ActionResult<List<CenteralCompanyNameDTO>>> GetClinicsName()
        {
            // Execute the stored procedure and retrieve the raw data from the database
            var Clinics = await _dbContext.CenteralCompanies
                .FromSqlRaw("USE [PRMS_Database]\r\n\r\nDECLARE\t@return_value int\r\n\r\nEXEC\t@return_value = [dbo].[spCenteralCompanyClinics]\r\n\r\n")
                .ToListAsync();

            // Map the result to the DTO
            var ClinicsDTOs = _mapper.Map<List<CenteralCompanyNameDTO>>(Clinics);

            return Ok(ClinicsDTOs);
        }


        [HttpGet("spclinicDetails")]
        public async Task<ActionResult<List<ClinicDetaialCenteralCompanyDTO>>> GetClinicsDetails()
        {

            var Clinics = await _dbContext.CenteralCompanies
                .FromSqlRaw("USE [PRMS_Database]\r\nDECLARE\t@return_value int\r\nEXEC\t@return_value = [dbo].[spCenteralCompanyClinicsDetail]\r\n")
                .ToListAsync();


            var ClinicDetaialCenteralCompanyDTOs = _mapper.Map<List<ClinicDetaialCenteralCompanyDTO>>(Clinics);
            return Ok(Clinics);
        }


        [HttpPost]
        public async Task<ActionResult<CenteralCompanyDTO>> PostCenteralCompany(CenteralCompanyDTO centeralCompanyDTO)
        {
            if (_dbContext.CenteralCompanies == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.CenteralCompanies' is null.");
            }


            var centeralCompany = _mapper.Map<CenteralCompany>(centeralCompanyDTO);


            _dbContext.CenteralCompanies.Add(centeralCompany);
            await _dbContext.SaveChangesAsync();

            var savedCenteralCompanyDTO = _mapper.Map<CenteralCompanyDTO>(centeralCompany);


            return CreatedAtAction(nameof(GetCentralCompany), new { id = centeralCompany.SystemAdminId }, savedCenteralCompanyDTO);
        }





        // PUT: api/CenteralCompany/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCenteralCompany(int id, CenteralCompanyDTO centeralCompanyDto)
        {
            if (id != centeralCompanyDto.SystemAdminId)
            {
                return BadRequest();
            }

            // Map DTO to entity
            var centeralCompany = new CenteralCompany
            {
                SystemAdminId = centeralCompanyDto.SystemAdminId,
                FirstName = centeralCompanyDto.FirstName,
                MiddleName = centeralCompanyDto.MiddleName,
                LastName = centeralCompanyDto.LastName,
                CreatedDate = centeralCompanyDto.CreatedDate,
                Phone = centeralCompanyDto.Phone,
                EmailAddress = centeralCompanyDto.EmailAddress,
                
                ClinicId = centeralCompanyDto.ClinicId,
                HospitalId = centeralCompanyDto.HospitalId
            };

            _dbContext.Entry(centeralCompany).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CenteralCompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }



        // DELETE: api/CenteralCompany/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCenteralCompany(int id)
        {
            if (_dbContext.CenteralCompanies == null)
            {
                return NotFound();
            }

            var centeralCompany = await _dbContext.CenteralCompanies.FindAsync(id);
            if (centeralCompany == null)
            {
                return NotFound();
            }

            _dbContext.CenteralCompanies.Remove(centeralCompany);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool CenteralCompanyExists(int id)
        {
            return _dbContext.CenteralCompanies?.Any(e => e.SystemAdminId == id) ?? false;
        }
    }

}
