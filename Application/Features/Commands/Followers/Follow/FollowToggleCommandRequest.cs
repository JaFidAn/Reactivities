using Application.Core;
using MediatR;

namespace Application.Features.Commands.Followers.Follow
{
    public class FollowToggleCommandRequest : IRequest<Result<Unit>>
    {
        public string TargetUsername { get; set; }
    }
}