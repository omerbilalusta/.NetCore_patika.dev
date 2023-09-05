using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        private readonly IMapper _mapper;

        public CreateAuthorCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            // arrange (Hazırlık)
            var author = new Author { Name = "WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn", Surname = "WhenAlreadyExistAuthorTitleIsGiven_InvalidOperationException_ShouldBeReturn", BornDate = System.DateTime.Now.Date.AddYears(-1) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            // act (Çalıştırma)
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.model = new CreateAuthorModel() { Name = author.Name, Surname = author.Surname, BornDate = author.BornDate };

            // assert (Doğrulama)
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut!");
        }
        [Fact]
        public void ValidInputAreGiven_Author_ShouldBeCreated()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "hobbit",
                Surname = "hobbit",
                BornDate = System.DateTime.Now.Date.AddYears(-1)
            };
            command.model = model;

            // act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            // assert
            var author = _context.Authors.SingleOrDefault(author => author.Name == model.Name && author.Surname == model.Surname);

            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.BornDate.Should().Be(model.BornDate);
        }
    }
}