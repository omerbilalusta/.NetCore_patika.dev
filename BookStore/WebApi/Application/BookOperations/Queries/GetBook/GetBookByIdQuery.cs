using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Application.Queries.BookOperations.GetBook
{
    public class GetBookByIdQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int id {get; set;}
        public GetBookByIdQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GetBookViewModel Handle(){
            var book = _context.Books.Include(x=>x.Genre).Include(x=> x.Author).Where(book => book.Id == id).FirstOrDefault();
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
        public string Author { get; set;}
    }
}