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

namespace Operation.Command
{
    public class MovieCommandHandler :
        IRequestHandler<CreateMovieCommand, ApiResponse<MovieResponse>>,
        IRequestHandler<UpdateMovieCommand, ApiResponse>,
        IRequestHandler<DeleteMovieCommand, ApiResponse>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public MovieCommandHandler(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<MovieResponse>> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<Movie>(request.Model);

            var entity = await dbContext.Set<Movie>().AddAsync(mapped, cancellationToken);
            entity.Entity.PublishDate = DateTime.Now;
            entity.Entity.InsertDate = DateTime.Now;
            await dbContext.SaveChangesAsync(cancellationToken);


            request.ModelActors.ActorActressId.ForEach(x=>
            {
                var item = new Movie_ActorActress()
                {
                    ActorActressId = x,
                    MovieId = entity.Entity.Id
                };
                dbContext.Set<Movie_ActorActress>().Add(item);
            });
            await dbContext.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<MovieResponse>(entity.Entity);
            return new ApiResponse<MovieResponse>(response);
        }

        public async Task<ApiResponse> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Movie>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            entity.UpdateDate = DateTime.Now;
            entity.DirectorId = request.Model.DirectorId;
            entity.GenreId = request.Model.GenreId;
            entity.Price = request.Model.Price;
            entity.Name = request.Model.Name;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();

        }

        public async Task<ApiResponse> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Movie>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            entity.IsActive = false;
            entity.UpdateDate = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
