using MediatR;
using Netflix.Application.Common.GetAllContentQueryWithoutFiltering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;

namespace Netflix.Application.Genders.Queries.GetAllGenders
{
    internal class GetAllGendersQueryHandler : IRequestHandler<GetAllContentWithoutFilteringQuery<Gender>, List<Gender>>
    {
        private readonly IGenderRepository _genderRepository;

        public GetAllGendersQueryHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        public Task<List<Gender>> Handle(GetAllContentWithoutFilteringQuery<Gender> request, CancellationToken cancellationToken)
        {
            return _genderRepository.GetAllAsync(request.Skip, request.Take);
        }
    }
}
