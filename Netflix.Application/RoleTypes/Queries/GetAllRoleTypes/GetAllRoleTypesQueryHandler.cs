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

namespace Netflix.Application.RoleTypes.Queries.GetAllRoleTypes
{
    internal class GetAllRoleTypesQueryHandler : IRequestHandler<GetAllContentWithoutFilteringQuery<RoleType>, List<RoleType>>
    {
        private readonly IRoleTypeRepository _roleTypeRepository;

        public GetAllRoleTypesQueryHandler(IRoleTypeRepository roleTypeRepository)
        {
            _roleTypeRepository = roleTypeRepository;
        }

        public Task<List<RoleType>> Handle(GetAllContentWithoutFilteringQuery<RoleType> request, CancellationToken cancellationToken)
        {
            return _roleTypeRepository.GetAllAsync(request.Skip, request.Take);
        }
    }
}
