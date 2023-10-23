using FluentValidation;
using Schema.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation.Validation
{
    public class MovieValidator : AbstractValidator<MovieRequest>
    {
        public MovieValidator()
        {
            RuleFor(x=> x.Name).NotEmpty().WithMessage("Name can not be empty").MinimumLength(2).WithMessage("Name length can not be lower than 2");
            RuleFor(x=>x.Price).NotEmpty().WithMessage("Price can not be empty").GreaterThan(0).WithMessage("Price can not be 0 or lower");
            RuleFor(x=> x.DirectorId).NotEmpty().WithMessage("DirectorId can not be empty").GreaterThan(0).WithMessage("DirectorId can not be lower than 0");
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("GenreId can not be empty").GreaterThan(0).LessThan(5).WithMessage("GenreId can not be greater 5 and lower than 0");
        }
    }
}
