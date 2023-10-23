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
    public class ActorActressQueryHandler :
        IRequestHandler<GetAllActorActressQuery, ApiResponse<List<ActorActressResponse>>>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public ActorActressQueryHandler(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<ActorActressResponse>>> Handle(GetAllActorActressQuery request, CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<ActorActress>().ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<ActorActressResponse>>(list);
            return new ApiResponse<List<ActorActressResponse>>(mappedList);
        }
    }
}
