using MediatR;
using Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetAllCastingCalls
{
    internal class GetAllCastingCallsQueryHandler : IRequestHandler<GetAllCastingCallsQuery, List<CastingCall>>
    {
        private readonly ICastingCallRepository _castingCallRepository;

        public GetAllCastingCallsQueryHandler(ICastingCallRepository castingCallRepository)
        {
            _castingCallRepository = castingCallRepository;
        }
        public Task<List<CastingCall>> Handle(GetAllCastingCallsQuery request, CancellationToken cancellationToken)
        {
            return _castingCallRepository.GetAllAsync(request.Skip, request.Take, request.Criteria?.Locations ?? new List<string>(), request.Criteria?.PlayableAgeRanges ?? new List<string>(), request.Criteria?.ProjectTypes ?? new List<string>(), request.Criteria?.RoleTypes ?? new List<string>());
        }
    }
}
