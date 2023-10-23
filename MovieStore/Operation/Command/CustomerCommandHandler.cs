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
    public class CustomerCommandHandler :
        IRequestHandler<CreateCustomerCommand, ApiResponse<CustomerResponse>>,
        IRequestHandler<DeleteCustomerCommand, ApiResponse>
    {
        private readonly MovieStoreDbContext dbContext;
        private readonly IMapper mapper;

        public CustomerCommandHandler(MovieStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<CustomerResponse>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<Customer>(request.model);

            var entity = await dbContext.Set<Customer>().AddAsync(mapped);
            entity.Entity.InsertDate = DateTime.Now;
            entity.Entity.Role = "Customer";

            await dbContext.SaveChangesAsync(cancellationToken);

            var response = mapper.Map<CustomerResponse>(entity.Entity);
            return new ApiResponse<CustomerResponse>(response);
        }

        public async Task<ApiResponse> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await dbContext.Set<Customer>().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null)
                return new ApiResponse("Record Not Found");

            entity.IsActive = false;
            entity.UpdateDate = DateTime.Now;
            await dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse();
        }
    }
}
