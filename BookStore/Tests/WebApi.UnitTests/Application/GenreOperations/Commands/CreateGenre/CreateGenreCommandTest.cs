using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommandTest(CommonTestFixture commonTestFixture){
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn(){
            var genre = new Genre(){Name="Test_WhenAlreadyExistGenreTitleIsGiven_InvalidOperationException_ShouldBeReturn", IsActive=true};
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.model = new CreateGenreModel(){Name = genre.Name};

            FluentActions.Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü zaten mevcut");
        }

        [Fact]
        public void ValidInputIsGiven_Genre_ShouldBeReturn()
        {
            // Arrange
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel(){
                Name = "Test"
            };
            command.model = model;

            // Act
            FluentActions.Invoking(()=> command.Handle()).Invoke();

            // Assert
            var genre = _context.Genres.FirstOrDefault(x=> x.Name == model.Name);
            genre.Should().NotBeNull();
        }
    }
}