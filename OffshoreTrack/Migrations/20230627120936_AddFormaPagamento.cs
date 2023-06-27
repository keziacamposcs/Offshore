using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddFormaPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_formaPagamento",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormaPagamento",
                columns: table => new
                {
                    id_formaPagamento = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    formaPagamento = table.Column<string>(type: "TEXT", nullable: true),
                    descricao = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormaPagamento", x => x.id_formaPagamento);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ordemCompra_id_formaPagamento",
                table: "ordemCompra",
                column: "id_formaPagamento");

            migrationBuilder.AddForeignKey(
                name: "FK_ordemCompra_FormaPagamento_id_formaPagamento",
                table: "ordemCompra",
                column: "id_formaPagamento",
                principalTable: "FormaPagamento",
                principalColumn: "id_formaPagamento");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordemCompra_FormaPagamento_id_formaPagamento",
                table: "ordemCompra");

            migrationBuilder.DropTable(
                name: "FormaPagamento");

            migrationBuilder.DropIndex(
                name: "IX_ordemCompra_id_formaPagamento",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "id_formaPagamento",
                table: "ordemCompra");
        }
    }
}
