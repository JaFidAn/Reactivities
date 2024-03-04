using Application.Core;
using MediatR;

namespace Application.Features.Commands.Photos.SetMain
{
    public class SetMainPhotoCommandRequest : IRequest<Result<Unit>>
    {
        public string Id { get; set; }
    }
}