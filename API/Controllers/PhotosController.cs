using Application.Features.Commands.Photos.Add;
using Application.Features.Commands.Photos.Delete;
using Application.Features.Commands.Photos.SetMain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotosController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] AddPhotoCommandRequest request)
        {
            return HandleResult(await Mediator.Send(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new DeletePhotoCommandRequest { Id = id }));
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult(await Mediator.Send(new SetMainPhotoCommandRequest { Id = id }));
        }
    }
}