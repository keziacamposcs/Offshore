using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "tamanho",
                table: "material",
                newName: "peso");

            migrationBuilder.AlterColumn<byte[]>(
                name: "anexo",
                table: "parteSolta",
                type: "BLOB",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "descricao",
                table: "parteSolta",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dimensoes",
                table: "parteSolta",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fornecedorid_fornecedor",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_certificacao",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_fornecedor",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_local",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_status",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "localid_local",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "peso",
                table: "parteSolta",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "statusid_status",
                table: "parteSolta",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataFabricacao",
                table: "material",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "dataValidade",
                table: "material",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dimensoes",
                table: "material",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_certificacao",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_ordemcompra",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_partesolta",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_status",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ordemCompraid_oc",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "parteSoltaid_partesolta",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "statusid_status",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "atividadeLogPS",
                columns: table => new
                {
                    id_atividadeLogPs = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    acao = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: true),
                    usuarioid_usuario = table.Column<int>(type: "INTEGER", nullable: true),
                    id_parteSolta = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividadeLogPS", x => x.id_atividadeLogPs);
                    table.ForeignKey(
                        name: "FK_atividadeLogPS_parteSolta_id_parteSolta",
                        column: x => x.id_parteSolta,
                        principalTable: "parteSolta",
                        principalColumn: "id_partesolta");
                    table.ForeignKey(
                        name: "FK_atividadeLogPS_usuario_usuarioid_usuario",
                        column: x => x.usuarioid_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "Certificacao",
                columns: table => new
                {
                    id_certificacao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    certificacao = table.Column<string>(type: "TEXT", nullable: true),
                    orgaoEmissor = table.Column<string>(type: "TEXT", nullable: true),
                    dataEmissao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    dataValidade = table.Column<DateTime>(type: "TEXT", nullable: true),
                    anexo = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Materialid_material = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificacao", x => x.id_certificacao);
                    table.ForeignKey(
                        name: "FK_Certificacao_material_Materialid_material",
                        column: x => x.Materialid_material,
                        principalTable: "material",
                        principalColumn: "id_material");
                });

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_fornecedorid_fornecedor",
                table: "parteSolta",
                column: "fornecedorid_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_id_certificacao",
                table: "parteSolta",
                column: "id_certificacao");

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_localid_local",
                table: "parteSolta",
                column: "localid_local");

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_statusid_status",
                table: "parteSolta",
                column: "statusid_status");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_certificacao",
                table: "material",
                column: "id_certificacao");

            migrationBuilder.CreateIndex(
                name: "IX_material_ordemCompraid_oc",
                table: "material",
                column: "ordemCompraid_oc");

            migrationBuilder.CreateIndex(
                name: "IX_material_parteSoltaid_partesolta",
                table: "material",
                column: "parteSoltaid_partesolta");

            migrationBuilder.CreateIndex(
                name: "IX_material_statusid_status",
                table: "material",
                column: "statusid_status");

            migrationBuilder.CreateIndex(
                name: "IX_atividadeLogPS_id_parteSolta",
                table: "atividadeLogPS",
                column: "id_parteSolta");

            migrationBuilder.CreateIndex(
                name: "IX_atividadeLogPS_usuarioid_usuario",
                table: "atividadeLogPS",
                column: "usuarioid_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Certificacao_Materialid_material",
                table: "Certificacao",
                column: "Materialid_material");

            migrationBuilder.AddForeignKey(
                name: "FK_material_Certificacao_id_certificacao",
                table: "material",
                column: "id_certificacao",
                principalTable: "Certificacao",
                principalColumn: "id_certificacao");

            migrationBuilder.AddForeignKey(
                name: "FK_material_Status_statusid_status",
                table: "material",
                column: "statusid_status",
                principalTable: "Status",
                principalColumn: "id_status");

            migrationBuilder.AddForeignKey(
                name: "FK_material_ordemCompra_ordemCompraid_oc",
                table: "material",
                column: "ordemCompraid_oc",
                principalTable: "ordemCompra",
                principalColumn: "id_oc");

            migrationBuilder.AddForeignKey(
                name: "FK_material_parteSolta_parteSoltaid_partesolta",
                table: "material",
                column: "parteSoltaid_partesolta",
                principalTable: "parteSolta",
                principalColumn: "id_partesolta");

            migrationBuilder.AddForeignKey(
                name: "FK_parteSolta_Certificacao_id_certificacao",
                table: "parteSolta",
                column: "id_certificacao",
                principalTable: "Certificacao",
                principalColumn: "id_certificacao");

            migrationBuilder.AddForeignKey(
                name: "FK_parteSolta_Status_statusid_status",
                table: "parteSolta",
                column: "statusid_status",
                principalTable: "Status",
                principalColumn: "id_status");

            migrationBuilder.AddForeignKey(
                name: "FK_parteSolta_fornecedor_fornecedorid_fornecedor",
                table: "parteSolta",
                column: "fornecedorid_fornecedor",
                principalTable: "fornecedor",
                principalColumn: "id_fornecedor");

            migrationBuilder.AddForeignKey(
                name: "FK_parteSolta_local_localid_local",
                table: "parteSolta",
                column: "localid_local",
                principalTable: "local",
                principalColumn: "id_local");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_material_Certificacao_id_certificacao",
                table: "material");

            migrationBuilder.DropForeignKey(
                name: "FK_material_Status_statusid_status",
                table: "material");

            migrationBuilder.DropForeignKey(
                name: "FK_material_ordemCompra_ordemCompraid_oc",
                table: "material");

            migrationBuilder.DropForeignKey(
                name: "FK_material_parteSolta_parteSoltaid_partesolta",
                table: "material");

            migrationBuilder.DropForeignKey(
                name: "FK_parteSolta_Certificacao_id_certificacao",
                table: "parteSolta");

            migrationBuilder.DropForeignKey(
                name: "FK_parteSolta_Status_statusid_status",
                table: "parteSolta");

            migrationBuilder.DropForeignKey(
                name: "FK_parteSolta_fornecedor_fornecedorid_fornecedor",
                table: "parteSolta");

            migrationBuilder.DropForeignKey(
                name: "FK_parteSolta_local_localid_local",
                table: "parteSolta");

            migrationBuilder.DropTable(
                name: "atividadeLogPS");

            migrationBuilder.DropTable(
                name: "Certificacao");

            migrationBuilder.DropIndex(
                name: "IX_parteSolta_fornecedorid_fornecedor",
                table: "parteSolta");

            migrationBuilder.DropIndex(
                name: "IX_parteSolta_id_certificacao",
                table: "parteSolta");

            migrationBuilder.DropIndex(
                name: "IX_parteSolta_localid_local",
                table: "parteSolta");

            migrationBuilder.DropIndex(
                name: "IX_parteSolta_statusid_status",
                table: "parteSolta");

            migrationBuilder.DropIndex(
                name: "IX_material_id_certificacao",
                table: "material");

            migrationBuilder.DropIndex(
                name: "IX_material_ordemCompraid_oc",
                table: "material");

            migrationBuilder.DropIndex(
                name: "IX_material_parteSoltaid_partesolta",
                table: "material");

            migrationBuilder.DropIndex(
                name: "IX_material_statusid_status",
                table: "material");

            migrationBuilder.DropColumn(
                name: "descricao",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "dimensoes",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "fornecedorid_fornecedor",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "id_certificacao",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "id_fornecedor",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "id_local",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "id_status",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "localid_local",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "peso",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "statusid_status",
                table: "parteSolta");

            migrationBuilder.DropColumn(
                name: "dataFabricacao",
                table: "material");

            migrationBuilder.DropColumn(
                name: "dataValidade",
                table: "material");

            migrationBuilder.DropColumn(
                name: "dimensoes",
                table: "material");

            migrationBuilder.DropColumn(
                name: "id_certificacao",
                table: "material");

            migrationBuilder.DropColumn(
                name: "id_ordemcompra",
                table: "material");

            migrationBuilder.DropColumn(
                name: "id_partesolta",
                table: "material");

            migrationBuilder.DropColumn(
                name: "id_status",
                table: "material");

            migrationBuilder.DropColumn(
                name: "ordemCompraid_oc",
                table: "material");

            migrationBuilder.DropColumn(
                name: "parteSoltaid_partesolta",
                table: "material");

            migrationBuilder.DropColumn(
                name: "statusid_status",
                table: "material");

            migrationBuilder.RenameColumn(
                name: "peso",
                table: "material",
                newName: "tamanho");

            migrationBuilder.AlterColumn<string>(
                name: "anexo",
                table: "parteSolta",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldNullable: true);
        }
    }
}
