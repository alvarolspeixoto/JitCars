using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Insira a cor")]
        [DisplayName("Cor")]
        [StringLength(50)]
        // depois dá pra mudar pra um enum de cores ou uma classe que cadastre as cores no banco
        public string? Cor {  get; set; }

        public int? VendaId { get; set; }
        public virtual Venda? Venda { get; set; }

    }
}
