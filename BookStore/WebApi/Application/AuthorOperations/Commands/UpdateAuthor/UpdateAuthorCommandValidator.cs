using System.Data;
using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator(){
            RuleFor(command => command.Id).GreaterThan(0);
            RuleFor(command => command.model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(command => command.model.BornDate).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}