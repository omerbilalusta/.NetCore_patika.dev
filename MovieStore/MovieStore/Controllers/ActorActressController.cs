using Base.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Operation.Cqrs;
using Schema.Dto;

namespace MovieStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorActressController : ControllerBase
    {
        private IMediator mediator;
        public ActorActressController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<ActorActressResponse>>> GetAllActorActress()
        {
            var operation = new GetAllActorActressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<ActorActressResponse>> CreateActorActress(ActorActressRequest model)
        {
            var operation = new CreateActorActressCommand(model);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateActorActress(ActorActressRequest model, int id)
        {
            var operation = new UpdateActorActressCommand(model, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteActorActress(int id)
        {
            var operation = new DeleteActorActressCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
