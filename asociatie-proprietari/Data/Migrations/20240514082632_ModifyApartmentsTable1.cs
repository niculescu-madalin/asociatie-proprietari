using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asociatie_proprietari.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifyApartmentsTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumeApartament",
                table: "Apartament",
                newName: "NumarApartament");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumarApartament",
                table: "Apartament",
                newName: "NumeApartament");
        }
    }
}
