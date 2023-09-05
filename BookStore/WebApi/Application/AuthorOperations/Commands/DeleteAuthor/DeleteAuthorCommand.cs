using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int Id { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle(){
            var author = _context.Authors.SingleOrDefault(x=> x.Id == Id);
            if(author == null)
                throw new InvalidOperationException("Silinmek istenen yazar bulunamadı!");
                
            if(_context.Books.Where(x=> x.AuthorID == Id).Any())
                throw new InvalidOperationException("Silinmek istenen yazara ait kitap bulunduğu için silinemedi!");
            
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}