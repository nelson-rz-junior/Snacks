using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Snacks.Migrations
{
    public partial class BasketItemIdOrderItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasketItemId",
                table: "OrderItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketItemId",
                table: "OrderItems");

            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
