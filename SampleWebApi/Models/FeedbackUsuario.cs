using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class FeedbackUsuario
    {
        public int IdFeedbackUsuario { get; set; }
        public int IdUsuarioEnvio { get; set; }
        public int IdUsuarioDestino { get; set; }
        public int IDUsuarioIntermedio { get; set; }
        public int IdTipoFeedbackUsurio { get; set; }
        public int IdReport {  get; set; }
        public Boolean StatusFeedback {  get; set; }
        public string Mensagem { get; set; }
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
