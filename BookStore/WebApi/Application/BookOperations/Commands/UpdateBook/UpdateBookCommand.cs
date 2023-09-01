using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.Command.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public UpdateBookModel model { get; set;}
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int id {get; set;}
        public UpdateBookCommand(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book == null)
                throw new InvalidOperationException("Güncellenmek istenen kitap bulunamadı!");
            
            _mapper.Map(model,book);
            // book.GenreId = model.GenreId != default ? model.GenreId : book.GenreId;
            // book.Title = model.Title != default ? model.Title : book.Title;
            
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