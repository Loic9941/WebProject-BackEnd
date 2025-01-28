using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCascadeDel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Contacts_AuthorId",
                table: "Ratings");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Ratings",
                newName: "ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_AuthorId",
                table: "Ratings",
                newName: "IX_Ratings_ContactId");

            migrationBuilder.AddColumn<int>(
                name: "ContactId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ContactId",
                table: "Products",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Contacts_ContactId",
                table: "Products",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Contacts_ContactId",
                table: "Ratings",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Contacts_ContactId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Contacts_ContactId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Products_ContactId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "Ratings",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_ContactId",
                table: "Ratings",
                newName: "IX_Ratings_AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Contacts_AuthorId",
                table: "Ratings",
                column: "AuthorId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
