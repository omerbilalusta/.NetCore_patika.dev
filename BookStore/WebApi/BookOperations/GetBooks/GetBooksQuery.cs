using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle(){
            var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> viewModel = _mapper.Map<List<BooksViewModel>>(bookList);
            return viewModel;
        }
    }

    public class BooksViewModel // VievModel
    {
        public string Title { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
        public string Genre { get; set; }
    }
}