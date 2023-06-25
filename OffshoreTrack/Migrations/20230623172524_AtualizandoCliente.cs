using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "endereco",
                table: "cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "razaoSocial",
                table: "cliente",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "telefone",
                table: "cliente",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "endereco",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "razaoSocial",
                table: "cliente");

            migrationBuilder.DropColumn(
                name: "telefone",
                table: "cliente");
        }
    }
}
