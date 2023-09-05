using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAuthorNotFound_InvalidOperationException_ShouldBeReturn()
        {
            // arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            command.Id = 999;

            // act
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenmek istenen yazar bulunamadı!");
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeUpdated()
        {
            // arrange
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
            command.Id = 1;
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                Name = "hobbit",
                Surname = "hobbit",
                BornDate = System.DateTime.Now.Date.AddYears(-1)
            };
            command.model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Id == command.Id);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BornDate.Should().Be(model.BornDate);
        }
    }
}