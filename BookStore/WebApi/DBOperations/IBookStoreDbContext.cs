//BookStoreDbContext bağımlılığınından(Controller'larda ve bazı başka yerlerde kullanıyorduk) kurtulmak için interface'ini oluşturduk.
using Microsoft.EntityFrameworkCore;
using WebApi.Entites;

namespace WebApi.DBOperations
{
    public interface IBookStoreDbContext
    {
        DbSet<Book> Books {get; set;}
        DbSet<Genre> Genres {get; set;}
        DbSet<Author> Authors {get; set;}

        int SaveChanges();
    }
}