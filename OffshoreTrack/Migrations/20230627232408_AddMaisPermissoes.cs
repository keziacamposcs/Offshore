using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AddMaisPermissoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "permissaoCertificado",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoCliente",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoContrato",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoCriticidade",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.AddColumn<bool>(
                name: "permissaoFornecedor",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoLocal",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoManutencao",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoMaterial",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoOrdemCompra",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoParteSolta",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoRateio",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoSetor",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "permissaoTipo",
                table: "permissao",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "permissaoCertificado",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoCliente",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoContrato",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoCriticidade",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoEmpresa",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoFormaPagamento",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoFornecedor",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoLocal",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoManutencao",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoMaterial",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoOrdemCompra",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoParteSolta",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoRateio",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoSetor",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "permissaoTipo",
                table: "permissao");
        }
    }
}
