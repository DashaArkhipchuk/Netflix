using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.ProjectTypes.Queries.GetProjectTypesById
{
    internal class GetProjectTypeByIdQueryHandler : IRequestHandler<GetContentByIdQuery<ProjectType>, ProjectType?>
    {
        private readonly IProjectTypeRepository _projectTypeRepository;

        public GetProjectTypeByIdQueryHandler(IProjectTypeRepository projectTypeRepository)
        {
            _projectTypeRepository = projectTypeRepository;
        }

        public Task<ProjectType?> Handle(GetContentByIdQuery<ProjectType> request, CancellationToken cancellationToken)
        {
            return _projectTypeRepository.GetTypeById(request.Id);
        }
    }
}
