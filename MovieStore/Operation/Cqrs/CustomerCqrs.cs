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
    public record CreateCustomerCommand (CustomerRequest model) : IRequest<ApiResponse<CustomerResponse>>;
    public record DeleteCustomerCommand (int Id) : IRequest<ApiResponse>;
}
