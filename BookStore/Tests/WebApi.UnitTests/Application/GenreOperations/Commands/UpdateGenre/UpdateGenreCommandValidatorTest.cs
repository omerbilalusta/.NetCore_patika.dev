using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1,"Bio")]
        public void WhenInvalidInputsIsGiven_Validator_ShouldBeReturnError(int id, string GenreName){
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = id;
            command.model = new UpdateGenreModel(){
                Name=GenreName
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotReturnError(){//HappyPath
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.model = new UpdateGenreModel(){
                Name="Biography"
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}