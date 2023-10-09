using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entites;

namespace WebApi.Application.Command.UserOperations.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel model { get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle(){
            var user = _context.Users.SingleOrDefault(x=> x.Email == model.Email);
            
            if(user != null)
                throw new InvalidOperationException("Kitap zaten mevcut!");
            
            user = _mapper.Map<User>(model);
           
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public class CreateUserModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}