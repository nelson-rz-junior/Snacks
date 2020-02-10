using Microsoft.EntityFrameworkCore.Migrations;

namespace Snacks.Migrations
{
    public partial class SnackNameInBasketItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SnackName",
                table: "BasketItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SnackName",
                table: "BasketItems");
        }
    }
}
