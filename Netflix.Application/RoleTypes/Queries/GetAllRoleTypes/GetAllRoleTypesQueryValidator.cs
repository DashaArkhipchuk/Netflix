using FluentValidation;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using Netflix.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.RoleTypes.Queries.GetAllRoleTypes
{
    public class GetAllRoleTypesQueryValidator : GetAllContentWithoutFilteringQueryGenericValidator<RoleType>
    {
        public GetAllRoleTypesQueryValidator() : base() { }
    }
}
