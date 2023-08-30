using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBooksQuery(BookStoreDbContext context){
            _context = context;
        }
        public List<BooksViewModel> Handle(){
            var bookList = _context.Books.OrderBy(x=>x.Id).ToList<Book>();
            List<BooksViewModel> viewModel = new List<BooksViewModel>();
            foreach (var item in bookList)
            {
                viewModel.Add(new BooksViewModel(){
                    Title = item.Title,
                    Genre = ((GenreEnum)item.GenreId).ToString(),
                    PublishDate = item.PublishDate.Date.ToString("dd/MM/yyyy"),
                    PageCount = item.PageCount
                });
            }
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