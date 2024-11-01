using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "O campo IDPerfil é obrigatório")]
        public int? IdPerfil { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string Nome { get; set; }
    }
}
