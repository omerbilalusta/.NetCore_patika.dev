using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateAuthorCommandValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData("a", "Morgan")]
        [InlineData("Arthur", "a")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name, string surname)
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null,null);
            command.model = new CreateAuthorModel()
            {
                Name = name,
                Surname = surname,
                BornDate = System.DateTime.Now.Date.AddYears(-1)
            };

            // act
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGıven_Validator_ShouldBeReturnError()
        {
            //arrenge
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.model = new CreateAuthorModel()
            {
                Name = "Arthur",
                Surname = "Morgan",
                BornDate = DateTime.Now.Date,
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError() //HappyPath
        {
            //arrenge
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.model = new CreateAuthorModel()
            {
                Name = "Arthur",
                Surname = "Morgan",
                BornDate = DateTime.Now.Date.AddYears(-2),
            };
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}