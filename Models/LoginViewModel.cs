using System.ComponentModel.DataAnnotations;

namespace JitCars.Models {
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o nome de usuário")]
        [Display(Name = "Usuário")]
        public string? NomeUsuario { get; set; }

        [Required(ErrorMessage = "Informe sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string? Senha { get; set; }

        [Display(Name = "Manter logado?")]
        public bool ManterLogado { get; set; }
    }
}