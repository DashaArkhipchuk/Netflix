using MediatR;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.ProjectTypes.Queries.GetAllProjectTypes
{
    internal class GetAllProjectTypesQueryHandler : IRequestHandler<GetAllContentWithoutFilteringQuery<ProjectType>, List<ProjectType>>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        public GetAllProjectTypesQueryHandler(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        public Task<List<ProjectType>> Handle(GetAllContentWithoutFilteringQuery<ProjectType> request, CancellationToken cancellationToken)
        {
            return _projectTypeRepository.GetAllAsync(request.Skip, request.Take);
        }
    }
}
