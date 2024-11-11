using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class Report
    {
        public int IdReport { get; set; }
        public int IdFeedback { get; set; }
        public int IdTipoFeedback { get; set; }
        public Boolean Deferido { get; set; }

    }


    public class TipoReport
    {
        public string IdTipoReport { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string Nome {  get; set; }
    }
}
