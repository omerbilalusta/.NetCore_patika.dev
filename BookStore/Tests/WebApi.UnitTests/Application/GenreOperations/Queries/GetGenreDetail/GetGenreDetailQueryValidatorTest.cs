using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public GetGenreDetailQueryValidatorTest(CommonTestFixture commonTestFixture){
            _context = commonTestFixture.Context;
        }
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnError(){
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreID =0;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(1);
        }
        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError(){
            GetGenreDetailQuery query = new GetGenreDetailQuery(null,null);
            query.GenreID = 1;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);
            result.Errors.Count.Should().Be(0);
        }
    }
}