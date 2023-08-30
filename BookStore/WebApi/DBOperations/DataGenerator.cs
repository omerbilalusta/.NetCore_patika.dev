using Microsoft.EntityFrameworkCore;

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
                context.Books.AddRange(
                        new Book{
                        //Id = 1, //Id vermesek bile sistem zaten sıralı şekilde id veriyor ve bu birer birer artıyor.
                        Title = "Lean Startup",
                        GenreId = 1 ,// Personal Growth
                        PageCount = 200,
                        PublishDate = new DateTime(2012,03,12)
                    },
                    new Book{
                        //Id = 2,
                        Title = "Herland",
                        GenreId = 2 ,// Science Fiction
                        PageCount = 250,
                        PublishDate = new DateTime(2010,02,23)
                    },
                    new Book{
                        //Id = 3,
                        Title = "Dune",
                        GenreId = 2 ,// Science Fiction
                        PageCount = 540,
                        PublishDate = new DateTime(2020,04,21)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}