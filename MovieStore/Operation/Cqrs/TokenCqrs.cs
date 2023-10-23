using Azure.Core;
using Base.Response;
using MediatR;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Cqrs
{
    public record CreateTokenCommand (TokenRequest model) : IRequest<ApiResponse<TokenResponse>>;
}
