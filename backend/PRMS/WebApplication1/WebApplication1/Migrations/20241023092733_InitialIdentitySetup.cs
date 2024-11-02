using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRMS_BackendAPI.Migrations
{
    public partial class InitialIdentitySetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ccbbf",
                column: "ConcurrencyStamp",
                value: "c7527263-4878-4a6c-b7a5-2060f8c95e52");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add432ffbbf",
                column: "ConcurrencyStamp",
                value: "eec23a61-4112-44a0-91c6-fe6d6ea1fd4b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce3754cd-5f73-4f7b-a529-d3081ea98e38", "AQAAAAEAACcQAAAAEMWHuIxUBXI9BTciVf6yfXNnx4/hk1Pz430UoHPqETv+8o1w2bXrE3DkvTyLvjVnNg==", "dbbc4900-362f-4665-b81b-e23c0b2bb892" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a79f57ba-7565-403d-a0e8-cfa53542e8ac", "AQAAAAEAACcQAAAAEIFN3yFZyBm1dFCshAkOmppcFsm/P7vhWxAdaBjJP8dNnHtlqqA2JfRfxgkgK3/DoA==", "1cb3ed03-6337-4a33-9de3-bb9abc53ada9" });
        }
    }
}
