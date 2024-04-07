using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using JitCars.Enums;

namespace JitCars.Models
{
    public class Modelo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira uma marca")]
        [StringLength(50)]
        public string? Marca { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Insira um nome")]
        [StringLength(50)]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe o ano de fabricação")]
        [DisplayName("Ano de fabricação")]
        public int AnoFabricacao { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de carroceria")]
        public Carroceria Carroceria { get; set; }

        [StringLength(255)]
        [DisplayName("Imagem do modelo (URL)")]
        public string? ImagemUrl { get; set; }
    }
}
