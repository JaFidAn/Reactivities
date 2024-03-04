using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.Commands.Comments.Create
{
    public class CreateCommentCommandRequest : IRequest<Result<CommentDto>>
    {
        public string Body { get; set; }
        public Guid ActivityId { get; set; }
    }
}