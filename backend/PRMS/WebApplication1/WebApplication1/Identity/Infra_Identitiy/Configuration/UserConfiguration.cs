using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PRMS_BackendAPI.Identity.Infra_Identitiy.Models;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(
            new ApplicationUser
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // Admin User ID
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "System",
                LastName = "Admin",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "p@ssword1"),
                EmailConfirmed = true,
                Role = "Adminstrator"
            },
            new ApplicationUser
            {
                Id = "9e445865-a24d-4543-a6c6-9443d048cdb9", // User User ID
                Email = "user@localhost.com",
                NormalizedEmail = "USER@LOCALHOST.COM",
                FirstName = "System",
                LastName = "User",
                UserName = "user@localhost.com",
                NormalizedUserName = "USER@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "p@ssword1"),
                EmailConfirmed = true,
                Role = "User"
            },
               new ApplicationUser
               {
                   Id = "7e445865-a24d-4543-a6c6-9443d048cdb9", // User User ID
                   Email = "hospitaladmin@localhost.com",
                   NormalizedEmail = "HOSPITALADMIN@LOCALHOST.COM",
                   FirstName = "System",
                   LastName = "HospitalAdmin",
                   UserName = "hopitaladmin@localhost.com",
                   NormalizedUserName = "HOSPITALAdmin@LOCALHOST.COM",
                   PasswordHash = hasher.HashPassword(null, "p@ssword2"),
                   EmailConfirmed = true,
                   Role = "HospitalAdmin"
               }
        );
    }
}
