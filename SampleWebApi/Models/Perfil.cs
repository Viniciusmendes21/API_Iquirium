using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Models
{
    public class Perfil
    {
        public int IdPerfil { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório")]
        [StringLength(128, ErrorMessage = "Você excedeu o número de caracteres máximo: 128")]
        public string Nome { get; set; }
    }
}
