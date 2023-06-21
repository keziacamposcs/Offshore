using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "tipo",
                table: "manutencao");

            migrationBuilder.AddColumn<int>(
                name: "id_tipo",
                table: "manutencao",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_manutencao_id_tipo",
                table: "manutencao",
                column: "id_tipo");

            migrationBuilder.AddForeignKey(
                name: "FK_manutencao_tipo_id_tipo",
                table: "manutencao",
                column: "id_tipo",
                principalTable: "tipo",
                principalColumn: "id_tipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_manutencao_tipo_id_tipo",
                table: "manutencao");

            migrationBuilder.DropIndex(
                name: "IX_manutencao_id_tipo",
                table: "manutencao");

            migrationBuilder.DropColumn(
                name: "id_tipo",
                table: "manutencao");

            migrationBuilder.AddColumn<string>(
                name: "tipo",
                table: "manutencao",
                type: "TEXT",
                nullable: true);
        }
    }
}
