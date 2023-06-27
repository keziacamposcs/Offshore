using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoOC3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "moeda",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "moeda",
                table: "ordemCompra");
        }
    }
}
