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
    public record CreateActorActressCommand(ActorActressRequest model) : IRequest<ApiResponse<ActorActressResponse>>;
    public record UpdateActorActressCommand(ActorActressRequest model, int Id) : IRequest<ApiResponse>;
    public record DeleteActorActressCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllActorActressQuery() : IRequest<ApiResponse<List<ActorActressResponse>>>;
}
