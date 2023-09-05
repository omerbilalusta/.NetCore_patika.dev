using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorModel model { get; set;}

        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle(){
            var author = _context.Authors.SingleOrDefault(x=> x.Name == model.Name && x.Surname == model.Surname);
            
            if(author != null)
                throw new InvalidOperationException("Yazar zaten mevcut!");

            _context.Authors.Add(_mapper.Map<Author>(model));
            _context.SaveChanges();
        }

    }
    public class CreateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set; }
    }
}