using Microsoft.CodeAnalysis.Editing;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PRMS_BackendAPI.Identity.Infra_Identitiy.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using PRMS_BackendAPI.Identity.Infra_Identitiy;

namespace PRMS_BackendAPI.Identity.Infra_Identitiy
{
    public class PRMS_IdentityDbContext : IdentityDbContext<ApplicationUser>
    {

        public PRMS_IdentityDbContext(DbContextOptions<PRMS_IdentityDbContext> options) : base(options) { 
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());


        }
    }
}
