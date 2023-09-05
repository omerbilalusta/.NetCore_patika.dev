using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            // arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.Id = 0;

            // act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError() //HappyPath
        {
            // arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(null);
            command.Id = 1;

            // act
            DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Equals(0);
        }
    }
}