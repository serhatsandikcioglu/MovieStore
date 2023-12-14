using FluentValidation;
using MovieStore.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Service.Validatior
{
    public class DirectorCreateDTOValidator : AbstractValidator<DirectorCreateDTO>
    {
        public DirectorCreateDTOValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname cannot be empty");
        }
    }
}
