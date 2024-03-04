using Application.Core;
using MediatR;

namespace Application.Features.Commands.Photos.Delete
{
    public class DeletePhotoCommandRequest : IRequest<Result<Unit>>
    {
        public string Id { get; set; }
    }
}