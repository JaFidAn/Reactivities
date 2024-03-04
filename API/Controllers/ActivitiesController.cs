using Application.Core;
using Application.Core.Activities;
using Application.Features.Commands.Activities.Create;
using Application.Features.Commands.Activities.Delete;
using Application.Features.Commands.Activities.Edit;
using Application.Features.Commands.Attendees.Update;
using Application.Features.Queries.Activities.GetAll;
using Application.Features.Queries.Activities.GetById;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetActivities([FromQuery] ActivityParams activityParams)
        {
            return HandlePagedResult(await Mediator.Send(new GetActivitiesQueryRequest { Params = activityParams }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new GetActivityByIdQueryRequest { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            return HandleResult(await Mediator.Send(new CreateActivityCommandRequest { Activity = activity }));
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditActivity(Guid id, Activity activity)
        {
            activity.Id = id;
            return HandleResult(await Mediator.Send(new EditActivityCommandRequest { Activity = activity }));

        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            return HandleResult(await Mediator.Send(new DeleteActivityCommandRequest { Id = id }));
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            return HandleResult(await Mediator.Send(new UpdateAttendanceCommandRequest { Id = id }));
        }
    }
}