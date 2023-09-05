using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.DBOperations;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommandTest(CommonTestFixture commonTestFixture){
            _context = commonTestFixture.Context;
        }
        [Fact]
        public void WhenGenreNotFound_InvalidOperationException_ShouldBeReturn(){
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 0;

            FluentActions.Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");
        }
        
        [Fact]
        public void WhenAlreadyGenreGiven_InvalidOperationException_ShouldBeReturn(){
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            command.model = new UpdateGenreModel(){
                Name = "Romance"
            };

            FluentActions.Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aynı isimli bir kitap türü zaten mevcut!");
        }
        [Fact]
        public void WhenInputIsValid_Confirmed_ShouldBeUpdated(){ //HappyPath
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            command.model = new UpdateGenreModel(){
                Name="Action"
            };

            FluentActions.Invoking(()=> command.Handle()).Invoke();

            var genre = _context.Genres.FirstOrDefault(x=> x.Id == command.GenreId);

            genre.Should().NotBeNull();
            genre.Name.Should().Be(command.model.Name);
        }
    }
}