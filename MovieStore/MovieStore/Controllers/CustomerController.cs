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
    public class CustomerController : ControllerBase
    {
        private IMediator mediator;
        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerResponse>> CreateCustomer(CustomerRequest model)
        {
            var operation = new CreateCustomerCommand(model);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteCustomer(int id)
        {
            var operation = new DeleteCustomerCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
