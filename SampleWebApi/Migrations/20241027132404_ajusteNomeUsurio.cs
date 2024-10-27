using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ajusteNomeUsurio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFeedbackProduto_tblUsuario_IdUsuarioEnvio",
                table: "tblFeedbackProduto");

            migrationBuilder.RenameColumn(
                name: "IdUsuarioEnvio",
                table: "tblFeedbackProduto",
                newName: "IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_tblFeedbackProduto_IdUsuarioEnvio",
                table: "tblFeedbackProduto",
                newName: "IX_tblFeedbackProduto_IdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_tblFeedbackProduto_tblUsuario_IdUsuario",
                table: "tblFeedbackProduto",
                column: "IdUsuario",
                principalTable: "tblUsuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblFeedbackProduto_tblUsuario_IdUsuario",
                table: "tblFeedbackProduto");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "tblFeedbackProduto",
                newName: "IdUsuarioEnvio");

            migrationBuilder.RenameIndex(
                name: "IX_tblFeedbackProduto_IdUsuario",
                table: "tblFeedbackProduto",
                newName: "IX_tblFeedbackProduto_IdUsuarioEnvio");

            migrationBuilder.AddForeignKey(
                name: "FK_tblFeedbackProduto_tblUsuario_IdUsuarioEnvio",
                table: "tblFeedbackProduto",
                column: "IdUsuarioEnvio",
                principalTable: "tblUsuario",
                principalColumn: "IdUsuario",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
