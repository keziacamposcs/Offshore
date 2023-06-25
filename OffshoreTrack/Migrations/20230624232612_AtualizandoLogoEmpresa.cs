using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoLogoEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empresa_ordemCompra_OrdemCompraid_oc",
                table: "Empresa");

            migrationBuilder.DropIndex(
                name: "IX_Empresa_OrdemCompraid_oc",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "OrdemCompraid_oc",
                table: "Empresa");

            migrationBuilder.AddColumn<int>(
                name: "empresaid_empresa",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<byte[]>(
                name: "logoEmpresa",
                table: "Empresa",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "logoEmpresa",
                table: "Empresa",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdemCompraid_oc",
                table: "Empresa",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_OrdemCompraid_oc",
                table: "Empresa",
                column: "OrdemCompraid_oc");

            migrationBuilder.AddForeignKey(
                name: "FK_Empresa_ordemCompra_OrdemCompraid_oc",
                table: "Empresa",
                column: "OrdemCompraid_oc",
                principalTable: "ordemCompra",
                principalColumn: "id_oc");
        }
    }
}
