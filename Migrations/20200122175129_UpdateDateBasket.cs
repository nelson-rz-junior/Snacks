using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Snacks.Migrations
{
    public partial class UpdateDateBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Baskets",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "BasketItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "BasketItems");
        }
    }
}
