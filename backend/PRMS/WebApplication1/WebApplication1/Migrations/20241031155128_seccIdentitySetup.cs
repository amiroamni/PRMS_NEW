using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRMS_BackendAPI.Migrations
{
    public partial class seccIdentitySetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ccbbf",
                column: "ConcurrencyStamp",
                value: "997bf338-cb6d-4eff-827a-501d4d6eaf94");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432eebbf",
                column: "ConcurrencyStamp",
                value: "49bddd7c-42f9-4cd5-afe9-323ad48c64fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ffbbf",
                column: "ConcurrencyStamp",
                value: "110a388c-9bd7-49ba-b5ad-b4e19d2915a8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3c3c18cb-074b-49ff-ae05-45c4a1779155", "AQAAAAEAACcQAAAAEFM7u3k9+SjMmEMCPgLcTsvhn62zhCJCFzLFEfdi5sF5onkVJMylZUVjSy4B39cVRQ==", "690dfeac-0821-4984-9895-df6b008d1304" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a0980546-6a71-4d73-a581-5e87b19f8164", "AQAAAAEAACcQAAAAEGGbWPPSzQbQyClS/lYmlTJ3DePrOP7ji+XFA5PlKcICxIEFe6Js87TBuXJprQygjQ==", "06d27935-d509-4029-acf3-5653c93c79cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "89f60405-ee60-4bec-9379-d3bae0240923", "AQAAAAEAACcQAAAAENqciwI4wBfYIj4oBiZISm3pkfIqhuzWPdbR+u7dI8dnCa23EhMBQG4KlBWCBGuJ2A==", "5e79248a-e829-4727-a6fd-f23aa8779084" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ccbbf",
                column: "ConcurrencyStamp",
                value: "76ccb35d-20c0-40c4-ba78-6c9e6be1a362");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432eebbf",
                column: "ConcurrencyStamp",
                value: "13bc58a1-9505-4de1-bea5-843287926f48");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ffbbf",
                column: "ConcurrencyStamp",
                value: "5246f41d-ae3f-4ff5-bb5f-34215b539e5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfc5672a-3ba6-4f62-8ed4-ec19a31b24b2", "AQAAAAEAACcQAAAAEIy7e+pt8ul3SZC4OhbX0fZ8Rp0iW7PxF86JLrbBg4kyMJIkdQc1AcJRGCvFVADYBg==", "4f83a544-214c-4c08-8958-bcd0c8ed92ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ccf0ee24-2229-4168-9e4a-a9fb0c48368e", "AQAAAAEAACcQAAAAEBNwUhT/wzgpfovcppRddGFwn10TTK75hGF5yBj85wtuV1AbspXeTgnKZEaIsjPwcQ==", "218e0f50-8741-49a7-8a2a-35b04ac50d39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3b04eef-9875-4a36-a846-5a345568561a", "AQAAAAEAACcQAAAAEOzN1E/iuChSAj9zxeCoCeAsvPGbPDixGCJxtbIid+VnSpa68Dy0V0hUW/p84MI7Fg==", "c47ae4f7-aa76-4820-9699-b10181f003e7" });
        }
    }
}
