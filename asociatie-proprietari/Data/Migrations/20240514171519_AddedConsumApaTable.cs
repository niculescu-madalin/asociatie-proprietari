using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace asociatie_proprietari.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedConsumApaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsumApa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsumApaRece = table.Column<int>(type: "int", nullable: true),
                    ConsumApaCalda = table.Column<int>(type: "int", nullable: true),
                    ApartamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsumApa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsumApa_Apartament_ApartamentId",
                        column: x => x.ApartamentId,
                        principalTable: "Apartament",
                        principalColumn: "ApartamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsumApa_ApartamentId",
                table: "ConsumApa",
                column: "ApartamentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsumApa");
        }
    }
}
