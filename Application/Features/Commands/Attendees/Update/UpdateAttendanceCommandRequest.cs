using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using MediatR;

namespace Application.Features.Commands.Attendees.Update
{
    public class UpdateAttendanceCommandRequest : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
    }
}