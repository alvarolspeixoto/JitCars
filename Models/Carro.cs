using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JitCars.Enums;

namespace JitCars.Models
{
    public class Carro
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o número do chassi")]
        [DisplayName("Número do chassi")]
        public int NumeroChassi { get; set; }

        [Required(ErrorMessage = "Insira o preço de custo")]
        [DisplayName("Preço de custo")]
        public decimal PrecoCusto { get; set; }

        [Required(ErrorMessage = "Insira o preço de venda")]
        [DisplayName("Preço de venda")]
        public decimal PrecoVenda { get; set; }

        [Required(ErrorMessage = "Escolha o modelo do carro")]
        [DisplayName("Modelo")]
        public int ModeloId { get; set; }
        public virtual Modelo? Modelo { get; set; }

        [Required(ErrorMessage = "Escolha a cor")]
        [DisplayName("Cor")]
        public Cor Cor { get; set; }

        public int? VendaId { get; set; }
        public virtual Venda? Venda { get; set; }

    }
}
