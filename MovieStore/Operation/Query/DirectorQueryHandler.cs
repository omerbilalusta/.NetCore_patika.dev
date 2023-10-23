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
    public class DirectorQueryHandler :
        IRequestHandler<GetAllDirectorQuery, ApiResponse<List<DirectorResponse>>>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public DirectorQueryHandler(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<DirectorResponse>>> Handle(GetAllDirectorQuery request, CancellationToken cancellationToken)
        {
            var list = await dbContext.Set<Director>().ToListAsync(cancellationToken);

            var mappedList = mapper.Map<List<DirectorResponse>>(list);
            return new ApiResponse<List<DirectorResponse>>(mappedList);
        }
    }
}
