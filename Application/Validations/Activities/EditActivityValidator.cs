using Application.Features.Commands.Activities.Edit;
using FluentValidation;

namespace Application.Validations.Activities
{
    public class EditActivityValidator : AbstractValidator<EditActivityCommandRequest>
    {
        public EditActivityValidator()
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