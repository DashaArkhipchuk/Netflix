using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.EthnicAppearances.Queries.GetEthnicAppearanceById
{
    internal class GetEthnicAppearanceByIdQueryHandler : IRequestHandler<GetContentByIdQuery<EthnicAppearance>, EthnicAppearance?>
    {
        private readonly IEthnicAppearanceRepository _appearanceRepository;

        public GetEthnicAppearanceByIdQueryHandler(IEthnicAppearanceRepository appearanceRepository)
        {
            _appearanceRepository = appearanceRepository;
        }
        public Task<EthnicAppearance?> Handle(GetContentByIdQuery<EthnicAppearance> request, CancellationToken cancellationToken)
        {
            return _appearanceRepository.GetTypeById(request.Id);
        }
    }
}
