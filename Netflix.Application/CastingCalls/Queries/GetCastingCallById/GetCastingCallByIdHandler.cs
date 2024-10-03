using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingCalls.Queries.GetCastingCallById
{
    internal class GetCastingCallByIdHandler : IRequestHandler<GetContentByIdQuery<CastingCall>, CastingCall?>
    {
        private readonly ICastingCallRepository _castingCallRepository;

        public GetCastingCallByIdHandler(ICastingCallRepository castingCallRepository)
        {
            _castingCallRepository = castingCallRepository;
        }
        public Task<CastingCall?> Handle(GetContentByIdQuery<CastingCall> request, CancellationToken cancellationToken)
        {
            return _castingCallRepository.GetByIdAsync(request.Id);
        }
    }
}
