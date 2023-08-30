using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBookCommand
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommand(BookStoreDbContext context){
            _context = context;
        }
        public void Handle(int id){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book == null)
                throw new InvalidOperationException("Silinmek istenen kitap mevcut değil!");
            
            _context.Remove(book);
            _context.SaveChanges();
        }
    }
}