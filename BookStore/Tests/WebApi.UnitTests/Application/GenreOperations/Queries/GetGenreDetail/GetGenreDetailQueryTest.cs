using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.Queries.BookOperations.GetBook;
using WebApi.DBOperations;

namespace Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenreDetailQueryTest(CommonTestFixture commonTestFixture){
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Theory]
        [InlineData(0)]
        public void WhenGenreCouldntFind_InvalidOperationException_ShouldReturn(int id){
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreID = id;

            FluentActions.Invoking(()=> query.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap türü bulunamadı!");
        }
        [Fact]
        public void WhenGenreFind_Genre_ShouldReturn(){
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreID = 1;

            FluentActions.Invoking(()=> query.Handle()).Invoke();
            var genre = _context.Genres.FirstOrDefault(x=> x.Id == query.GenreID);
            genre.Should().NotBeNull();
        }
    }
}