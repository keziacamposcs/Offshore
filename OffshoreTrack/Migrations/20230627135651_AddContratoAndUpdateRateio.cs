using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddContratoAndUpdateRateio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "porcentagem2",
                table: "rateio",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "porcentagem1",
                table: "rateio",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    id_contrato = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    contrato = table.Column<string>(type: "TEXT", nullable: true),
                    dataInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    dataFim = table.Column<DateTime>(type: "TEXT", nullable: true),
                    anexo = table.Column<string>(type: "TEXT", nullable: true),
                    id_fornecedor = table.Column<int>(type: "INTEGER", nullable: true),
                    id_cliente = table.Column<int>(type: "INTEGER", nullable: true),
                    id_status = table.Column<int>(type: "INTEGER", nullable: true),
                    id_setor = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.id_contrato);
                    table.ForeignKey(
                        name: "FK_Contrato_Status_id_status",
                        column: x => x.id_status,
                        principalTable: "Status",
                        principalColumn: "id_status");
                    table.ForeignKey(
                        name: "FK_Contrato_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "FK_Contrato_fornecedor_id_fornecedor",
                        column: x => x.id_fornecedor,
                        principalTable: "fornecedor",
                        principalColumn: "id_fornecedor");
                    table.ForeignKey(
                        name: "FK_Contrato_setor_id_setor",
                        column: x => x.id_setor,
                        principalTable: "setor",
                        principalColumn: "id_setor");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_id_cliente",
                table: "Contrato",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_id_fornecedor",
                table: "Contrato",
                column: "id_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_id_setor",
                table: "Contrato",
                column: "id_setor");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_id_status",
                table: "Contrato",
                column: "id_status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contrato");

            migrationBuilder.AlterColumn<decimal>(
                name: "porcentagem2",
                table: "rateio",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "porcentagem1",
                table: "rateio",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "REAL",
                oldNullable: true);
        }
    }
}
