using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Queries.Followers.GetFollowers
{
    public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQueryRequest, Result<List<Profiles.Profile>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public GetFollowersQueryHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<Profiles.Profile>>> Handle(GetFollowersQueryRequest request, CancellationToken cancellationToken)
        {
            var profiles = new List<Profiles.Profile>();

            switch (request.Predicate)
            {
                case
                    "followers":
                    profiles = await _context.UserFollowings
                        .Where(x => x.Target.UserName == request.Username)
                        .Select(u => u.Observer)
                        .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.Getusername() })
                        .ToListAsync();
                    break;

                case
                    "following":
                    profiles = await _context.UserFollowings
                        .Where(x => x.Observer.UserName == request.Username)
                        .Select(u => u.Target)
                        .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.Getusername() })
                        .ToListAsync();
                    break;
            }

            return Result<List<Profiles.Profile>>.Success(profiles);
        }
    }
}