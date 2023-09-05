using FluentAssertions;
using TestSetup;
using WebApi.Application.Command.BookOperations.UpdateBook;
using static WebApi.Application.Command.BookOperations.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(1,"Lord Of The Rings", 0)]
        [InlineData(0,"Lord Of The Rings",1)]
        [InlineData(1,"",1)]
        [InlineData(0,"",1)]
        [InlineData(0,"",0)]
        [InlineData(1,"Lor",1)]
        [InlineData(0,"Lord",1)]
        [InlineData(1,"Lord",0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string Title, int GenreId){
            UpdateBookCommand command = new UpdateBookCommand(null,null);
            command.id = id;
            command.model = new UpdateBookModel(){
                Title = Title,
                GenreId = GenreId
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        public void WhenInputsAreValid_Validator_ShouldNotBeReturnErrors(){//HappyPath
            UpdateBookCommand command = new UpdateBookCommand(null,null);
            command.id = 1;
            command.model = new UpdateBookModel(){
                Title = "Lord Of The Rings",
                GenreId = 1
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var results = validator.Validate(command);
            results.Errors.Count.Should().Be(0);
        }
    }
}