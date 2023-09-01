using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var genre = _context.Genres.FirstOrDefault(x=> x.Id == GenreId);
            if(genre == null)
                throw new InvalidOperationException("Kitap türü bulunamadı!");
            else if(_context.Genres.Any(x=> x.Name.ToLower() == model.Name.ToLower() && x.Id != GenreId))
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut!");
            
            genre.Name = string.IsNullOrEmpty(model.Name) ? genre.Name : model.Name ;
            genre.IsActive = model.IsActive;
            
            _context.Update(genre);
            _context.SaveChanges();
        }

    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}