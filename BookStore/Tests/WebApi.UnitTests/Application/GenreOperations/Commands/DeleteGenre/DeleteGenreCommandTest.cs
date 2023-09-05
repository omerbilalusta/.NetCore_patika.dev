using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommandTest(CommonTestFixture commonTestFixture){
            _context = commonTestFixture.Context;
        }
        
        [Fact]
        public void WhenGenreCouldntFind_InvalidOperationException_ShouldBeReturn(){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 0;
            
            FluentActions.Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap türü bulunamadı!");
        }
        [Fact]
        public void WhenGenreFound_Confirmed_ShouldNotBeReturnError(){
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();
            _context.Genres.FirstOrDefault(x=> x.Id == command.GenreId).Should().BeNull();
        }
    }
}