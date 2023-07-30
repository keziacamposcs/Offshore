using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class updateMoeda2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_moeda",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_moeda",
                table: "ordemCompra",
                column: "id_moeda");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_Moeda_id_moeda",
                table: "ordemCompra",
                column: "id_moeda",
                principalTable: "Moeda",
                principalColumn: "id_moeda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_Moeda_id_moeda",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_moeda",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_moeda",
                table: "ordemCompra");
        }
    }
}
