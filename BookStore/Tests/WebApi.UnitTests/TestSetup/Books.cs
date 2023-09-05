using WebApi.DBOperations;
using WebApi.Entites;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context){ //extension metod olmuş oldu.
            context.Books.AddRange(
                new Book{
                //Id = 1, //Id vermesek bile sistem zaten sıralı şekilde id veriyor ve bu birer birer artıyor.
                    Title = "Lean Startup",
                    GenreId = 1 ,
                    PageCount = 200,
                    PublishDate = new DateTime(2012,03,12),
                    AuthorID = 1
                },
                new Book{
                    Title = "Herland",
                    GenreId = 2 ,
                    PageCount = 250,
                    PublishDate = new DateTime(2010,02,23),
                    AuthorID = 2
                },
                new Book{
                    //Id = 3,
                    Title = "Dune",
                    GenreId = 2 ,
                    PageCount = 540,
                    PublishDate = new DateTime(2020,04,21),
                    AuthorID = 2
                }
            );
        }
    }
}