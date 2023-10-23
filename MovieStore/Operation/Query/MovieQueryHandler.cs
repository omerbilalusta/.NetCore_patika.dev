using AutoMapper;
using Base.Response;
using Data.Context;
using Data.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Operation.Cqrs;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Query
{
    public class MovieQueryHandler :
        IRequestHandler<GetAllMovieQuery, ApiResponse<List<MovieResponse>>>,
        IRequestHandler<GetMovieByIdQuery, ApiResponse<MovieResponse>>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public MovieQueryHandler(MovieStoreDbContext dbContext, IMapper mapper = null)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<MovieResponse>>> Handle(GetAllMovieQuery request, CancellationToken cancellationToken)
        {
            var movieList = await dbContext.Set<Movie>().ToListAsync(cancellationToken);

            var mapped = mapper.Map<List<MovieResponse>>(movieList);
            return new ApiResponse<List<MovieResponse>>(mapped);
        }

        public async Task<ApiResponse<MovieResponse>> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            var movie = await dbContext.Set<Movie>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            var mapped = mapper.Map<MovieResponse>(movie);
            return new ApiResponse<MovieResponse>(mapped);
        }
    }
}
