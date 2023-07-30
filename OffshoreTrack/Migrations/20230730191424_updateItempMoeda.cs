using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class updateItempMoeda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "id_moeda",
                table: "Item",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_id_moeda",
                table: "Item",
                column: "id_moeda");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Moeda_id_moeda",
                table: "Item",
                column: "id_moeda",
                principalTable: "Moeda",
                principalColumn: "id_moeda");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Moeda_id_moeda",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_id_moeda",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "id_moeda",
                table: "Item");
        }
    }
}
