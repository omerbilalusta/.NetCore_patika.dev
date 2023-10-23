using Azure.Core;
using Base.Response;
using Base.Token;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Operation.Cqrs;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Command
{
    public class TokenCommandHandler :
        IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly JwtConfig jwtConfig;

        public TokenCommandHandler(MovieStoreDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.dbContext = dbContext;
            this.jwtConfig = jwtConfig.CurrentValue;
        }

        public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.FirstName == request.model.Firstname && x.LastName == request.model.Lastname, cancellationToken);
            if (entity == null)
                return new ApiResponse<TokenResponse>("Invalid user information");

            if (entity.IsActive == false)
                return new ApiResponse<TokenResponse>("Invalid user");

            string token = Token(entity);
            TokenResponse tokenResponse = new()
            {
                Token = token,
                ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                Firstname = entity.FirstName,
                Lastname = entity.LastName
            };

            return new ApiResponse<TokenResponse>(tokenResponse);
        }

        private string Token(Customer customer)
        {
            Claim[] claims = GetClaims(customer);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }

        private Claim[] GetClaims(Customer customer)
        {
            var claims = new[]
            {
                new Claim("Id", customer.Id.ToString()),
                new Claim("Firstname", customer.FirstName.ToString()),
                new Claim("Lastname", customer.LastName.ToString()),
                new Claim("Role", customer.Role),
            };

            return claims;
        }
    }
}
