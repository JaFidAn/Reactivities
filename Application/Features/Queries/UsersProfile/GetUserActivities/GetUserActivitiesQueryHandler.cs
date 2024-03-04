using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Features.Queries.UsersProfile.GetUserActivities
{
    public class GetUserActivitiesQueryHandler : IRequestHandler<GetUserActivitiesQueryRequest, Result<List<UserActivityDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GetUserActivitiesQueryHandler(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<Result<List<UserActivityDto>>> Handle(GetUserActivitiesQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _context.ActivityAttendees
                .Where(u => u.AppUser.UserName == request.Username)
                .OrderBy(d => d.Activity.Date)
                .ProjectTo<UserActivityDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            query = request.Predicate switch
            {
                "past" => query.Where(d => d.Date <= DateTime.UtcNow),
                "hosting" => query.Where(h => h.HostUsername == request.Username),
                _ => query.Where(d => d.Date >= DateTime.UtcNow)
            };

            var activities = await query.ToListAsync();

            return Result<List<UserActivityDto>>.Success(activities);
        }
    }
}