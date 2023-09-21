using FluentValidation;
using VakifBankTask1.ViewModels;

namespace VakifBankTask1.Application.ProductOperations.Commands.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator()
        {
            RuleFor(product => product.id).NotEmpty().GreaterThan(0);
        }
    }
}
