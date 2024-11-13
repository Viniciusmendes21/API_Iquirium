using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class FeedbackUsuario
    {
        public int IdFeedbackUsuario { get; set; }

        [Required(ErrorMessage = "O campo IdUsuarioEnvio é obrigatório")]
        public int IdUsuarioEnvio { get; set; }

        [Required(ErrorMessage = "O campo IdUsuarioDestino é obrigatório")]
        public int? IdUsuarioDestino { get; set; }

        [Required(ErrorMessage = "O campo IdUsuarioIntermedio é obrigatório")]
        public int? IdUsuarioIntermedio { get; set; }

        [Required(ErrorMessage = "O campo IdTipoFeedbackUsuario é obrigatório")]
        public int? IdTipoFeedbackUsuario { get; set; }

        [Required(ErrorMessage = "O campo StatusFeedback é obrigatório")]
        public Boolean StatusFeedback { get; set; }

        [Required(ErrorMessage = "O campo Mensagem é obrigatório")]
        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string Mensagem { get; set; }

        [Required(ErrorMessage = "O campo DataEnvio é obrigatório")]
        public DateTime DataAprovacao { get; set; }

    }

    public class TipoFeedbackUsuario
    {
        public int IdTipoFeedbackUsuario { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string Nome { get; set; }
    }

}
