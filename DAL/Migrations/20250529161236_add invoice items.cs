using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class addinvoiceitems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "CreatedAt", "DeliveryPartnerId", "PaidAt", "Status", "UserId" },
                values: new object[] { 1, new DateTime(2025, 5, 29, 14, 46, 41, 0, DateTimeKind.Unspecified), 4, new DateTime(2025, 5, 29, 14, 46, 41, 0, DateTimeKind.Unspecified), "Paid", 1 });

            migrationBuilder.InsertData(
                table: "InvoiceItems",
                columns: new[] { "Id", "CreatedAt", "DeliveredAt", "EstimatedDeliveryDate", "InTransitAt", "InvoiceId", "Name", "PickedUpAt", "ProductId", "Quantity", "ReadyToBePickedUp", "Status", "UnitPrice", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 29, 15, 40, 18, 0, DateTimeKind.Unspecified), null, null, null, 1, "Armoire à vin", null, 8, 1, null, "inPreparation", 800.00m, 1 },
                    { 2, new DateTime(2025, 5, 29, 15, 40, 28, 0, DateTimeKind.Unspecified), null, null, new DateTime(2025, 5, 29, 17, 41, 38, 0, DateTimeKind.Unspecified), 1, "Bord de mer", null, 2, 1, null, "readyToBePickedUp", 245.00m, 1 },
                    { 3, new DateTime(2025, 5, 29, 15, 40, 34, 0, DateTimeKind.Unspecified), null, new DateOnly(2025, 3, 8), null, 1, "Coucher de soleil sur le fleuve", new DateTime(2025, 5, 29, 17, 43, 3, 0, DateTimeKind.Unspecified), 1, 1, new DateTime(2025, 5, 29, 17, 41, 33, 0, DateTimeKind.Unspecified), "pickedUp", 500.00m, 1 },
                    { 4, new DateTime(2025, 5, 29, 15, 40, 39, 0, DateTimeKind.Unspecified), null, null, null, 1, "Meuble de salon", null, 7, 1, null, "inPreparation", 1200.00m, 1 },
                    { 5, new DateTime(2025, 5, 29, 15, 40, 44, 0, DateTimeKind.Unspecified), null, null, null, 1, "Meuble TV", null, 9, 1, null, "inPreparation", 900.00m, 1 },
                    { 6, new DateTime(2025, 5, 29, 15, 40, 48, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 29, 17, 42, 38, 0, DateTimeKind.Unspecified), new DateOnly(2025, 3, 6), new DateTime(2025, 5, 29, 17, 42, 34, 0, DateTimeKind.Unspecified), 1, "Peinture abstraite", new DateTime(2025, 5, 29, 17, 42, 24, 0, DateTimeKind.Unspecified), 3, 1, new DateTime(2025, 5, 29, 17, 41, 28, 0, DateTimeKind.Unspecified), "delivered", 300.00m, 1 },
                    { 7, new DateTime(2025, 5, 29, 15, 40, 53, 0, DateTimeKind.Unspecified), null, null, null, 1, "Pot antique", null, 5, 1, null, "inPreparation", 65.00m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Id", "CreatedAt", "InvoiceItemId", "Rate", "Text" },
                values: new object[] { 1, new DateTime(2025, 5, 29, 17, 43, 43, 0, DateTimeKind.Unspecified), 6, 5, "Très belle peinture, livraison rapide !" });

            migrationBuilder.InsertData(
                table: "Comment",
                columns: new[] { "Id", "CreatedAt", "RatingId", "Text" },
                values: new object[] { 1, new DateTime(2025, 5, 29, 17, 43, 43, 0, DateTimeKind.Unspecified), 1, "Merci pour votre commentaire positif !" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "InvoiceItems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
