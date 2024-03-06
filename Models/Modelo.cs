﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public DateTime DataFabricacao { get; set; }

        [Required(ErrorMessage = "Insira um tipo (Ex.: Hatch, Sedan)")]
        [StringLength(50)]
        public string? Tipo { get; set; }
    }
}