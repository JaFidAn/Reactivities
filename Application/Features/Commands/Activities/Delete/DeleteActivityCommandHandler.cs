using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Features.Commands.Activities.Delete
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommandRequest, Result<Unit>>
    {
        private readonly DataContext _context;
        public DeleteActivityCommandHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<Result<Unit>> Handle(DeleteActivityCommandRequest request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Id);

            if (activity == null) return null;

            _context.Remove(activity);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to delete the activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}