using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorByDetailValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorByDetailValidatorTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidInputIsGiven_InvalidOperationException_ShouldBeReturn(int id)
        {
            // arrange
            GetAuthorByDetail command = new GetAuthorByDetail(_mapper, _context);
            command.Id = id;

            GetAuthorByDetailValidator validate = new GetAuthorByDetailValidator();
            var result = validate.Validate(command);

            // act
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        public void WhenValidInputIsGiven_Author_ShouldBeReturn()
        {
            // arrange
            GetAuthorByDetail command = new GetAuthorByDetail(_mapper, _context);
            command.Id = 1;

            GetAuthorByDetailValidator validate = new GetAuthorByDetailValidator();
            var result = validate.Validate(command);

            // act
            result.Errors.Count.Should().Be(0);
        }
    }
}