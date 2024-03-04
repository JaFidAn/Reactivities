using Application.Core;
using Domain;
using MediatR;

namespace Application.Features.Commands.Activities.Create
{
    public class CreateActivityCommandRequest : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
}