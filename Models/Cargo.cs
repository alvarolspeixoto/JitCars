using System.ComponentModel.DataAnnotations;

namespace JitCars.Models
{
    public class Cargo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o título do cargo")]
        [StringLength(50)]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "Informe a data de contratação")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Informe o salário do cargo")]
        public decimal Salario { get; set; }

    }
}
