using Application.Core;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.Photos.Add
{
    public class AddPhotoCommandRequest : IRequest<Result<Photo>>
    {
        public IFormFile File { get; set; }
    }
}