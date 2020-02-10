using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Snacks.Migrations
{
    public partial class BasketIdOrderItemGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                table: "OrderItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "OrderItems");
        }
    }
}
