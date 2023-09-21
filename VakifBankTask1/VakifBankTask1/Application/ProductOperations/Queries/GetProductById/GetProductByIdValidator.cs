using FluentValidation;
using VakifBankTask1.Application.ProductOperations.Commands.UpdateProduct;

namespace VakifBankTask1.Application.ProductOperations.Queries.GetProductById
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdValidator()
        {
            RuleFor(product => product.Id).NotEmpty().GreaterThan(0);
        }
    }
}
