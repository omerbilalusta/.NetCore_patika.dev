using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture // Bu kısımda test sürecinde ihtiyacımız olacak gerekli configurasyonları ayaraldık.
    {
        public BookStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;}
        public CommonTestFixture(){
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName: "BookStoreTestDB").Options;
            Context = new BookStoreDbContext(options);
            Context.Database.EnsureCreated(); // Database'in oluşturulduğundan emin olmak için
            Context.AddBooks();
            Context.AddGenres();
            Context.AddAuthors();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}