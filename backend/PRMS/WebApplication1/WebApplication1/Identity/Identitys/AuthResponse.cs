namespace PRMS_BackendAPI.Identity.Identitys
{
    public class AuthResponse
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public IList<string> Role { get; set; }
    }
}
