using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure you use the correct EF Core namespace
using PRMS_BackendAPI.DTO.AppointmentDTOs;
using PRMS_BackendAPI.Models;
using PRMS_BackendAPI.DTO;
using Microsoft.AspNetCore.Authorization;


namespace PRMS_BackendAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public AppointmentController(PRMS_DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        // GET: api/centeral
        [HttpGet]
        public async Task<ActionResult<List<AppointmentDTO>>> GetAppointment()
        {

            if (_dbContext.Appointments == null)
            {
                return NotFound(); // Return 404 if no table exists
            }


            var AppointmentsDTO = _mapper.Map<List<AppointmentDTO>>(_dbContext.Appointments);

            // Return the list wrapped in an ActionResult
            return Ok(AppointmentsDTO);
        }

        [HttpGet("sp")]
        public async Task<ActionResult<List<AppointmentDTODetails>>> GetAppointmentDetail()
        {
            // Execute the stored procedure and retrieve the raw data from the database
            var appointments = await _dbContext.Appointments
                .FromSqlRaw("DECLARE\t@return_value int\r\n\r\nEXEC\t@return_value = [dbo].[GetspAppointmentDetails]\r\n\r\n")
                .ToListAsync();

            // Map the result to the DTO
            var appointmentDTOs = _mapper.Map<List<AppointmentDTODetails>>(appointments);

            return Ok(appointmentDTOs);
        }



        [HttpPost]
        public async Task<ActionResult<AppointmentDTO>> PostAppointments(AppointmentDTO appointmentDTO)
        {
            if (_dbContext.Appointments == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Appointmetns' is null.");
            }


            var appointment = _mapper.Map<Appointment>(appointmentDTO);


            _dbContext.Appointments.Add(appointment);
            await _dbContext.SaveChangesAsync();
            var savedAppointmentDTO = _mapper.Map<AppointmentDTO>(appointment);


            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId }, savedAppointmentDTO);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppointment(int id, AppointmentDTO appointmentDTO)
        {
            if (_dbContext.Appointments == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Appointments' is null.");
            }

            // Find the existing appointment by id
            var existingAppointment = await _dbContext.Appointments.FindAsync(id);

            if (existingAppointment == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Map the updated fields from DTO to the existing entity
            _mapper.Map(appointmentDTO, existingAppointment);

            try
            {
                await _dbContext.SaveChangesAsync(); // Save the changes
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency issues if needed, or check if the record still exists
                if (!AppointmentExists(id))
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

        private bool AppointmentExists(int id)
        {
            return _dbContext.Appointments.Any(e => e.AppointmentId == id);
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (_dbContext.Appointments == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Appointments' is null.");
            }

            // Find the existing appointment by id
            var appointment = await _dbContext.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound(); // Return 404 if not found
            }

            // Remove the appointment
            _dbContext.Appointments.Remove(appointment);
            await _dbContext.SaveChangesAsync(); // Save the changes

            return NoContent(); // Return 204 on successful deletion
        }



    }
}
