using AutoMapper;
using Base.Response;
using Data.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using Operation.Cqrs;
using Schema.Dto;

namespace MovieStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IMediator mediator;
        public TokenController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
        {
            var operation = new CreateTokenCommand(request);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
