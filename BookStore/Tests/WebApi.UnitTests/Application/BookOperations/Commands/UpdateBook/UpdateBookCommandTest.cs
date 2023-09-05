using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.Command.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Application.Command.BookOperations.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateBookCommandTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn(){
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            command.id = 0;

            FluentActions.Invoking(() => command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenmek istenen kitap bulunamadı!");
        }

        [Fact]
        public void WhenInputIsValid_Confirmed_ShouldBeUpdated(){ //HappyPath
            UpdateBookCommand command = new UpdateBookCommand(_context, _mapper);
            command.id=1;
            
            UpdateBookModel model = new UpdateBookModel(){
                Title = "Lord Of The Rings",
                GenreId = 1
            };
            
            command.model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var book = _context.Books.FirstOrDefault(x=> x.Id == command.id);

            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}