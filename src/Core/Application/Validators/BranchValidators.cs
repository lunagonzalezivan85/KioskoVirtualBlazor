using Core.Application.DTOs;
using FluentValidation;

namespace Core.Application.Validators
{
    public class CreateBranchDtoValidator : AbstractValidator<CreateBranchDto>
    {
        public CreateBranchDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sucursal es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La dirección es requerida")
                .MaximumLength(200).WithMessage("La dirección no puede exceder los 200 caracteres");

            RuleFor(x => x.ContactInfo)
                .NotEmpty().WithMessage("La información de contacto es requerida")
                .MaximumLength(100).WithMessage("La información de contacto no puede exceder los 100 caracteres");
        }
    }

    public class UpdateBranchDtoValidator : AbstractValidator<UpdateBranchDto>
    {
        public UpdateBranchDtoValidator()
        {
            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("El ID de la sucursal debe ser válido");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre de la sucursal es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("La dirección es requerida")
                .MaximumLength(200).WithMessage("La dirección no puede exceder los 200 caracteres");

            RuleFor(x => x.ContactInfo)
                .NotEmpty().WithMessage("La información de contacto es requerida")
                .MaximumLength(100).WithMessage("La información de contacto no puede exceder los 100 caracteres");
        }
    }
}
