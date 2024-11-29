using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class FeedbackProduto
    {
        
        public int IdFeedbackProduto { get; set; }

        [Required(ErrorMessage = "O campo IDUsuario é obrigatório")]
        public int? IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo IdTipoFeedbackProduto é obrigatório")]
        public int? IdTipoFeedbackProduto {  get; set; }

        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string? Comentario { get; set; }

        [Required(ErrorMessage = "O campo DataEnvio é obrigatório")]
        public DateTime DataEnvio { get; set; }

    }

    public class TipoFeedbackProduto
    {
        public int IdTipoFeedbackProduto { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string Nome { get; set; }
    }

}
