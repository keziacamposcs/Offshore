using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoFornecedor2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "inscricaoEstadual",
                table: "fornecedor",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "inscricaoMunicipal",
                table: "fornecedor",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inscricaoEstadual",
                table: "fornecedor");

            migrationBuilder.DropColumn(
                name: "inscricaoMunicipal",
                table: "fornecedor");
        }
    }
}
