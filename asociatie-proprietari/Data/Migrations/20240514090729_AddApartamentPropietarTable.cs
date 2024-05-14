using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asociatie_proprietari.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApartamentPropietarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartamentPropietar_Apartament_ApartamentsApartamentId",
                table: "ApartamentPropietar");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartamentPropietar_AspNetUsers_PropietarsId",
                table: "ApartamentPropietar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartamentPropietar",
                table: "ApartamentPropietar");

            migrationBuilder.RenameColumn(
                name: "PropietarsId",
                table: "ApartamentPropietar",
                newName: "PropietarId");

            migrationBuilder.RenameColumn(
                name: "ApartamentsApartamentId",
                table: "ApartamentPropietar",
                newName: "ApartamentId");

            migrationBuilder.RenameIndex(
                name: "IX_ApartamentPropietar_PropietarsId",
                table: "ApartamentPropietar",
                newName: "IX_ApartamentPropietar_PropietarId");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "ApartamentPropietar",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartamentPropietar",
                table: "ApartamentPropietar",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_ApartamentPropietar_ApartamentId",
                table: "ApartamentPropietar",
                column: "ApartamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartamentPropietar_Apartament_ApartamentId",
                table: "ApartamentPropietar",
                column: "ApartamentId",
                principalTable: "Apartament",
                principalColumn: "ApartamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartamentPropietar_AspNetUsers_PropietarId",
                table: "ApartamentPropietar",
                column: "PropietarId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartamentPropietar_Apartament_ApartamentId",
                table: "ApartamentPropietar");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartamentPropietar_AspNetUsers_PropietarId",
                table: "ApartamentPropietar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApartamentPropietar",
                table: "ApartamentPropietar");

            migrationBuilder.DropIndex(
                name: "IX_ApartamentPropietar_ApartamentId",
                table: "ApartamentPropietar");

            migrationBuilder.DropColumn(
                name: "id",
                table: "ApartamentPropietar");

            migrationBuilder.RenameColumn(
                name: "PropietarId",
                table: "ApartamentPropietar",
                newName: "PropietarsId");

            migrationBuilder.RenameColumn(
                name: "ApartamentId",
                table: "ApartamentPropietar",
                newName: "ApartamentsApartamentId");

            migrationBuilder.RenameIndex(
                name: "IX_ApartamentPropietar_PropietarId",
                table: "ApartamentPropietar",
                newName: "IX_ApartamentPropietar_PropietarsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApartamentPropietar",
                table: "ApartamentPropietar",
                columns: new[] { "ApartamentsApartamentId", "PropietarsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApartamentPropietar_Apartament_ApartamentsApartamentId",
                table: "ApartamentPropietar",
                column: "ApartamentsApartamentId",
                principalTable: "Apartament",
                principalColumn: "ApartamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartamentPropietar_AspNetUsers_PropietarsId",
                table: "ApartamentPropietar",
                column: "PropietarsId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
