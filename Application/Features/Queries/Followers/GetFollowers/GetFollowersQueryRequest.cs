using Application.Core;
using MediatR;

namespace Application.Features.Queries.Followers.GetFollowers
{
    public class GetFollowersQueryRequest : IRequest<Result<List<Profiles.Profile>>>
    {
        public string Username { get; set; }
        public string Predicate { get; set; }
    }
}