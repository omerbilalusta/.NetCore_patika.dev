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
    public class ActorActressCommandHandler :
        IRequestHandler<CreateActorActressCommand, ApiResponse<ActorActressResponse>>,
        IRequestHandler<UpdateActorActressCommand, ApiResponse>,
        IRequestHandler<DeleteActorActressCommand, ApiResponse>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public ActorActressCommandHandler(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<ActorActressResponse>> Handle(CreateActorActressCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<ActorActress>(request.model);

            var entity = await dbContext.Set<ActorActress>().AddAsync(mapped);
            entity.Entity.InsertDate = DateTime.Now;
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<ActorActressResponse>(entity.Entity);
            return new ApiResponse<ActorActressResponse>(result);
        }

        public async Task<ApiResponse> Handle(UpdateActorActressCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<ActorActress>().FirstOrDefaultAsync(x=> x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            entity.FirstName = request.model.FirstName;
            entity.LastName = request.model.LastName;
            entity.UpdateDate = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();

        }

        public async Task<ApiResponse> Handle(DeleteActorActressCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<ActorActress>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            entity.IsActive = false;
            entity.UpdateDate = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
