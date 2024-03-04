using Application.Core;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Commands.Comments.Create
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommandRequest, Result<CommentDto>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public CreateCommentCommandHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<CommentDto>> Handle(CreateCommentCommandRequest request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.ActivityId);

            if (activity == null) return null;

            var user = await _context.Users
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.Getusername());

            var comment = new Comment
            {
                Body = request.Body,
                Activity = activity,
                Author = user,
            };

            activity.Comments.Add(comment);

            var success = await _context.SaveChangesAsync() > 0;

            if (success) return Result<CommentDto>.Success(_mapper.Map<CommentDto>(comment));

            return Result<CommentDto>.Failure("Failed to add comment");
        }
    }
}