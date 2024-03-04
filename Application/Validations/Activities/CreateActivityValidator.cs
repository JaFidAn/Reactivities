using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Commands.Activities.Create;
using FluentValidation;

namespace Application.Validations.Activities
{
    public class CreateActivityValidator : AbstractValidator<CreateActivityCommandRequest>
    {
        public CreateActivityValidator()
        {
            RuleFor(x => x.Activity.Title).NotEmpty();
            RuleFor(x => x.Activity.Date).NotEmpty();
            RuleFor(x => x.Activity.Description).NotEmpty();
            RuleFor(x => x.Activity.Category).NotEmpty();
            RuleFor(x => x.Activity.City).NotEmpty();
            RuleFor(x => x.Activity.Venue).NotEmpty();
        }
    }
}