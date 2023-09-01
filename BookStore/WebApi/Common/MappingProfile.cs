using AutoMapper;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.Command.BookOperations.CreateBook;
using WebApi.Application.Command.BookOperations.DeleteBook;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.Queries.BookOperations.GetBook;
using WebApi.Application.Queries.BookOperations.GetBooks;
using WebApi.Entites;
using static WebApi.Application.Command.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.Application.Command.BookOperations.UpdateBook.UpdateBookCommand;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile(){
            CreateMap<CreateBookModel, Book>(); // Bu komut CreateBookModel nesnesi Book nesnesine maplenebilir olsun demektir.
            CreateMap<Book,GetBookViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name)).ForMember(dest=> dest.Author, opt=> opt.MapFrom(src=> src.Author.Name + " " +src.Author.Surname)); // ForMembet fonksiyonu ile GenreId si girilmiş olan her kitap için Genre tablosuna giderek o ID ile eşleşen Genre varlığının ismi getiriliyor.
            CreateMap<Book, BooksViewModel>().ForMember(dest=> dest.Genre, opt=> opt.MapFrom(src=> src.Genre.Name)).ForMember(dest=> dest.Author, opt=> opt.MapFrom(src=> src.Author.Name + " " +src.Author.Surname));
            CreateMap<UpdateBookModel,Book>();
            CreateMap<Genre, GenresViewModel>(); //Genre'yı GenresViewModel'e dönüştür demiş oluyorum
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, GetAuthorByDetailModel>();
            CreateMap<CreateAuthorModel, Author>();
            CreateMap<UpdateAuthorModel, Author>();
        }
    }
}