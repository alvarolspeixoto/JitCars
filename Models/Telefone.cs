using System.ComponentModel.DataAnnotations;

namespace JitCars.Models
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o número de telefone")]
        public string? Numero { get; set; }

        public int? ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

    }
}
