using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class SixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_permissao",
                table: "usuario",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "permissao",
                columns: table => new
                {
                    id_permissao = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome_permissao = table.Column<string>(type: "TEXT", nullable: true),
                    pode_criar = table.Column<bool>(type: "INTEGER", nullable: false),
                    pode_ler = table.Column<bool>(type: "INTEGER", nullable: false),
                    pode_atualizar = table.Column<bool>(type: "INTEGER", nullable: false),
                    pode_deletar = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissao", x => x.id_permissao);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuario_id_permissao",
                table: "usuario",
                column: "id_permissao");

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_permissao_id_permissao",
                table: "usuario",
                column: "id_permissao",
                principalTable: "permissao",
                principalColumn: "id_permissao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuario_permissao_id_permissao",
                table: "usuario");

            migrationBuilder.DropTable(
                name: "permissao");

            migrationBuilder.DropIndex(
                name: "IX_usuario_id_permissao",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "id_permissao",
                table: "usuario");
        }
    }
}
