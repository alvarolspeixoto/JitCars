using System.ComponentModel.DataAnnotations;

namespace JitCars.Models
{
    public class Endereco
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira um CEP")]
        [StringLength(8)]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "Insira um estado")]
        [StringLength(20)]
        public int? Estado { get; set; }

        [Required(ErrorMessage = "Insira uma cidade")]
        [StringLength(30)]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "Insira um bairro")]
        [StringLength(20)]
        public string? Bairro { get; set; }

        [Required(ErrorMessage = "Insira uma rua")]
        [StringLength(30)]
        public string? Rua { get; set; }

        [Required(ErrorMessage = "Insira um número")]
        public int Numero { get; set; }



    }
}
