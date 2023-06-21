using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_parteSolta_ordemCompra_id_oc",
                table: "parteSolta");

            migrationBuilder.DropIndex(
                name: "IX_parteSolta_id_oc",
                table: "parteSolta");

            migrationBuilder.AddColumn<int>(
                name: "ocid_oc",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_fornecedor",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_fornecedor2",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_fornecedor3",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_material",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_parteSolta",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_setor",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "prioridade",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_ocid_oc",
                table: "parteSolta",
                column: "ocid_oc");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_fornecedor",
                table: "ordemCompra",
                column: "id_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_fornecedor2",
                table: "ordemCompra",
                column: "id_fornecedor2");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_fornecedor3",
                table: "ordemCompra",
                column: "id_fornecedor3");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_material",
                table: "ordemCompra",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_parteSolta",
                table: "ordemCompra",
                column: "id_parteSolta");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_setor",
                table: "ordemCompra",
                column: "id_setor");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_fornecedor_id_fornecedor",
                table: "ordemCompra",
                column: "id_fornecedor",
                principalTable: "fornecedor",
                principalColumn: "id_fornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_fornecedor_id_fornecedor2",
                table: "ordemCompra",
                column: "id_fornecedor2",
                principalTable: "fornecedor",
                principalColumn: "id_fornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_fornecedor_id_fornecedor3",
                table: "ordemCompra",
                column: "id_fornecedor3",
                principalTable: "fornecedor",
                principalColumn: "id_fornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_material_id_material",
                table: "ordemCompra",
                column: "id_material",
                principalTable: "material",
                principalColumn: "id_material");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_parteSolta_id_parteSolta",
                table: "ordemCompra",
                column: "id_parteSolta",
                principalTable: "parteSolta",
                principalColumn: "id_partesolta");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_setor_id_setor",
                table: "ordemCompra",
                column: "id_setor",
                principalTable: "setor",
                principalColumn: "id_setor");

            migrationBuilder.AddForeignKey(
                name: "FK_parteSolta_ordemCompra_ocid_oc",
                table: "parteSolta",
                column: "ocid_oc",
                principalTable: "ordemCompra",
                principalColumn: "id_oc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_fornecedor_id_fornecedor",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_fornecedor_id_fornecedor2",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_fornecedor_id_fornecedor3",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_material_id_material",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_parteSolta_id_parteSolta",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_setor_id_setor",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_parteSolta_ordemCompra_ocid_oc",
                table: "parteSolta");

            migrationBuilder.DropIndex(
                name: "IX_parteSolta_ocid_oc",
                table: "parteSolta");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_fornecedor",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_fornecedor2",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_fornecedor3",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_material",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_parteSolta",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_setor",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "ocid_oc",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "id_fornecedor",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_fornecedor2",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_fornecedor3",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_material",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_parteSolta",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_setor",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "prioridade",
                table: "ordemCompra");

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_id_oc",
                table: "parteSolta",
                column: "id_oc");

            migrationBuilder.AddForeignKey(
                name: "FK_parteSolta_ordemCompra_id_oc",
                table: "parteSolta",
                column: "id_oc",
                principalTable: "ordemCompra",
                principalColumn: "id_oc");
        }
    }
}
