using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class item : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "item1",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item2",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item3",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item4",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "item5",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade1",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade2",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade3",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade4",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "quantidade5",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor1",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor2",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor3",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor4",
                table: "ordemCompra");

            migrationBuilder.DropColumn(
                name: "valor5",
                table: "ordemCompra");

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    id_item = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    item = table.Column<string>(type: "TEXT", nullable: true),
                    valor = table.Column<double>(type: "REAL", nullable: true),
                    quantidade = table.Column<int>(type: "INTEGER", nullable: true),
                    id_oc = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.id_item);
                    table.ForeignKey(
                        name: "FK_Item_ordemCompra_id_oc",
                        column: x => x.id_oc,
                        principalTable: "ordemCompra",
                        principalColumn: "id_oc");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_id_oc",
                table: "Item",
                column: "id_oc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.AddColumn<string>(
                name: "item1",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item2",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item3",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item4",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "item5",
                table: "ordemCompra",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade1",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade2",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade3",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade4",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "quantidade5",
                table: "ordemCompra",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor1",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor2",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor3",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor4",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "valor5",
                table: "ordemCompra",
                type: "REAL",
                nullable: true);
        }
    }
}
