using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TiendaServicios0.Api.Libro.Migrations
{
    public partial class PrimeraMigracionSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "libreriaMaterial",
                columns: table => new
                {
                    LibreriaMaterialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AutorLibro = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_libreriaMaterial", x => x.LibreriaMaterialId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "libreriaMaterial");
        }
    }
}
