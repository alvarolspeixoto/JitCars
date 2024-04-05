using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace JitCars.Models
{
    public class Funcionario : IdentityUser
    {
        [Required]
        public string? PrimeiroNome { get; set; }

        [Required]
        public string? Sobrenome { get; set; }

        [Required]
        public string? Cpf { get; set; }

        [Required]
        public int EnderecoId { get; set; }
        public virtual Endereco? Endereco { get; set; }

        [Required]
        public int CargoId { get; set; }
        public virtual Cargo? Cargo { get; set; }

        public string? Telefone { get; set; }
    }
}
