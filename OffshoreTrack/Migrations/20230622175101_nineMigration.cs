using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OffshoreTrack.Migrations
{
    /// <inheritdoc />
    public partial class nineMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "cod_barra",
                table: "material",
                newName: "qrcode");

            migrationBuilder.AddColumn<int>(
                name: "id_manutencao",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "manutencaoid_manutencao",
                table: "material",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data",
                table: "manutencao",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_conclusao",
                table: "manutencao",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "data_prevista",
                table: "manutencao",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id_status",
                table: "manutencao",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    id_status = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    status = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.id_status);
                });

            migrationBuilder.CreateIndex(
                name: "IX_material_manutencaoid_manutencao",
                table: "material",
                column: "manutencaoid_manutencao");

            migrationBuilder.CreateIndex(
                name: "IX_manutencao_id_status",
                table: "manutencao",
                column: "id_status");

            migrationBuilder.AddForeignKey(
                name: "FK_manutencao_Status_id_status",
                table: "manutencao",
                column: "id_status",
                principalTable: "Status",
                principalColumn: "id_status");

            migrationBuilder.AddForeignKey(
                name: "FK_material_manutencao_manutencaoid_manutencao",
                table: "material",
                column: "manutencaoid_manutencao",
                principalTable: "manutencao",
                principalColumn: "id_manutencao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_manutencao_Status_id_status",
                table: "manutencao");

            migrationBuilder.DropForeignKey(
                name: "FK_material_manutencao_manutencaoid_manutencao",
                table: "material");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_material_manutencaoid_manutencao",
                table: "material");

            migrationBuilder.DropIndex(
                name: "IX_manutencao_id_status",
                table: "manutencao");

            migrationBuilder.DropColumn(
                name: "id_manutencao",
                table: "material");

            migrationBuilder.DropColumn(
                name: "manutencaoid_manutencao",
                table: "material");

            migrationBuilder.DropColumn(
                name: "data",
                table: "manutencao");

            migrationBuilder.DropColumn(
                name: "data_conclusao",
                table: "manutencao");

            migrationBuilder.DropColumn(
                name: "data_prevista",
                table: "manutencao");

            migrationBuilder.DropColumn(
                name: "id_status",
                table: "manutencao");

            migrationBuilder.RenameColumn(
                name: "qrcode",
                table: "material",
                newName: "cod_barra");
        }
    }
}
