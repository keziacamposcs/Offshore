using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class DeleteModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "usuario",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "tipo",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Status",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "setor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "rateio",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "permissao",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "manutencao",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "local",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "fornecedor",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "FormaPagamento",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Empresa",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "criticidade",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Contrato",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "cliente",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Deletado",
                table: "Certificacao",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "tipo");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Status");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "setor");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "rateio");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "permissao");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "material");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "manutencao");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "local");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "fornecedor");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "criticidade");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "Deletado",
                table: "Certificacao");
        }
    }
}
