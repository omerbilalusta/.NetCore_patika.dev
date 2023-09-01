using System.Data;
using FluentValidation;

namespace WebApi.Application.Queries.BookOperations.GetBook
{
    public class GetBookByIdQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookByIdQueryValidator(){
            RuleFor(query => query.id).GreaterThan(0);
        }
    }
}