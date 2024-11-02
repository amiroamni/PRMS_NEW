using Microsoft.AspNetCore.Identity;
using PRMS_BackendAPI.Models;

namespace PRMS_BackendAPI.Identity.Infra_Identitiy.Models
{
    public class ApplicationUser :  IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Role { get; set; }



    }
}
