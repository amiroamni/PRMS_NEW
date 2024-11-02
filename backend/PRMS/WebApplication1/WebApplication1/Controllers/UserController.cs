using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure you use the correct EF Core namespace
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.Models;

namespace PRMS_BackendAPI.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly IMapper _mapper;
        public UserController(PRMS_DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }



        [HttpGet]

        public async Task<ActionResult<List<UserDTO>>> GetUser()
        {

            if (_dbContext.Users == null)
            {
                return NotFound();
            }
            var userss = _mapper.Map<List<UserDTO>>(_dbContext.Users );
            return Ok(userss);
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUsers(UserDTO userdto)
        {
            if (_dbContext.Users == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Specialization' is null.");
            }


            var users = _mapper.Map<User>(userdto);


            _dbContext.Users.Add(users);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<User>(userdto);


            return CreatedAtAction(nameof(GetUser), new { id = users.UserId }, SavedDTO);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, UserDTO userdto)
        {
            if (_dbContext.Users == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Specialization' is null.");
            }


            var exsitingUser = await _dbContext.Users.FindAsync(id);

            if (exsitingUser == null)
            {
                return NotFound();
            }


            _mapper.Map(userdto, exsitingUser);

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
        public async Task<IActionResult> DeleteUser(int id)
        {
            var Users = await _dbContext.Users.FindAsync(id);
            if (Users == null)
            {
                return NotFound();
            }
            _dbContext.Users.Remove(Users);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
