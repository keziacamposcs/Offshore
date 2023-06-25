using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    id_empresa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    empresa = table.Column<string>(type: "TEXT", nullable: true),
                    razaoSocialEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    cnpjEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    inscricaoEstadualEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    inscricaoMunicipalEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    enderecoEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    telefoneEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    emailEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    responsavelEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    logoEmpresa = table.Column<string>(type: "TEXT", nullable: true),
                    OrdemCompraid_oc = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.id_empresa);
                    table.ForeignKey(
                        name: "FK_Empresa_ordemCompra_OrdemCompraid_oc",
                        column: x => x.OrdemCompraid_oc,
                        principalTable: "ordemCompra",
                        principalColumn: "id_oc");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Empresa_OrdemCompraid_oc",
                table: "Empresa",
                column: "OrdemCompraid_oc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Empresa");
        }
    }
}
