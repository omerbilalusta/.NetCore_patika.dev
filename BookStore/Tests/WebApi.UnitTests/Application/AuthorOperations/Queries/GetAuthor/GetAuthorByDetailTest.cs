using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorByDetailTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        
        private readonly IMapper _mapper;

        public GetAuthorByDetailTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            GetAuthorByDetail command = new GetAuthorByDetail(_mapper, _context);
            command.Id = 999;

            // act
            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>()
                         .And.Message.Should().Be("İlgili yazar bulunamadı!");
        }

        [Fact]
        public void WhenValidInputIsGiven_Author_ShouldBeReturn()
        {
            // arrange
            GetAuthorByDetail command = new GetAuthorByDetail(_mapper, _context);
            command.Id = 1;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.Id);
            author.Should().NotBeNull();
        }
    }
}