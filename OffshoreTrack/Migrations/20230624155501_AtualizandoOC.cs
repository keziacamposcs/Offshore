using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoOC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_material_id_material",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_parteSolta_id_parteSolta",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_material",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_parteSolta",
                table: "ordemCompra");

            migrationBuilder.RenameColumn(
                name: "valor",
                table: "ordemCompra",
                newName: "valor5");

            migrationBuilder.RenameColumn(
                name: "id_parteSolta",
                table: "ordemCompra",
                newName: "quantidade5");

            migrationBuilder.RenameColumn(
                name: "id_material",
                table: "ordemCompra",
                newName: "quantidade4");

            migrationBuilder.RenameColumn(
                name: "data_compra",
                table: "ordemCompra",
                newName: "observacao");

            migrationBuilder.AddColumn<byte[]>(
                name: "anexo",
                table: "ordemCompra",
                type: "BLOB",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_conclusao",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_oc",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_prevista",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_rateio",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_status",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_usuario",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item1",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item2",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item3",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item4",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item5",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade1",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade2",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade3",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor1",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor2",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor3",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor4",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_rateio",
                table: "ordemCompra",
                column: "id_rateio");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_status",
                table: "ordemCompra",
                column: "id_status");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_usuario",
                table: "ordemCompra",
                column: "id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_Status_id_status",
                table: "ordemCompra",
                column: "id_status",
                principalTable: "Status",
                principalColumn: "id_status");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_rateio_id_rateio",
                table: "ordemCompra",
                column: "id_rateio",
                principalTable: "rateio",
                principalColumn: "id_rateio");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_usuario_id_usuario",
                table: "ordemCompra",
                column: "id_usuario",
                principalTable: "usuario",
                principalColumn: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_Status_id_status",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_rateio_id_rateio",
                table: "ordemCompra");

            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_usuario_id_usuario",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_rateio",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_status",
                table: "ordemCompra");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_usuario",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "anexo",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "data_conclusao",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "data_oc",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "data_prevista",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_rateio",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_status",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_usuario",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item1",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item2",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item3",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item4",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item5",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade1",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade2",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade3",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor1",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor2",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor3",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor4",
                table: "ordemCompra");

            migrationBuilder.RenameColumn(
                name: "valor5",
                table: "ordemCompra",
                newName: "valor");

            migrationBuilder.RenameColumn(
                name: "quantidade5",
                table: "ordemCompra",
                newName: "id_parteSolta");

            migrationBuilder.RenameColumn(
                name: "quantidade4",
                table: "ordemCompra",
                newName: "id_material");

            migrationBuilder.RenameColumn(
                name: "observacao",
                table: "ordemCompra",
                newName: "data_compra");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_material",
                table: "ordemCompra",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_parteSolta",
                table: "ordemCompra",
                column: "id_parteSolta");

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
        }
    }
}
