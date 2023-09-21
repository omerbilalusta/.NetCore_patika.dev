using FluentValidation;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Application.ProductOperations.Commands.UpdateProduct
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdateViewModel>
    {
        public ProductUpdateValidator()
        {
            RuleFor(product => product.ProductName).NotNull().MinimumLength(3);
            RuleFor(product => product.UnitPrice).NotEmpty().GreaterThan(0);
            RuleFor(product => product.QuantityPerUnit).NotEmpty().GreaterThan(1);
            RuleFor(product => product.UnitsInStock).NotEmpty().GreaterThan(0);
        }
    }
}
