using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Move_part_of_status_to_order_item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ShippedAt",
                table: "Invoices");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "InvoiceItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                table: "InvoiceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EstimatedDeliveryDate",
                table: "InvoiceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InPreparationAt",
                table: "InvoiceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "InTransitAt",
                table: "InvoiceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickedUpAt",
                table: "InvoiceItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InvoiceItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "DeliveredAt",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "EstimatedDeliveryDate",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "InPreparationAt",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "InTransitAt",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "PickedUpAt",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InvoiceItems");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveredAt",
                table: "Invoices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedAt",
                table: "Invoices",
                type: "datetime2",
                nullable: true);
        }
    }
}
