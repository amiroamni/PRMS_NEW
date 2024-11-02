using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRMS_BackendAPI.Migrations
{
    public partial class secoundIdentitySetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cac43a6e-f7bb-4448-baaf-1add432ffbbf", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ccbbf",
                column: "ConcurrencyStamp",
                value: "76ccb35d-20c0-40c4-ba78-6c9e6be1a362");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ffbbf",
                column: "ConcurrencyStamp",
                value: "5246f41d-ae3f-4ff5-bb5f-34215b539e5d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cac43a6e-f7bb-4448-baaf-1add432eebbf", "13bc58a1-9505-4de1-bea5-843287926f48", "HospitalAdmin", "HOSPITALADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cac43a6e-f7bb-4448-baaf-1add432ffbbf", "9e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "ccf0ee24-2229-4168-9e4a-a9fb0c48368e", "AQAAAAEAACcQAAAAEBNwUhT/wzgpfovcppRddGFwn10TTK75hGF5yBj85wtuV1AbspXeTgnKZEaIsjPwcQ==", "Adminstrator", "218e0f50-8741-49a7-8a2a-35b04ac50d39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "Role", "SecurityStamp" },
                values: new object[] { "e3b04eef-9875-4a36-a846-5a345568561a", "AQAAAAEAACcQAAAAEOzN1E/iuChSAj9zxeCoCeAsvPGbPDixGCJxtbIid+VnSpa68Dy0V0hUW/p84MI7Fg==", "User", "c47ae4f7-aa76-4820-9699-b10181f003e7" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7e445865-a24d-4543-a6c6-9443d048cdb9", 0, "cfc5672a-3ba6-4f62-8ed4-ec19a31b24b2", "hospitaladmin@localhost.com", true, "System", "HospitalAdmin", false, null, "HOSPITALADMIN@LOCALHOST.COM", "HOSPITALAdmin@LOCALHOST.COM", "AQAAAAEAACcQAAAAEIy7e+pt8ul3SZC4OhbX0fZ8Rp0iW7PxF86JLrbBg4kyMJIkdQc1AcJRGCvFVADYBg==", null, false, "HospitalAdmin", "4f83a544-214c-4c08-8958-bcd0c8ed92ce", false, "hopitaladmin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cac43a6e-f7bb-4448-baaf-1add432eebbf", "7e445865-a24d-4543-a6c6-9443d048cdb9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cac43a6e-f7bb-4448-baaf-1add432eebbf", "7e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cac43a6e-f7bb-4448-baaf-1add432ffbbf", "9e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432eebbf");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e445865-a24d-4543-a6c6-9443d048cdb9");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ccbbf",
                column: "ConcurrencyStamp",
                value: "a75f1728-6c22-4ad0-b305-87b3ba1d3f3f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ffbbf",
                column: "ConcurrencyStamp",
                value: "733a919d-5d0a-4d24-8dc7-2926c15d880a");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cac43a6e-f7bb-4448-baaf-1add432ffbbf", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab771c61-2a17-4343-ba71-2bab22d99688", "AQAAAAEAACcQAAAAEEoz40LgSDtSf5se1GVLPpaL4vIRGmWJ8/Jn3AvUCHcAe2DGfrB8geb8pL31AmTg5w==", "d038a6b8-f087-4e20-a388-1817449656d7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b5a75e3-1589-4458-b5cc-527a4f6a38c1", "AQAAAAEAACcQAAAAEBfV7IaZ3+9c+4wkfwuWlxxdtCObdCTo4bJQFuyvOiDX1iSGN3VsfIHjCJHI6luYDA==", "dfe9bb60-9992-4b65-8b72-a6afe0916048" });
        }
    }
}
