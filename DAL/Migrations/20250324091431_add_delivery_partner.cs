using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_delivery_partner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryPartnerId",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DeliveryPartnerId",
                table: "Invoices",
                column: "DeliveryPartnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_DeliveryPartnerId",
                table: "Invoices",
                column: "DeliveryPartnerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_DeliveryPartnerId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_DeliveryPartnerId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DeliveryPartnerId",
                table: "Invoices");
        }
    }
}
