using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SampleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class createDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblTipoFeedbackProduto",
                columns: table => new
                {
                    IdTipoFeedbackProduto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTipoFeedbackProduto", x => x.IdTipoFeedbackProduto);
                });

            migrationBuilder.CreateTable(
                name: "tblFeedbackProduto",
                columns: table => new
                {
                    IdFeedbackProduto = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuarioEnvio = table.Column<int>(type: "integer", nullable: false),
                    IdTipoFeedbackProduto = table.Column<int>(type: "integer", nullable: false),
                    Comentario = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    DataEnvio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFeedbackProduto", x => x.IdFeedbackProduto);
                    table.ForeignKey(
                        name: "FK_tblFeedbackProduto_tblTipoFeedbackProduto_IdTipoFeedbackPro~",
                        column: x => x.IdTipoFeedbackProduto,
                        principalTable: "tblTipoFeedbackProduto",
                        principalColumn: "IdTipoFeedbackProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackProduto_IdTipoFeedbackProduto",
                table: "tblFeedbackProduto",
                column: "IdTipoFeedbackProduto",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFeedbackProduto");

            migrationBuilder.DropTable(
                name: "tblTipoFeedbackProduto");
        }
    }
}
