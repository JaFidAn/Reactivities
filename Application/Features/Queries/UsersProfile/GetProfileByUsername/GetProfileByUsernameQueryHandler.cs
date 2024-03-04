using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Application.Profiles;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Queries.UsersProfile.GetProfileByUsername
{
    public class GetProfileByUsernameQueryHandler : IRequestHandler<GetProfileByUsernameQueryRequest, Result<Profiles.Profile>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        public GetProfileByUsernameQueryHandler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<Profiles.Profile>> Handle(GetProfileByUsernameQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider, new { currentUsername = _userAccessor.Getusername() })
                .SingleOrDefaultAsync(x => x.Username == request.Username);

            return Result<Profiles.Profile>.Success(user);
        }
    }
}