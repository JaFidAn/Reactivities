using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Queries.Comments.GetAll
{
    public class GetCommentsQueryHandler : IRequestHandler<GetCommentsQueryRequest, Result<List<CommentDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GetCommentsQueryHandler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<CommentDto>>> Handle(GetCommentsQueryRequest request, CancellationToken cancellationToken)
        {
            var comments = await _context.Comments
                .Where(x => x.Activity.Id == request.ActivityId)
                .OrderByDescending(x => x.CreatedAt)
                .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return Result<List<CommentDto>>.Success(comments);
        }
    }
}