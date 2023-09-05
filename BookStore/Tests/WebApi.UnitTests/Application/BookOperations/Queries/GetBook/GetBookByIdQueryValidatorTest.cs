using FluentAssertions;
using TestSetup;
using WebApi.Application.Queries.BookOperations.GetBook;

namespace Application.BookOperations.Queries.GetBook
{
    public class GetBookByIdQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int id)
        {
            GetBookByIdQuery command = new GetBookByIdQuery(null, null);
            command.id = id;

            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturn(){
            GetBookByIdQuery command = new GetBookByIdQuery(null, null);
            command.id = 1;

            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}