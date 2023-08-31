using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int id {get; set;}

        public DeleteBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle(){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book == null)
                throw new InvalidOperationException("Silinmek istenen kitap mevcut değil!");
            
            _context.Remove(book);
            _context.SaveChanges();
        }
    }
}