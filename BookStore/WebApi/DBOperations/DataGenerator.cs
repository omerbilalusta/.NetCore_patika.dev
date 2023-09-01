using Microsoft.EntityFrameworkCore;
using WebApi.Entites;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider){ // Program ayağa kalkmadan önce veri tabanına bir kaç veri eklemek için DataGenerator.cs oluşturuldu ve alttaki işlemler yapıldı.
            using(var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>())){
                if (context.Books.Any())
                {
                    return; //Zaten veri tabanında bir kaç adet veri varsa bu çalışmasın istedik.
                }
                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name= "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }
                );
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
                        //Id = 2,
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
                context.Authors.AddRange(
                    new Author{
                        Name = "Jhon",
                        Surname = "Marston",
                        BornDate = new DateTime(1873,01,01)
                    },
                    new Author{
                        Name = "Arthur",
                        Surname = "Morgan",
                        BornDate = new DateTime(1863,01,01)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}