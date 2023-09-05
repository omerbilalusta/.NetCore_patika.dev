using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public UpdateAuthorModel model { get; set;}
        public int Id { get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle(){
            var author = _context.Authors.FirstOrDefault(x=> x.Id == Id);
            if(author == null)
                throw new InvalidOperationException("Güncellenmek istenen yazar bulunamadı!");
            
            author.Name = string.IsNullOrEmpty(model.Name) ? author.Name : model.Name;
            author.BornDate = author.BornDate != default ? model.BornDate : author.BornDate;
            author.Surname = author.Surname != default ?  model.Surname : author.Surname ;

            //_mapper.Map(model, author);
            
            _context.Authors.Update(author);
            _context.SaveChanges();

        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BornDate { get; set;}
    }
}