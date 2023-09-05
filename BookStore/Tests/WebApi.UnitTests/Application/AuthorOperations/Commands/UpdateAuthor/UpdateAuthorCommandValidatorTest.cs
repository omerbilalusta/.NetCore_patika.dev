using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"Arthur","Morgan")]
        [InlineData(1,"","Morgan")]
        [InlineData(0,"","Morgan")]
        [InlineData(0,"Arthur","")]
        [InlineData(1,"Ar","Morgan")]
        [InlineData(0,"Lord","Morgan")]
        [InlineData(1,"Lord","Mo")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id, string name,  string surname){
            UpdateAuthorCommand command = new UpdateAuthorCommand(null,null);
            command.Id = id;
            command.model = new UpdateAuthorModel(){
                Name = name,
                Surname = surname,
                BornDate = System.DateTime.Now.Date.AddYears(-1)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenInputsAreValid_Validator_ShouldNotBeReturnErrors(){//HappyPath
            UpdateAuthorCommand command = new UpdateAuthorCommand(null,null);
            command.Id = 1;
            command.model = new UpdateAuthorModel(){
                Name = "Arthur",
                Surname = "Morgan",
                BornDate = System.DateTime.Now.Date.AddYears(-1)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var results = validator.Validate(command);
            results.Errors.Count.Should().Be(0);
        }
    }
}