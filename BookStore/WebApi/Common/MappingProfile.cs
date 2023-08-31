using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBook;
using WebApi.BookOperations.GetBooks;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateBookModel, Book>(); // Bu komut CreateBookModel nesnesi Book nesnesine maplenebilir olsun demektir.
            CreateMap<Book,GetBookViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BooksViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> ((GenreEnum)src.GenreId).ToString()));
            CreateMap<UpdateBookModel,Book>();
        }
    }
}