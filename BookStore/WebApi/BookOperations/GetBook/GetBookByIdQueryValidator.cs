using System.Data;
using FluentValidation;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator(){
            RuleFor(query => query.id).GreaterThan(0);
        }
    }
}