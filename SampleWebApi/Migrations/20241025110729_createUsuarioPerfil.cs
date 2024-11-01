using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SampleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class createUsuarioPerfil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPerfil",
                columns: table => new
                {
                    IdPerfil = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPerfil", x => x.IdPerfil);
                });

            migrationBuilder.CreateTable(
                name: "tblUsuario",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdPerfil = table.Column<int>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUsuario", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_tblUsuario_tblPerfil_IdPerfil",
                        column: x => x.IdPerfil,
                        principalTable: "tblPerfil",
                        principalColumn: "IdPerfil",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblUsuario_IdPerfil",
                table: "tblUsuario",
                column: "IdPerfil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblUsuario");

            migrationBuilder.DropTable(
                name: "tblPerfil");
        }
    }
}
