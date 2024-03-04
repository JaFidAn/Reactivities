using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.UsersProfile.GetUserActivities
{
    public class GetUserActivitiesQueryRequest : IRequest<Result<List<UserActivityDto>>>
    {
        public string Username { get; set; }
        public string Predicate { get; set; }
    }
}