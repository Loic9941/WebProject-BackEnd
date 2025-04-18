using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InvoiceItemId",
                table: "Ratings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_InvoiceItemId",
                table: "Ratings",
                column: "InvoiceItemId",
                unique: true,
                filter: "[InvoiceItemId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_InvoiceItems_InvoiceItemId",
                table: "Ratings",
                column: "InvoiceItemId",
                principalTable: "InvoiceItems",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_InvoiceItems_InvoiceItemId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_InvoiceItemId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "InvoiceItemId",
                table: "Ratings");
        }
    }
}
