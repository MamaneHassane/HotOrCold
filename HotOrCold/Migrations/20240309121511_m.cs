using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotOrCold.Migrations
{
    /// <inheritdoc />
    public partial class m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId1",
                table: "DrinkCopies");

            migrationBuilder.DropIndex(
                name: "IX_DrinkCopies_DrinkId1",
                table: "DrinkCopies");

            migrationBuilder.DropColumn(
                name: "DrinkId1",
                table: "DrinkCopies");

            migrationBuilder.AddColumn<int>(
                name: "DrinkId",
                table: "DrinkCopies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DrinkCopies_DrinkId",
                table: "DrinkCopies",
                column: "DrinkId");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId",
                table: "DrinkCopies",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "DrinkId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId",
                table: "DrinkCopies");

            migrationBuilder.DropIndex(
                name: "IX_DrinkCopies_DrinkId",
                table: "DrinkCopies");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "DrinkCopies");

            migrationBuilder.AddColumn<int>(
                name: "DrinkId1",
                table: "DrinkCopies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DrinkCopies_DrinkId1",
                table: "DrinkCopies",
                column: "DrinkId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DrinkCopies_Drinks_DrinkId1",
                table: "DrinkCopies",
                column: "DrinkId1",
                principalTable: "Drinks",
                principalColumn: "DrinkId");
        }
    }
}
