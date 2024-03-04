using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Features.Commands.Activities.Edit
{
    public class EditActivityCommandHandler : IRequestHandler<EditActivityCommandRequest, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EditActivityCommandHandler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Unit>> Handle(EditActivityCommandRequest request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id);

            if (activity == null) return null;

            _mapper.Map(request.Activity, activity);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to update activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}