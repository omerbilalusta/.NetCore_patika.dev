using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.Command.BookOperations.CreateBook;
using WebApi.DBOperations;
using WebApi.Entites;
using static WebApi.Application.Command.BookOperations.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandTest(CommonTestFixture testFixture){ //parantez içerisinde CommonTextFixture'ı çağırarak onun constructor metodunu çalıştırmış olduk, böylelikle konfiglerimiz çalışmış oldu.
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            //arrenge (Hazırlık)
            var book = new Book(){Title = "Test_WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn", PageCount=100, PublishDate= new DateTime(1990,01,10), GenreId=1};
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.model = new CreateBookModel(){ Title = book.Title };

            //act && assert (Çalıştırma, Doğrulama)
            FluentActions
                    .Invoking(()=> command.Handle()) // çalışması gereken methotları Invoke içerisinde veriyoruz.
                    .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut!"); // alacağımız sonucu Should() 'da kontrol ediyoruz.
        }
        [Fact]
        public void ValidInputAreGiven_Book_ShouldBeCreated(){
            //arrenge
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel(){
                Title ="hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.AddYears(-1),
                GenreId =1
            };
            command.model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var book =_context.Books.SingleOrDefault(book => book.Title == model.Title);
            
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}