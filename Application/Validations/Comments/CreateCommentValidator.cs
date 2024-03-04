using Application.Features.Commands.Comments.Create;
using FluentValidation;

namespace Application.Validations.Comments
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentCommandRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.Body).NotEmpty();
            RuleFor(x => x.Body).Length(2, 250).WithMessage("Comment can not be less than 1 character");
        }
    }
}