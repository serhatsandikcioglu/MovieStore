using FluentValidation;
using MovieStore.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Validatior
{
    public class MovieUpdateDTOValidator : AbstractValidator<MovieUpdateDTO>
    {
        public MovieUpdateDTOValidator() {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name cannot be empty");
            RuleFor(x => x.Year).NotEmpty().WithMessage("Year cannot be empty");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category cannot be empty");
            RuleFor(x => x.DirectorId).NotEmpty().WithMessage("Director id cannot be empty");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price cannot be empty");
        }
    }
}
