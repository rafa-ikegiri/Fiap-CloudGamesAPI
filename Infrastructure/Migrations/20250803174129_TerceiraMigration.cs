using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TerceiraMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Usuario",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bool");

            migrationBuilder.AlterColumn<bool>(
                name: "Multiplayer",
                table: "Jogos",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bool");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsAdmin",
                table: "Usuario",
                type: "bool",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Multiplayer",
                table: "Jogos",
                type: "bool",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
