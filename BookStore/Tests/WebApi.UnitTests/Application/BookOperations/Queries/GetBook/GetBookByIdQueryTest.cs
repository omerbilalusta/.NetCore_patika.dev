using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.Queries.BookOperations.GetBook;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.BookOperations.Queries.GetBook
{
    public class GetBookByIdQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetBookByIdQueryTest(CommonTestFixture commonTestFixture){
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }
        
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenBookCouldntFind_InvalidOperationException_ShouldBeReturn(int id){
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.id = id;
            
            FluentActions.Invoking(() => query.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("İlgili kitap bulunamadı!");
        }
        [Fact]
        public void WhenBookFind_Book_ShouldBeReturn(){ //HappyPath
            GetBookByIdQuery query = new GetBookByIdQuery(_context, _mapper);
            query.id = 1;
            
            FluentActions.Invoking(() => query.Handle()).Invoke();
            var book = _context.Books.FirstOrDefault(x=> x.Id == query.id);
            book.Should().NotBeNull();
        }
    }
}