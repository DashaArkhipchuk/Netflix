using MediatR;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.CastingDirectorProfileTypes.Queries.GetAllCastingDirectorProfileTypes
{
    internal class GetAllCastingDirectorProfileTypesQueryHandler : IRequestHandler<GetAllContentWithoutFilteringQuery<CastingDirectorType>, List<CastingDirectorType>>
    {
        private ICastingDirectorTypeRepository _castingDirectorTypesRepository;

        public GetAllCastingDirectorProfileTypesQueryHandler(ICastingDirectorTypeRepository castingDirectorTypesRepository)
        {
            _castingDirectorTypesRepository = castingDirectorTypesRepository;
        }

        public async Task<List<CastingDirectorType>> Handle(GetAllContentWithoutFilteringQuery<CastingDirectorType> request, CancellationToken cancellationToken)
        {
            return await _castingDirectorTypesRepository.GetAllAsync(request.Skip, request.Take);
        }
    }
}
