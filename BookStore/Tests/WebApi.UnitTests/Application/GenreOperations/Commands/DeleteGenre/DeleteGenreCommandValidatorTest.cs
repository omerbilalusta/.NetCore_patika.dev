using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(int id){
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = id;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);
            
            result.Errors.Count.Should().Be(1);
        }
        [Fact]
        public void WhenValidInpuIsGiven_Validator_ShouldBeReturnError(){
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 1;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}