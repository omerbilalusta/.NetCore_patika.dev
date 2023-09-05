using FluentAssertions;
using TestSetup;
using WebApi.Application.Command.BookOperations.DeleteBook;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(-100)]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(int id){
            DeleteBookCommand command = new DeleteBookCommand(null,null);
            command.id = id;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(1);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(){ //HappyPath
            DeleteBookCommand command = new DeleteBookCommand(null,null);
            command.id=1;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count().Should().Be(0);

        }
    }
}