using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthor
{
    public class GetAuthorByDetail
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public int Id {get; set;}
        public GetAuthorByDetail(IMapper mapper, IBookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public GetAuthorByDetailModel Handle(){
            var author = _context.Authors.FirstOrDefault(x=> x.Id == Id);
            if(author == null)
                throw new InvalidOperationException("İlgili yazar bulunamadı!");

            GetAuthorByDetailModel viewModel = _mapper.Map<GetAuthorByDetailModel>(author);

            return viewModel;
        }
    }
    public class GetAuthorByDetailModel
    {
        public string Name { get; set; }
        public string Surname {get; set;}
        public DateTime BornDate { get; set; }
    }
}