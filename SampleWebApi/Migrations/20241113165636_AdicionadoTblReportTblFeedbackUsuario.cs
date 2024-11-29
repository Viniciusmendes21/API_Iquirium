using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SampleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AdicionadoTblReportTblFeedbackUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblFeedbackUsuario",
                columns: table => new
                {
                    IdFeedbackUsuario = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdUsuarioEnvio = table.Column<int>(type: "integer", nullable: false),
                    IdUsuarioDestino = table.Column<int>(type: "integer", nullable: false),
                    IdUsuarioIntermedio = table.Column<int>(type: "integer", nullable: false),
                    IdTipoFeedbackUsuario = table.Column<int>(type: "integer", nullable: false),
                    StatusFeedback = table.Column<bool>(type: "boolean", nullable: false),
                    Mensagem = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    DataAprovacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFeedbackUsuario", x => x.IdFeedbackUsuario);
                    table.ForeignKey(
                        name: "FK_tblFeedbackUsuario_tblTipoFeedbackUsuario_IdTipoFeedbackUsu~",
                        column: x => x.IdTipoFeedbackUsuario,
                        principalTable: "tblTipoFeedbackUsuario",
                        principalColumn: "IdTipoFeedbackUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFeedbackUsuario_tblUsuario_IdUsuarioDestino",
                        column: x => x.IdUsuarioDestino,
                        principalTable: "tblUsuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFeedbackUsuario_tblUsuario_IdUsuarioEnvio",
                        column: x => x.IdUsuarioEnvio,
                        principalTable: "tblUsuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblFeedbackUsuario_tblUsuario_IdUsuarioIntermedio",
                        column: x => x.IdUsuarioIntermedio,
                        principalTable: "tblUsuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblReport",
                columns: table => new
                {
                    IdReport = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IdFeedbackUsuario = table.Column<int>(type: "integer", nullable: false),
                    IdTipoReport = table.Column<int>(type: "integer", nullable: false),
                    Deferido = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblReport", x => x.IdReport);
                    table.ForeignKey(
                        name: "FK_tblReport_tblFeedbackUsuario_IdFeedbackUsuario",
                        column: x => x.IdFeedbackUsuario,
                        principalTable: "tblFeedbackUsuario",
                        principalColumn: "IdFeedbackUsuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblReport_tblTipoReport_IdTipoReport",
                        column: x => x.IdTipoReport,
                        principalTable: "tblTipoReport",
                        principalColumn: "IdTipoReport",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackUsuario_IdTipoFeedbackUsuario",
                table: "tblFeedbackUsuario",
                column: "IdTipoFeedbackUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackUsuario_IdUsuarioDestino",
                table: "tblFeedbackUsuario",
                column: "IdUsuarioDestino");

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackUsuario_IdUsuarioEnvio",
                table: "tblFeedbackUsuario",
                column: "IdUsuarioEnvio");

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackUsuario_IdUsuarioIntermedio",
                table: "tblFeedbackUsuario",
                column: "IdUsuarioIntermedio");

            migrationBuilder.CreateIndex(
                name: "IX_tblReport_IdFeedbackUsuario",
                table: "tblReport",
                column: "IdFeedbackUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_tblReport_IdTipoReport",
                table: "tblReport",
                column: "IdTipoReport");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblReport");

            migrationBuilder.DropTable(
                name: "tblFeedbackUsuario");
        }
    }
}
