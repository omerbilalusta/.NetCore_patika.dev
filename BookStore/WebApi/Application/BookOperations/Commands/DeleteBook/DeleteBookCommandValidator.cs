using FluentValidation;

namespace WebApi.Application.Command.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator(){
            RuleFor(command => command.id).GreaterThan(0);
        }
    }
}