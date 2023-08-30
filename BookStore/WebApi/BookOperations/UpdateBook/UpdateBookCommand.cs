using WebApi.DBOperations;

namespace WebApi.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel model { get; set;}
        private readonly BookStoreDbContext _context;
        public UpdateBookCommand(BookStoreDbContext context){
            _context = context;
        }

        public void Handle(int id){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book == null)
                throw new InvalidOperationException("Güncellenmek istenen kitap bulunamadı!");
            
            book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
            book.Title = model.Title != default ? model.Title : book.Title;
            
            _context.Update(book);
            _context.SaveChanges();
        }

        public class UpdateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}