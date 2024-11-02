using PRMS_BackendAPI.Identity.Identitys;

namespace PRMS_BackendAPI.Identity.IdentityInterface
{
    public interface IAuthService
    {
        Task<AuthResponse> Login(AuthRequest request);

        Task<RegistrationResponse> Register(RegistraionRequest request);
    }
}
