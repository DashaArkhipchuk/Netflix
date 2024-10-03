using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain.Entities;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Genders.Queries.GetGenderById
{
    internal class GetGenderByIdQueryHandler : IRequestHandler<GetContentByIdQuery<Gender>, Gender?>
    {
        private readonly IGenderRepository _genderRepository;

        public GetGenderByIdQueryHandler(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }
        public Task<Gender?> Handle(GetContentByIdQuery<Gender> request, CancellationToken cancellationToken)
        {
            return _genderRepository.GetTypeById(request.Id);
        }
    }
}
