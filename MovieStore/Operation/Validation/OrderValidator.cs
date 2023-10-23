using FluentValidation;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class OrderValidator : AbstractValidator<OrderRequest>
    {
        public OrderValidator() { 
            RuleFor(x=> x.CustomerId).NotEmpty().WithMessage("CustomerId can not be empty!");
        }
    }
}
