using System.ComponentModel.DataAnnotations;

namespace JitCars.Models {
    public class MudarSenhaViewModel {
        [Required(ErrorMessage = "Insira sua senha antiga")]
        [Display(Name = "Senha atual")]
        [DataType(DataType.Password)]
        public string? SenhaAtual { get; set; }

        [Required(ErrorMessage = "Insira a nova senha")]
        [Display(Name = "Nova senha")]
        [DataType(DataType.Password)]
        public string? NovaSenha { get; set; }

        [Display(Name = "Confirmação de senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não batem")]
        [DataType(DataType.Password)]
        public string? SenhaConfirmacao { get; set; }
    }
}