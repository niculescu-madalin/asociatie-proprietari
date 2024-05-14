using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asociatie_proprietari.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "ImagineProfil",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nume",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prenume",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PropietarId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagineProfil",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nume",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Prenume",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PropietarId",
                table: "AspNetUsers");
        }
    }
}
