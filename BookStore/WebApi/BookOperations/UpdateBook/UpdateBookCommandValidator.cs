using FluentValidation;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator(){
            RuleFor(command => command.id).GreaterThan(0);
            RuleFor(command => command.model.Title).MinimumLength(4).NotEmpty();
            RuleFor(command => command.model.GenreId).GreaterThan(0);
            //RuleFor(command => command.model.GenreId).IsInEnum(); //önceden bu IsinEnum() fonksiyonu işini doğru yapıyordu ancak şimdi tamamıyle yanlış çalışıyor.
        }
    }
}