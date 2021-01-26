using Microsoft.EntityFrameworkCore.Migrations;

namespace SBAdmin.Web.Migrations
{
    /// <summary>
    /// Class SeedRoles
    /// </summary>
    public partial class SeedRoles : Migration
    {
        /// <summary>
        /// Method Up
        /// </summary>
        /// <param name="migrationBuilder">migrationBuilder</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "716289aa-98e0-4355-ad28-5b2a44409523", "6bf8bf61-b168-4d7a-b5ec-920ef4e87959", "Admin", "ADMIN" },
                    { "c45f45b0-8a11-416f-a136-b372d4b366b0", "37dbec96-8489-480d-9bb0-68b7491a810c", "User", "USER" }
                });
        }

        /// <summary>
        /// Down
        /// </summary>
        /// <param name="migrationBuilder">migrationBuilder</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "716289aa-98e0-4355-ad28-5b2a44409523");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c45f45b0-8a11-416f-a136-b372d4b366b0");
        }
    }
}
