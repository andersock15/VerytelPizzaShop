using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerytelPizzaShop.Migrations
{
    /// <inheritdoc />
    public partial class AddCreationDateAndStatusToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id_order");

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_date",
                table: "orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "current_timestamp");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "orders",
                type: "text",
                nullable: false,
                defaultValue: "order placed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creation_date",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "status",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "id_order",
                table: "orders",
                newName: "Id");
        }
    }
}
