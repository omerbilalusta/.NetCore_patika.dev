using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel model {get; set;}
        private readonly IBookStoreDbContext _context;

        public CreateGenreCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(x=> x.Name == model.Name);
            if(genre != null)
                throw new InvalidOperationException("Kitap türü zaten mevcut");
            
            genre = new Genre();
            genre.Name = model.Name;
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }
    }
    public class CreateGenreModel{
        public string Name { get; set; }
    }
}