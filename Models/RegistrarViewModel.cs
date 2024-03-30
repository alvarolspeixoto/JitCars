using System.ComponentModel.DataAnnotations;

namespace JitCars.Models {
    public class RegistrarViewModel
    {
        [Required(ErrorMessage = "Informe o primeiro nome")]
        [Display(Name = "Primeiro nome")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O primeiro nome deve ter no mínimo 3 caracteres")]
        public string? PrimeiroNome { get; set; }

        [Required(ErrorMessage = "Informe o sobrenome")]
        [Display(Name = "Sobrenome")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O sobrenome deve ter no mínimo 3 caracteres")]
        public string? Sobrenome { get; set; }

        [Required(ErrorMessage = "Informe o nome de usuário")]
        [Display(Name = "Nome de usuário")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter no mínimo 3 caracteres")]
        public string? NomeUsuario { get; set; }

        [Required(ErrorMessage = "Informe o CPF")]
        [Display(Name = "CPF")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter 11 dígitos")]
        public string? Cpf { get; set; }

        [Required]
        public Endereco? Endereco { get; set; }

        [Required(ErrorMessage = "Escolha o cargo")]
        [Display(Name = "Cargo")]
        public int CargoId { get; set; }

        [Required(ErrorMessage = "Insira uma senha")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [Display(Name = "Confirmação de senha")]
        [Compare("Senha", ErrorMessage = "As senhas não batem")]
        public string? SenhaConfirmacao { get; set; }


    }
}