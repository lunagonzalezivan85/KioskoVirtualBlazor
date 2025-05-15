using Core.Application.DTOs;
using FluentValidation;

namespace Core.Application.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido")
                .EmailAddress().WithMessage("El correo electrónico no es válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es requerido")
                .MinimumLength(3).WithMessage("El nombre completo debe tener al menos 3 caracteres");
        }
    }

    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido")
                .EmailAddress().WithMessage("El correo electrónico no es válido");

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("El nombre completo es requerido")
                .MinimumLength(3).WithMessage("El nombre completo debe tener al menos 3 caracteres");
        }
    }

    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo electrónico es requerido")
                .EmailAddress().WithMessage("El correo electrónico no es válido");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida");
        }
    }
}
