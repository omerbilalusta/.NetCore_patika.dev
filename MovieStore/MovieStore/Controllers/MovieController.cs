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
    public class MovieController : ControllerBase
    {
        private IMediator mediator;
        public MovieController(IMediator mediator )
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<MovieResponse>>> GetAllMovies()
        {
            var operation = new GetAllMovieQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<MovieResponse>> GetMovieById(int id)
        {
            var operation = new GetMovieByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<MovieResponse>> CreateMovie([FromQuery]MovieRequest model, [FromBody]MovieRequestActors modelActors)
        {
            var operation = new CreateMovieCommand(model, modelActors);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateMovie(MovieRequest model, int id)
        {
            var operation = new UpdateMovieCommand(model, id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteMovie(int id)
        {
            var operation = new DeleteMovieCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
