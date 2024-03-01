using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotOrCold.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCopies_Carts_CartId",
                table: "DrinkCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId",
                table: "DrinkCopies");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Drinks");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "DrinkCopies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "DrinkCopies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCopies_Carts_CartId",
                table: "DrinkCopies",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId",
                table: "DrinkCopies",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "DrinkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCopies_Carts_CartId",
                table: "DrinkCopies");

            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId",
                table: "DrinkCopies");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Drinks");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Drinks",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "DrinkCopies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CartId",
                table: "DrinkCopies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCopies_Carts_CartId",
                table: "DrinkCopies",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId",
                table: "DrinkCopies",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "DrinkId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
