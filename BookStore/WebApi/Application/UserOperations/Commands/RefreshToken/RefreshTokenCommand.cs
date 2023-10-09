using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using WebApi.DBOperations;
using WebApi.Entites;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;
using TokenHandler = WebApi.TokenOperations.TokenHandler;

namespace WebApi.Application.Command.UserOperations.CreateUser
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set;}
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public Token Handle(){
            var user = _context.Users.FirstOrDefault(x=> x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user == null)
            {
                throw new InvalidOperationException("Valid bir Refresh token Bulunamadı!");
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
    }
}