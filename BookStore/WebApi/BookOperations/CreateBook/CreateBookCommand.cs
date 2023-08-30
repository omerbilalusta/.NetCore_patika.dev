using WebApi.DBOperations;

namespace WebApi.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel model { get; set;}
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context){
            _context = context;
        }
        public void Handle(){
            var book = _context.Books.SingleOrDefault(x=> x.Title == model.Title);
            
            if(book != null)
                throw new InvalidOperationException("Kitap zaten mevcut!");
            
            book = new Book();
            book.Title = model.Title;
            book.PublishDate = model.PublishDate;
            book.GenreId = model.GenreId;
            book.PageCount = model.PageCount;

            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public class CreateBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }
}