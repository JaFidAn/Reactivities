using Application.Core;
using Application.Core.Activities;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.Activities.GetAll
{
    public class GetActivitiesQueryRequest : IRequest<Result<PagedList<ActivityDto>>>
    {
        public ActivityParams Params { get; set; }
    }
}