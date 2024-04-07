using Microsoft.AspNetCore.Identity;
namespace JitCars.Services.Identity

{
    public class ErrosCustomizados : IdentityErrorDescriber
    {

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = "A senha deve conter pelo menos um dígito ('0'-'9')"
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"A senha deve conter pelo menos {length} caracteres"
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "A senha deve conter pelo menos uma letra maiúscula ('A'-'Z')"
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = $"O nome de usuário {userName} já está em uso"
            };
        }
    }
}
