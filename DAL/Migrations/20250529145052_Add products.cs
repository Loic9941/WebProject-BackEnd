using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Addproducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Available", "Category", "CreatedAt", "Description", "Image", "Name", "Price", "UserId" },
                values: new object[,]
                {
                    { 1, true, "Peinture", new DateTime(2025, 5, 29, 14, 46, 41, 0, DateTimeKind.Unspecified), "Peinture sur toile datant de 1920", "/images/491eea5e-ba08-46b5-8404-f73bce32f872_A1QdHDA29ML._AC_UF1000,1000_QL80_.jpg", "Coucher de soleil sur le fleuve", 500m, 6 },
                    { 2, true, "Peinture", new DateTime(2025, 5, 29, 16, 43, 43, 0, DateTimeKind.Unspecified), "Aquarelle datant de 1963", "/images/3bcd32f9-5452-427a-8219-2f863051a08f_images.jpg", "Bord de mer", 245m, 6 },
                    { 3, true, "Peinture", new DateTime(2025, 5, 29, 17, 43, 43, 0, DateTimeKind.Unspecified), "Peinture abstraite aux tonalités bleues inspirantes", "/images/8067df12-c43a-4ed6-92d1-c691d1be1954_peinture abstraite moderne bleue.jpg", "Peinture abstraite", 300m, 6 },
                    { 4, true, "Poterie", new DateTime(2025, 5, 29, 18, 43, 43, 0, DateTimeKind.Unspecified), "Pot en céramique émaillée noir artisanal. Parfait pour mettre vos plantest", "/images/393a53fa-20ae-4ff9-a781-83407db2b29b_pot-en-ceramique-emaillee-noir-o-39-x-31-cm.jpg", "Pot en céramique émaillée", 75m, 8 },
                    { 5, true, "Poterie", new DateTime(2025, 5, 29, 19, 43, 43, 0, DateTimeKind.Unspecified), "Pot en terre cuite avec motifs", "/images/bd71d69e-9734-4425-9fea-469dc5628a6c_images (1).jpg", "Pot antique", 65m, 8 },
                    { 6, true, "Sculpture", new DateTime(2025, 5, 29, 20, 43, 43, 0, DateTimeKind.Unspecified), "Pot de la couleur du soleil. Parfait pour apporter de la lumière dans votre maison", "/images/26d077dd-4efb-43e4-8741-c483e6111be6_images (2).jpg", "Potterie soleil", 50m, 8 },
                    { 7, true, "Menuiserie", new DateTime(2025, 5, 29, 21, 43, 43, 0, DateTimeKind.Unspecified), "Meuble de salon fait main en chêne véritable", "/images/edbb4f5f-f889-41d8-b394-22791ea8e198_110.jpg", "Meuble de salon", 1200m, 7 },
                    { 8, true, "Menuiserie", new DateTime(2025, 5, 29, 22, 43, 43, 0, DateTimeKind.Unspecified), "Armoire à vin en bois de cèdre assemblée à la main. Parfait pour stocker vos bouteilles.", "/images/61ddf700-3b6b-4771-bca3-ffbd415adf1a_550x366.jpg", "Armoire à vin", 800m, 7 },
                    { 9, true, "Menuiserie", new DateTime(2025, 5, 29, 23, 43, 43, 0, DateTimeKind.Unspecified), "Meuble TV en bois de placage de frene naturel. Teinte noisette", "/images/2abf68ee-d6a6-4789-b9a7-b28d319f453e_meuble-tv-en-bois-de-placage-de-frene-naturel-en-teinte-noisette-en-plusieurs-tailles.jpg", "Meuble TV", 900m, 7 },
                    { 10, true, "Sculpture", new DateTime(2025, 5, 30, 21, 43, 43, 0, DateTimeKind.Unspecified), "Sculpture ours polaire de 1m de hauteur", "/images/4011b3dc-8808-4f91-8ac7-20f1c6a9c410_images (3).jpg", "Sculpture ours polaire", 1500m, 7 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "Lastname",
                value: " ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6,
                column: "Lastname",
                value: "Bpost");
        }
    }
}
