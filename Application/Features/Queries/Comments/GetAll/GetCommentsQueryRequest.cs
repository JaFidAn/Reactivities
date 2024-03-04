using Application.Core;
using Application.DTOs;
using MediatR;

namespace Application.Features.Queries.Comments.GetAll
{
    public class GetCommentsQueryRequest : IRequest<Result<List<CommentDto>>>
    {
        public Guid ActivityId { get; set; }
    }
}