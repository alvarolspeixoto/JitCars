using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JitCars.Models
{
    public class Venda
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira a data da venda")]
        public DateTime Data { get; set; } = DateTime.UtcNow;

        [StringLength(44)]
        [DisplayName("Nota Fiscal")]
        public string? NotaFiscal { get; set; }

        [Required(ErrorMessage = "Informe o método de pagamento")]
        [DisplayName("Forma de pagamento")] 
        public string? FormaPagamento { get; set; }

        [Required(ErrorMessage = "Informe o cliente")]
        [DisplayName("Digite o CPF do cliente para filtrar")]
        public int ClienteId { get; set; }
        public virtual Cliente? Cliente { get; set; }

        [Required(ErrorMessage = "Informe o funcionário responsável pela venda")]
        [DisplayName("Funcionário")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario? Funcionario { get; set; }
        
    }
}
