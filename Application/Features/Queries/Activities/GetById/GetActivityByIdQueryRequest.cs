using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.Activities.GetById
{
    public class GetActivityByIdQueryRequest : IRequest<Result<ActivityDto>>
    {
        public Guid Id { get; set; }
    }
}