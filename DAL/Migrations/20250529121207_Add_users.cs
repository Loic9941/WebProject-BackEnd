using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Add_users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Firstname", "Lastname", "PasswordHash", "Role", "Salt" },
                values: new object[,]
                {
                    { 1, "client1@test.be", "Bernard", "Lacheteur", "D6B1EEBBA6E4556DA0E2", "Customer", "Thursday" },
                    { 2, "client2@test.be", "Fred", "Duclient", "D6B1EEBBA6E4556DA0E2", "Customer", "Thursday" },
                    { 3, "admin@test.be", "Gérard", "Ladmin", "D6B1EEBBA6E4556DA0E2", "Administrator", "Thursday" },
                    { 4, "dhl@test.be", "DHL", "DHL", "D6B1EEBBA6E4556DA0E2", "DeliveryPartner", "Thursday" },
                    { 5, "bpost@test.be", "Bpost", "Bpost", "D6B1EEBBA6E4556DA0E2", "DeliveryPartner", "Thursday" },
                    { 6, "artiste1@test.be", "Rodin", "Bpost", "D6B1EEBBA6E4556DA0E2", "Artisan", "Thursday" },
                    { 7, "artiste2@test.be", "Leonard", "De vinci", "D6B1EEBBA6E4556DA0E2", "Artisan", "Thursday" },
                    { 8, "artiste3@test.be", "Salvador", "Dali", "D6B1EEBBA6E4556DA0E2", "Artisan", "Thursday" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
