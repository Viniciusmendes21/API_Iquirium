using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class mudançaRelaçãoFeedbackProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblFeedbackProduto_IdTipoFeedbackProduto",
                table: "tblFeedbackProduto");

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackProduto_IdTipoFeedbackProduto",
                table: "tblFeedbackProduto",
                column: "IdTipoFeedbackProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tblFeedbackProduto_IdTipoFeedbackProduto",
                table: "tblFeedbackProduto");

            migrationBuilder.CreateIndex(
                name: "IX_tblFeedbackProduto_IdTipoFeedbackProduto",
                table: "tblFeedbackProduto",
                column: "IdTipoFeedbackProduto",
                unique: true);
        }
    }
}
