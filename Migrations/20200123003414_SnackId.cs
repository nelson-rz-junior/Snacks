using Microsoft.EntityFrameworkCore.Migrations;

namespace Snacks.Migrations
{
    public partial class SnackId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Snacks_SnackId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<int>(
                name: "SnackId",
                table: "BasketItems",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Snacks_SnackId",
                table: "BasketItems",
                column: "SnackId",
                principalTable: "Snacks",
                principalColumn: "SnackId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_Snacks_SnackId",
                table: "BasketItems");

            migrationBuilder.AlterColumn<int>(
                name: "SnackId",
                table: "BasketItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_Snacks_SnackId",
                table: "BasketItems",
                column: "SnackId",
                principalTable: "Snacks",
                principalColumn: "SnackId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
