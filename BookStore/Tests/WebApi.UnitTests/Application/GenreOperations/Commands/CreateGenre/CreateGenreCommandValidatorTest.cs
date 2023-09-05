using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.CreateGenre;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("P")]
        [InlineData("per")]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnError(string title){
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.model = new CreateGenreModel(){
                Name = title
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(){
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.model = new CreateGenreModel(){
                Name = "Biography"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}