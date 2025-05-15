using Core.Application.DTOs;
using FluentValidation;

namespace Core.Application.Validators
{
    public class CreateMenuItemDtoValidator : AbstractValidator<CreateMenuItemDto>
    {
        public CreateMenuItemDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del ítem es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0")
                .LessThan(1000000).WithMessage("El precio es demasiado alto");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Debe seleccionar una categoría válida");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("La URL de la imagen no puede exceder los 500 caracteres")
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("La URL de la imagen debe ser válida");
        }
    }

    public class UpdateMenuItemDtoValidator : AbstractValidator<UpdateMenuItemDto>
    {
        public UpdateMenuItemDtoValidator()
        {
            RuleFor(x => x.MenuItemId)
                .GreaterThan(0).WithMessage("El ID del ítem debe ser válido");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del ítem es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("La descripción no puede exceder los 500 caracteres");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que 0")
                .LessThan(1000000).WithMessage("El precio es demasiado alto");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Debe seleccionar una categoría válida");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("La URL de la imagen no puede exceder los 500 caracteres")
                .Must(uri => string.IsNullOrEmpty(uri) || Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("La URL de la imagen debe ser válida");
        }
    }
}
