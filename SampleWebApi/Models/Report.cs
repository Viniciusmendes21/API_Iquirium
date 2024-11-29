using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class Report
    {
        public int IdReport { get; set; }

        [Required(ErrorMessage = "O campo IdFeedback é obrigatório")]
        public int? IdFeedbackUsuario { get; set; }

        [Required(ErrorMessage = "O campo IdTipoReport é obrigatório")]
        public int? IdTipoReport { get; set; }

        [Required(ErrorMessage = "O campo Deferido é obrigatório")]
        public Boolean Deferido { get; set; }

    }


    public class TipoReport
    {
        public int IdTipoReport { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string Nome { get; set; }
    }
}
