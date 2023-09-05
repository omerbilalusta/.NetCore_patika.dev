using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.Command.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int id {get; set;}

        public DeleteBookCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle(){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book == null)
                throw new InvalidOperationException("Silinmek istenen kitap mevcut değil!");
            
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}