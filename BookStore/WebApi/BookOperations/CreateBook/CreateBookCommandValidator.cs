using FluentValidation;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand> // FluentValidation'ı kalıtım olarak almış ve CreateBookCommand sınıfına göre kurallar yazacağımızı söylemiş olduk.
    {
        public CreateBookCommandValidator(){// Kuralları aşağıda yazdık.
            RuleFor(command => command.model.GenreId).GreaterThan(0); // Kitap türü id'si sıfırdan büyük olmalı
            RuleFor(command => command.model.PageCount).GreaterThan(0); // Sayfa sayısı sıfırdan büyük olmalı
            RuleFor(command => command.model.PublishDate.Date).NotEmpty().LessThan(DateTime.Now.Date); // Yayınlanma tarihi bulunduğumuz günden öncesini işaret etmeli
            RuleFor(command => command.model.Title).NotEmpty().MinimumLength(4); // Başlığı 4 karakterden kısa olamaz
        }
    }
}