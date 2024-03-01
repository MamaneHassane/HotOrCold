using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotOrCold.Migrations
{
    /// <inheritdoc />
    public partial class m14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Drinks");

            migrationBuilder.AddColumn<int>(
                name: "Drinktype",
                table: "Drinks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Drinktype",
                table: "Drinks");

            migrationBuilder.AddColumn<int>(
                name: "Name",
                table: "Drinks",
                type: "int",
                maxLength: 11,
                nullable: false,
                defaultValue: 0);
        }
    }
}
