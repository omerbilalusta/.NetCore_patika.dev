using FluentAssertions;
using TestSetup;
using WebApi.Application.Command.BookOperations.CreateBook;
using static WebApi.Application.Command.BookOperations.CreateBook.CreateBookCommand;


namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTest : IClassFixture<CommonTestFixture>
    {
        
        [Theory] //birden fazla kez bu test sınıfı çalışması gerekeceği için [Theory]'yi kullanıyoruz. Düzenleme: Theory kullanmamımızın sebebi istediğimiz test girişlerini vermekde olabilir.
        [InlineData("Lord Of The Rings", 0,0)]
        [InlineData("Lord Of The Rings", 0,1)]
        [InlineData("Lord Of The Rings", 100,0)]
        [InlineData("", 0,0)]
        [InlineData("", 100,1)]
        [InlineData("", 0,1)]
        [InlineData("Lor", 100,1)]
        [InlineData("Lord", 100,0)]
        [InlineData("Lord", 0,1)]
        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId){

            //arrenge
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel(){
                Title= title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };
            //act
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGıven_Validator_ShouldBeReturnError(){
            //arrenge
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel(){
                Title= "Lord of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError(){
            //arrenge
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel(){
                Title= "Lord of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1
            };
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);
            result.Errors.Count.Should().Be(0);
        }
    }
}