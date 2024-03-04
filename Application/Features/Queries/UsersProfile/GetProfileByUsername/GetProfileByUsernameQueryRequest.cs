using Application.Core;
using Application.Profiles;
using MediatR;

namespace Application.Features.Queries.UsersProfile.GetProfileByUsername
{
    public class GetProfileByUsernameQueryRequest : IRequest<Result<Profile>>
    {
        public string Username { get; set; }
    }
}