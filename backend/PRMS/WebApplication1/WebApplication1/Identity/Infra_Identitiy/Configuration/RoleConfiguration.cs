using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "cac43a6e-f7bb-4448-baaf-1add432ccbbf", // Administrator Role ID
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                
            },
            new IdentityRole
            {
                Id = "cac43a6e-f7bb-4448-baaf-1add432ffbbf", // User Role ID
                Name = "User",
                NormalizedName = "USER"
            },
            new IdentityRole
            {
                Id = "cac43a6e-f7bb-4448-baaf-1add432eebbf", // User Role ID
                Name = "HospitalAdmin",
                NormalizedName = "HOSPITALADMIN"
            }
        );
    }
}
