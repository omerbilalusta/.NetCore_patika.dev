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
    public record CreateMovieCommand(MovieRequest Model, MovieRequestActors ModelActors) : IRequest<ApiResponse<MovieResponse>>;
    public record UpdateMovieCommand(MovieRequest Model, int Id) : IRequest<ApiResponse>;
    public record DeleteMovieCommand(int Id) : IRequest<ApiResponse>;
    public record GetAllMovieQuery() : IRequest<ApiResponse<List<MovieResponse>>>;
    public record GetMovieByIdQuery(int Id) : IRequest<ApiResponse<MovieResponse>>;

}
