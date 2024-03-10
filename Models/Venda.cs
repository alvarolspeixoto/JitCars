using System.ComponentModel.DataAnnotations;

namespace JitCars.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira a data da venda")]
        public DateTime Data {  get; set; }

        [StringLength(44)]
        public string? NotaFiscal { get; set; }

        [Required(ErrorMessage = "Informe o método de pagamento")]
        public string? FormaPagamento { get; set; }

        [Required(ErrorMessage = "Informe o cliente")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        
    }
}
