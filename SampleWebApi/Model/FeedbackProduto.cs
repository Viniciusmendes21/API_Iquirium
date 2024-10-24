namespace API_Iquirium.Models
{
    public class FeedbackProduto
    {
        public int IdFeedbackProduto { get; set; }
        public int IdUsuarioEnvio { get; set; }
        public int IdTipoFeedbackProduto {  get; set; }
        public string? Comentario { get; set; }
        public DateTime? DataEnvio { get; set; }
    }

    public class TipoFeedbackProduto
    {
        public int IdTipoFeedbackProduto { get; set; }
        public string Nome { get; set; }
    }
}
