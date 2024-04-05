using System.ComponentModel.DataAnnotations;

namespace JitCars.Enums
{
    public enum FormaPagamento
    {
        Dinheiro = 0,
        [Display(Name = "Cartão de crédito")]
        CartaoCredito = 1,
        [Display(Name = "Cartão de débito")]
        CartaoDebito = 2,
        Boleto = 3,
        [Display(Name = "Transferência")]
        Transferencia = 4,
        Pix = 5,
        Cheque = 6
    }

}


// Comentário para CAIOoooo (quando ver, remova):
// fiz o enum aí, mas tu vai ter que resolver como exibir o nome que vc realmente quer,
// em vez desses nomes q tão aí, ex.: CartaoCredito -> Cartão de crédito