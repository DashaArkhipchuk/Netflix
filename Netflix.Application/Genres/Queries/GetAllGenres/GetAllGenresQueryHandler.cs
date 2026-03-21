using MediatR;
using Netflix.Application.Common.Content;
using Netflix.Domain;
using Netflix.Domain.DTOs;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Application.Genres.Queries.GetAllGenres
{
    internal class GetAllGenresQueryHandler : IRequestHandler<GetAllContentWithOptionalPaginationQuery<GenreModel>, PagedResult<GenreModel>>
    {
        private readonly IGenreRepository _genreRepository;

        public GetAllGenresQueryHandler(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public Task<PagedResult<GenreModel>> Handle(GetAllContentWithOptionalPaginationQuery<GenreModel> request, CancellationToken cancellationToken)
        {
            return _genreRepository.GetAllAsync(request.Skip, request.Take);
        }
    }
}
