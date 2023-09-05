using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorCouldntFind_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = 999;

            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinmek istenen yazar bulunamadı!");
        }

        [Fact]
        public void WhenAuthorFindButHasBook_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = 1;

            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinmek istenen yazara ait kitap bulunduğu için silinemedi!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeDeleted()
        {
            // arrange
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = 3;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.Id);

            author.Should().BeNull();
        }
    }
}