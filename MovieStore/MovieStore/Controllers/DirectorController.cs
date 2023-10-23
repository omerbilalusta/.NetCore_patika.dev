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
    public class DirectorController : ControllerBase
    {
        private IMediator mediator;
        public DirectorController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<DirectorResponse>>> GetAllDirectors()
        {
            var operation = new GetAllDirectorQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<DirectorResponse>> CreateDirector(DirectorRequest model)
        {
            var operation = new CreateDirectorCommand(model);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateDirector(DirectorRequest model, int id)
        {
            var operation = new UpdateDirectorCommand(model, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteDirector(int id)
        {
            var operation = new DeleteDirectorCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
