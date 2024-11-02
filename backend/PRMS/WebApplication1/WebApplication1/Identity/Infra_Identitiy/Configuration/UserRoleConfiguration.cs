using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(
            new IdentityUserRole<string>
            {
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9", // Admin User ID
                RoleId = "cac43a6e-f7bb-4448-baaf-1add432ccbbf"  // Administrator Role ID
            },
            new IdentityUserRole<string>
            {
                UserId = "9e445865-a24d-4543-a6c6-9443d048cdb9", // User User ID
                RoleId = "cac43a6e-f7bb-4448-baaf-1add432ffbbf"  // User Role ID
            },
             new IdentityUserRole<string>
             {
                 UserId = "7e445865-a24d-4543-a6c6-9443d048cdb9", 
                 RoleId = "cac43a6e-f7bb-4448-baaf-1add432eebbf"  
             }
        );
    }
}
