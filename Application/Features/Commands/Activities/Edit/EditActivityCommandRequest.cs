using Application.Core;
using Domain;
using MediatR;

namespace Application.Features.Commands.Activities.Edit
{
    public class EditActivityCommandRequest : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
}