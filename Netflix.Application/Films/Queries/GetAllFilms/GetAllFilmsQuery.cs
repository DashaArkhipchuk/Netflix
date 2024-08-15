using MediatR;
using Netflix.Domain;

namespace Netflix.Application.Films.Queries.GetAllFilms
{
    public record GetAllFilmsQuery
    (

        int Skip = 0,
        int Take = 10
    ) : IRequest<List<Film>>;
}
