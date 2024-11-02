using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Ensure you use the correct EF Core namespace
using PRMS_BackendAPI.DTO;
using PRMS_BackendAPI.Models;


namespace WebApplication1.Controllers
{
    [Route("API/[Controller]")]
    [ApiController]
    public class ReferralController : ControllerBase
    {
        private readonly PRMS_DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public ReferralController(PRMS_DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<ActionResult<List<ReferralDTO>>> GETReferral()
        {

            if (_dbContext.Referrals == null)
            {
                return NotFound();
            }

            var referralDTO = _mapper.Map<List<ReferralDTO>>(_dbContext.Referrals);
            return Ok(referralDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ReferralDTO>> PostReferral(ReferralDTO referralDTO)
        {
            if (_dbContext.Referrals == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Referral' is null.");
            }


            var referrals = _mapper.Map<Referral>(referralDTO);


            _dbContext.Referrals.Add(referrals);
            await _dbContext.SaveChangesAsync();

            var SavedDTO = _mapper.Map<ReferralDTO>(referrals);


            return CreatedAtAction(nameof(GETReferral), new { id = referrals.ReferralId }, SavedDTO);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferral(int id, ReferralDTO referralDTO)
        {
            if (_dbContext.Referrals == null)
            {
                return Problem("Entity set 'PRMS_DatabaseContext.Referral' is null.");
            }


            var exsitingReferral = await _dbContext.Referrals.FindAsync(id);

            if (exsitingReferral == null)
            {
                return NotFound();
            }


            _mapper.Map(referralDTO, exsitingReferral);

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
        public async Task<IActionResult> DeleteReferrals(int id)
        {
            var referral = await _dbContext.Referrals.FindAsync(id);
            if (referral == null)
            {
                return NotFound();
            }
            _dbContext.Referrals.Remove(referral);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
