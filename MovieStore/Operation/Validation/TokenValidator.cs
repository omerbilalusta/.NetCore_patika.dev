using FluentValidation;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class TokenValidator : AbstractValidator<TokenRequest>
    {
        public TokenValidator()
        {
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("Firstname can not be empty").MinimumLength(2).MaximumLength(50).WithMessage("FirstName length can not be lower than 2 and more than 50!");
            RuleFor(x => x.Lastname).NotEmpty().WithMessage("Lastname can not be empty").MinimumLength(2).MaximumLength(50).WithMessage("LastName length can not be lower than 2 and more than 50!");
        }
    }
}
