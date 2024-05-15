using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asociatie_proprietari.Data.Migrations
{
    /// <inheritdoc />
    public partial class Modifiedtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "An",
                table: "ConsumApa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Luna",
                table: "ConsumApa",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Angajat",
                columns: table => new
                {
                    AngajatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Functie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salariu = table.Column<int>(type: "int", nullable: false),
                    Bonus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Angajat", x => x.AngajatId);
                });

            migrationBuilder.CreateTable(
                name: "Contract",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Furnizor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataIncepere = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFinalizare = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInregistrare = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataScadenta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SumaDePlata = table.Column<int>(type: "int", nullable: true),
                    SumaPlatita = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractId = table.Column<int>(type: "int", nullable: true),
                    ApartamentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Factura_Apartament_ApartamentId",
                        column: x => x.ApartamentId,
                        principalTable: "Apartament",
                        principalColumn: "ApartamentId");
                    table.ForeignKey(
                        name: "FK_Factura_Contract_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contract",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Plata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumarCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumeCard = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardCVV = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SumaPlatita = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacturaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plata", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plata_Factura_FacturaId",
                        column: x => x.FacturaId,
                        principalTable: "Factura",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factura_ApartamentId",
                table: "Factura",
                column: "ApartamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Factura_ContractId",
                table: "Factura",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Plata_FacturaId",
                table: "Plata",
                column: "FacturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Angajat");

            migrationBuilder.DropTable(
                name: "Plata");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Contract");

            migrationBuilder.DropColumn(
                name: "An",
                table: "ConsumApa");

            migrationBuilder.DropColumn(
                name: "Luna",
                table: "ConsumApa");
        }
    }
}
