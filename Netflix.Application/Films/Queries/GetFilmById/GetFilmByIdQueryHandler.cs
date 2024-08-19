using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Application.Films.Queries.GetAllFilms;
using Netflix.Domain;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Films.Queries.GetFilmByIdQuery
{
    internal class GetFilmByIdQueryHandler : IRequestHandler<GetContentByIdQuery<Film>, Film?>
    {
        private readonly IFilmRepository _filmRepository;

        public GetFilmByIdQueryHandler(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        public Task<Film?> Handle(GetContentByIdQuery<Film> request, CancellationToken cancellationToken)
        {
            return _filmRepository.GetByIdAsync(request.Id);
        }
    }
}
