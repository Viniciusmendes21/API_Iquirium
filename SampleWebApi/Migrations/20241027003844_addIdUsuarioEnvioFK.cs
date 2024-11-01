using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addIdUsuarioEnvioFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackProduto_IdUsuarioEnvio",
                table: "tblFeedbackProduto",
                column: "IdUsuarioEnvio");

            migrationBuilder.AddForeignKey(
                name: "FK_tblFeedbackProduto_tblUsuario_IdUsuarioEnvio",
                table: "tblFeedbackProduto",
                column: "IdUsuarioEnvio",
                principalTable: "tblUsuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFeedbackProduto_tblUsuario_IdUsuarioEnvio",
                table: "tblFeedbackProduto");

            migrationBuilder.DropIndex(
                name: "IX_tblFeedbackProduto_IdUsuarioEnvio",
                table: "tblFeedbackProduto");
        }
    }
}
