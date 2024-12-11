using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PRMS_BackendAPI.Identity.IdentityInterface;
using PRMS_BackendAPI.Identity.Identitys;

namespace PRMS_BackendAPI.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        public AccountController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody]AuthRequest request)
        {
            var response = await _authenticationService.Login(request);
            if (response == null) // Handle login failure
            {
                return Unauthorized();
            }
            return Ok(response);


        }


        [HttpPost("register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistraionRequest request)
        {
            return Ok(await _authenticationService.Register(request));
        }




    }
}

