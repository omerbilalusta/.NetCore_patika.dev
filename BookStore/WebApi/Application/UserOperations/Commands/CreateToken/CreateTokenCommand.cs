using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.Entites;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;
using TokenHandler = WebApi.TokenOperations.TokenHandler;

namespace WebApi.Application.Command.UserOperations.CreateUser
{
    public class CreateTokenCommand
    {
        public CreateTokenModel model { get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public Token Handle(){
            var user = _context.Users.FirstOrDefault(x=> x.Email == model.email);
            if (user == null || user.Password != model.password)
            {
                throw new InvalidOperationException("Kullanıcı Adı  Şifre Hatalı");
            }

            else
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.ExpireDate.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
        }
        public class CreateTokenModel
        {
            public string email { get; set; }
            public string password { get; set; }
        }
    }
}