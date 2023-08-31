using System.Reflection.Metadata;
using AutoMapper;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.BookOperations.GetBook
{
    public class GetBookByIdQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int id {get; set;}
        public GetBookByIdQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetBookViewModel Handle(){
            var book = _context.Books.SingleOrDefault(x=> x.Id == id);
            if(book == null)
                throw new InvalidOperationException("İlgili kitap bulunamadı!");
            GetBookViewModel model = _mapper.Map<GetBookViewModel>(book);

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