using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class CreateAtividadeLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "atividadeLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Acao = table.Column<string>(type: "TEXT", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    id_usuario = table.Column<int>(type: "INTEGER", nullable: true),
                    id_material = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_atividadeLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_atividadeLog_material_id_material",
                        column: x => x.id_material,
                        principalTable: "material",
                        principalColumn: "id_material");
                    table.ForeignKey(
                        name: "FK_atividadeLog_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_atividadeLog_id_material",
                table: "atividadeLog",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_atividadeLog_id_usuario",
                table: "atividadeLog",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "atividadeLog");
        }
    }
}
