using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoLogoEmpresa2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_Empresa_empresaid_empresa",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_empresaid_empresa",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "empresaid_empresa",
                table: "ordemCompra");

            migrationBuilder.AddColumn<int>(
                name: "id_empresa",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_empresa",
                table: "ordemCompra",
                column: "id_empresa");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_Empresa_id_empresa",
                table: "ordemCompra",
                column: "id_empresa",
                principalTable: "Empresa",
                principalColumn: "id_empresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_Empresa_id_empresa",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_empresa",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_empresa",
                table: "ordemCompra");

            migrationBuilder.AddColumn<int>(
                name: "empresaid_empresa",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_empresaid_empresa",
                table: "ordemCompra",
                column: "empresaid_empresa");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_Empresa_empresaid_empresa",
                table: "ordemCompra",
                column: "empresaid_empresa",
                principalTable: "Empresa",
                principalColumn: "id_empresa",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
