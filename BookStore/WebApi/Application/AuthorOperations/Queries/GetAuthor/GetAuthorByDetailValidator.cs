using FluentValidation;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorByDetailValidator : AbstractValidator<GetAuthorByDetail>
    {
        public GetAuthorByDetailValidator(){
            RuleFor(query => query.Id).GreaterThan(0);
        }
    }
}