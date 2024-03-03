using System.ComponentModel.DataAnnotations;

namespace JitCars.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o primeiro nome")]
        [StringLength(50)]
        public string? PrimeiroNome { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [StringLength(50)]
        public string? Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o endereço")]
        public int EnderecoId { get; set; }
        public virtual Endereco? Endereco { get; set; }

    }
}
