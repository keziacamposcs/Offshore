using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    cliente = table.Column<string>(type: "TEXT", nullable: true),
                    cnpj = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.id_cliente);
                });

            migrationBuilder.CreateTable(
                name: "criticidade",
                columns: table => new
                {
                    id_criticidade = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    criticidade = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_criticidade", x => x.id_criticidade);
                });

            migrationBuilder.CreateTable(
                name: "fornecedor",
                columns: table => new
                {
                    id_fornecedor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fornecedor = table.Column<string>(type: "TEXT", nullable: true),
                    cnpj = table.Column<string>(type: "TEXT", nullable: true),
                    endereco = table.Column<string>(type: "TEXT", nullable: true),
                    telefone = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    vendedor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fornecedor", x => x.id_fornecedor);
                });

            migrationBuilder.CreateTable(
                name: "ordemCompra",
                columns: table => new
                {
                    id_oc = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    oc = table.Column<string>(type: "TEXT", nullable: true),
                    valor = table.Column<double>(type: "REAL", nullable: true),
                    data_compra = table.Column<DateOnly>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordemCompra", x => x.id_oc);
                });

            migrationBuilder.CreateTable(
                name: "setor",
                columns: table => new
                {
                    id_setor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    setor = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setor", x => x.id_setor);
                });

            migrationBuilder.CreateTable(
                name: "tipo",
                columns: table => new
                {
                    id_tipo = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tipo = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipo", x => x.id_tipo);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    usuario = table.Column<string>(type: "TEXT", nullable: true),
                    senha = table.Column<string>(type: "TEXT", nullable: true),
                    nome = table.Column<string>(type: "TEXT", nullable: true),
                    cpf = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id_usuario);
                });

            migrationBuilder.CreateTable(
                name: "local",
                columns: table => new
                {
                    id_local = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    local = table.Column<string>(type: "TEXT", nullable: true),
                    id_cliente = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_local", x => x.id_local);
                    table.ForeignKey(
                        name: "FK_local_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                });

            migrationBuilder.CreateTable(
                name: "rateio",
                columns: table => new
                {
                    id_rateio = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rateio = table.Column<string>(type: "TEXT", nullable: true),
                    valor1 = table.Column<int>(type: "INTEGER", nullable: true),
                    valor2 = table.Column<int>(type: "INTEGER", nullable: true),
                    id_setor1 = table.Column<int>(type: "INTEGER", nullable: true),
                    id_setor2 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rateio", x => x.id_rateio);
                    table.ForeignKey(
                        name: "FK_rateio_setor_id_setor1",
                        column: x => x.id_setor1,
                        principalTable: "setor",
                        principalColumn: "id_setor");
                    table.ForeignKey(
                        name: "FK_rateio_setor_id_setor2",
                        column: x => x.id_setor2,
                        principalTable: "setor",
                        principalColumn: "id_setor");
                });

            migrationBuilder.CreateTable(
                name: "material",
                columns: table => new
                {
                    id_material = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    material = table.Column<string>(type: "TEXT", nullable: true),
                    descricao = table.Column<string>(type: "TEXT", nullable: true),
                    tamanho = table.Column<string>(type: "TEXT", nullable: true),
                    cod_barra = table.Column<string>(type: "TEXT", nullable: true),
                    anexo = table.Column<string>(type: "TEXT", nullable: true),
                    id_tipo = table.Column<int>(type: "INTEGER", nullable: true),
                    id_criticidade = table.Column<int>(type: "INTEGER", nullable: true),
                    id_setor = table.Column<int>(type: "INTEGER", nullable: true),
                    id_cliente = table.Column<int>(type: "INTEGER", nullable: true),
                    id_local = table.Column<int>(type: "INTEGER", nullable: true),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: true),
                    id_fornecedor = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_material", x => x.id_material);
                    table.ForeignKey(
                        name: "FK_material_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente");
                    table.ForeignKey(
                        name: "FK_material_criticidade_id_criticidade",
                        column: x => x.id_criticidade,
                        principalTable: "criticidade",
                        principalColumn: "id_criticidade");
                    table.ForeignKey(
                        name: "FK_material_fornecedor_id_fornecedor",
                        column: x => x.id_fornecedor,
                        principalTable: "fornecedor",
                        principalColumn: "id_fornecedor");
                    table.ForeignKey(
                        name: "FK_material_local_id_local",
                        column: x => x.id_local,
                        principalTable: "local",
                        principalColumn: "id_local");
                    table.ForeignKey(
                        name: "FK_material_setor_id_setor",
                        column: x => x.id_setor,
                        principalTable: "setor",
                        principalColumn: "id_setor");
                    table.ForeignKey(
                        name: "FK_material_tipo_id_tipo",
                        column: x => x.id_tipo,
                        principalTable: "tipo",
                        principalColumn: "id_tipo");
                    table.ForeignKey(
                        name: "FK_material_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateTable(
                name: "manutencao",
                columns: table => new
                {
                    id_manutencao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    manutencao = table.Column<string>(type: "TEXT", nullable: true),
                    descricao = table.Column<string>(type: "TEXT", nullable: true),
                    tipo = table.Column<string>(type: "TEXT", nullable: true),
                    id_material = table.Column<int>(type: "INTEGER", nullable: true),
                    id_setor = table.Column<int>(type: "INTEGER", nullable: true),
                    id_fornecedor = table.Column<int>(type: "INTEGER", nullable: true),
                    id_criticidade = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_manutencao", x => x.id_manutencao);
                    table.ForeignKey(
                        name: "FK_manutencao_criticidade_id_criticidade",
                        column: x => x.id_criticidade,
                        principalTable: "criticidade",
                        principalColumn: "id_criticidade");
                    table.ForeignKey(
                        name: "FK_manutencao_fornecedor_id_fornecedor",
                        column: x => x.id_fornecedor,
                        principalTable: "fornecedor",
                        principalColumn: "id_fornecedor");
                    table.ForeignKey(
                        name: "FK_manutencao_material_id_material",
                        column: x => x.id_material,
                        principalTable: "material",
                        principalColumn: "id_material");
                    table.ForeignKey(
                        name: "FK_manutencao_setor_id_setor",
                        column: x => x.id_setor,
                        principalTable: "setor",
                        principalColumn: "id_setor");
                });

            migrationBuilder.CreateTable(
                name: "parteSolta",
                columns: table => new
                {
                    id_partesolta = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    partesolta = table.Column<string>(type: "TEXT", nullable: true),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    anexo = table.Column<string>(type: "TEXT", nullable: true),
                    id_oc = table.Column<int>(type: "INTEGER", nullable: true),
                    id_material = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parteSolta", x => x.id_partesolta);
                    table.ForeignKey(
                        name: "FK_parteSolta_material_id_material",
                        column: x => x.id_material,
                        principalTable: "material",
                        principalColumn: "id_material");
                    table.ForeignKey(
                        name: "FK_parteSolta_ordemCompra_id_oc",
                        column: x => x.id_oc,
                        principalTable: "ordemCompra",
                        principalColumn: "id_oc");
                });

            migrationBuilder.CreateIndex(
                name: "IX_local_id_cliente",
                table: "local",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_manutencao_id_criticidade",
                table: "manutencao",
                column: "id_criticidade");

            migrationBuilder.CreateIndex(
                name: "IX_manutencao_id_fornecedor",
                table: "manutencao",
                column: "id_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_manutencao_id_material",
                table: "manutencao",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_manutencao_id_setor",
                table: "manutencao",
                column: "id_setor");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_cliente",
                table: "material",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_criticidade",
                table: "material",
                column: "id_criticidade");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_fornecedor",
                table: "material",
                column: "id_fornecedor");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_local",
                table: "material",
                column: "id_local");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_setor",
                table: "material",
                column: "id_setor");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_tipo",
                table: "material",
                column: "id_tipo");

            migrationBuilder.CreateIndex(
                name: "IX_material_id_usuario",
                table: "material",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_id_material",
                table: "parteSolta",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_parteSolta_id_oc",
                table: "parteSolta",
                column: "id_oc");

            migrationBuilder.CreateIndex(
                name: "IX_rateio_id_setor1",
                table: "rateio",
                column: "id_setor1");

            migrationBuilder.CreateIndex(
                name: "IX_rateio_id_setor2",
                table: "rateio",
                column: "id_setor2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "manutencao");

            migrationBuilder.DropTable(
                name: "parteSolta");

            migrationBuilder.DropTable(
                name: "rateio");

            migrationBuilder.DropTable(
                name: "material");

            migrationBuilder.DropTable(
                name: "ordemCompra");

            migrationBuilder.DropTable(
                name: "criticidade");

            migrationBuilder.DropTable(
                name: "fornecedor");

            migrationBuilder.DropTable(
                name: "local");

            migrationBuilder.DropTable(
                name: "setor");

            migrationBuilder.DropTable(
                name: "tipo");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "cliente");
        }
    }
}
