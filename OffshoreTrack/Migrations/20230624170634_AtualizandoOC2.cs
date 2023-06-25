using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoOC2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "valor2",
                table: "rateio",
                newName: "porcentagem2");

            migrationBuilder.RenameColumn(
                name: "valor1",
                table: "rateio",
                newName: "porcentagem1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "porcentagem2",
                table: "rateio",
                newName: "valor2");

            migrationBuilder.RenameColumn(
                name: "porcentagem1",
                table: "rateio",
                newName: "valor1");
        }
    }
}
