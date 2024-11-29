using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SampleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class CriadoTabelasTipoReportTipoFeedbackUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblTipoFeedbackUsuario",
                columns: table => new
                {
                    IdTipoFeedbackUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoFeedbackUsuario", x => x.IdTipoFeedbackUsuario);
                });

            migrationBuilder.CreateTable(
                name: "tblTipoReport",
                columns: table => new
                {
                    IdTipoReport = table.Column<string>(type: "integer", nullable: false),
                    Nome = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoReport", x => x.IdTipoReport);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTipoFeedbackUsuario");

            migrationBuilder.DropTable(
                name: "tblTipoReport");
        }
    }
}
