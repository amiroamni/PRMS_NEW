namespace PRMS_BackendAPI.Identity.Identitys
{
    public class JwtSettings
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public double DurationInMinutees { get; set; }
    }
}
