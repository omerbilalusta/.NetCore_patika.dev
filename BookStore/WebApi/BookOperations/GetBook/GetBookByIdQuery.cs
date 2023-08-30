using System.Reflection.Metadata;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBookByIdQuery(BookStoreDbContext context){
            _context = context;
        }

        public GetBookViewModel Handle(int id){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book == null)
                throw new InvalidOperationException("İlgili kitap bulunamadı!");
            GetBookViewModel model = new GetBookViewModel();
            model.Title = book.Title;
            model.Genre = ((GenreEnum)book.GenreId).ToString();
            model.PublishDate = book.PublishDate.ToString();
            model.PageCount = book.PageCount;

            return model;
        }
        
    }
    public class GetBookViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}