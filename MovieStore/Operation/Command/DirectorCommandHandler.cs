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
    public class DirectorCommandHandler :
        IRequestHandler<CreateDirectorCommand, ApiResponse<DirectorResponse>>,
        IRequestHandler<UpdateDirectorCommand, ApiResponse>,
        IRequestHandler<DeleteDirectorCommand, ApiResponse>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public DirectorCommandHandler(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<DirectorResponse>> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<Director>(request.model);

            var entity = await dbContext.Set<Director>().AddAsync(mapped, cancellationToken);
            entity.Entity.InsertDate = DateTime.Now;
            await dbContext.SaveChangesAsync(cancellationToken);

            var result = mapper.Map<DirectorResponse>(entity.Entity);
            return new ApiResponse<DirectorResponse>(result);

        }

        public async Task<ApiResponse> Handle(UpdateDirectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Director>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            entity.UpdateDate = DateTime.Now;
            entity.FirstName = request.model.FirstName;
            entity.LastName = request.model.LastName;
            
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();

        }

        public async Task<ApiResponse> Handle(DeleteDirectorCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Director>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record not found");

            entity.IsActive = false;
            entity.UpdateDate = DateTime.Now;

            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
