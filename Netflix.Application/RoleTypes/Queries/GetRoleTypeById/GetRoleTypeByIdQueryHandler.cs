
using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.RoleTypes.Queries.GetRoleTypeById
{
    internal class GetRoleTypeByIdQueryHandler : IRequestHandler<GetContentByIdQuery<RoleType>, RoleType?>
    {
        private readonly IRoleTypeRepository _roleTypeRepository;

        public GetRoleTypeByIdQueryHandler(IRoleTypeRepository roleTypeRepository)
        {
            _roleTypeRepository = roleTypeRepository;
        }
        public Task<RoleType?> Handle(GetContentByIdQuery<RoleType> request, CancellationToken cancellationToken)
        {
            return _roleTypeRepository.GetTypeById(request.Id);
        }
    }
}
