using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class EditPermissoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "permissaoEmpresa",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoFormaPagamento",
                table: "permissao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "permissaoEmpresa",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoFormaPagamento",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
