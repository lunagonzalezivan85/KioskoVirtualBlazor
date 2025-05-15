using Core.Application.DTOs;
using FluentValidation;

namespace Core.Application.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.BranchId)
                .GreaterThan(0).WithMessage("Debe seleccionar una sucursal válida");

            RuleFor(x => x.TableNumber)
                .GreaterThan(0).When(x => x.TableNumber.HasValue)
                .WithMessage("El número de mesa debe ser mayor que 0");

            RuleFor(x => x.Comments)
                .MaximumLength(500).WithMessage("Los comentarios no pueden exceder los 500 caracteres");

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("La orden debe contener al menos un ítem")
                .Must(x => x.Count <= 50).WithMessage("La orden no puede contener más de 50 ítems");

            RuleForEach(x => x.OrderDetails).SetValidator(new CreateOrderDetailDtoValidator());
        }
    }

    public class CreateOrderDetailDtoValidator : AbstractValidator<CreateOrderDetailDto>
    {
        public CreateOrderDetailDtoValidator()
        {
            RuleFor(x => x.MenuItemId)
                .GreaterThan(0).WithMessage("Debe seleccionar un ítem válido");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor que 0")
                .LessThan(100).WithMessage("La cantidad no puede exceder 99 unidades");

            RuleFor(x => x.Comments)
                .MaximumLength(200).WithMessage("Los comentarios no pueden exceder los 200 caracteres");
        }
    }

    public class UpdateOrderStatusDtoValidator : AbstractValidator<UpdateOrderStatusDto>
    {
        public UpdateOrderStatusDtoValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("El ID de la orden debe ser válido");

            RuleFor(x => x.NewStatus)
                .IsInEnum().WithMessage("El estado de la orden no es válido");
        }
    }
}
