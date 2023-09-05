using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.Command.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entites;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteBookCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenBookIsNotFound_InvalidOperationException_ShouldBeReturn(){
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context, _mapper);
            command.id = 0;

            //act && /assert
            FluentActions.Invoking(()=> command.Handle())
                         .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinmek istenen kitap mevcut değil!");
        }
        
        [Fact] //HappyPath (doğru çalıştığında)
        public void WhenBookIsFound_Confirmed_ShouldBeDeleted(){
            DeleteBookCommand command = new DeleteBookCommand(_context, _mapper);
            command.id = 1; //Test veritabanımızda zaten veri olduğu için "1" ID'sine sahip bir nesne zaten olacak.

            FluentActions.Invoking(()=> command.Handle()).Invoke();
            _context.Books.FirstOrDefault(x=> x.Id == command.id).Should().BeNull(); //Üst satırda Handle metodu çalıştırıldı ve ilgili veri silindi ardından silinmiş mi diye kontrol ettiğimizde silinmiş olmalı.
            
        }

    }
}