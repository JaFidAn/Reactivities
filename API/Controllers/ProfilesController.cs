using Application.Features.Queries.UsersProfile.GetProfileByUsername;
using Application.Features.Queries.UsersProfile.GetUserActivities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new GetProfileByUsernameQueryRequest { Username = username }));
        }

        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivities(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new GetUserActivitiesQueryRequest { Username = username, Predicate = predicate }));
        }
    }
}