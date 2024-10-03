using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netflix.Infrastructure.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly NetflixProjectContext _dbContext;

        public ActorRepository(NetflixProjectContext dbContext) => _dbContext = dbContext;

        public void Add(Actor actor)
        {
            _dbContext.Actors.Add(actor);
            _dbContext.SaveChanges();
        }

        public async Task<Actor?> GetProfileByClientId(Guid clientId)
        {
            return await _dbContext.Actors.Where(x => x.ClientId == clientId).SingleOrDefaultAsync();
        }
    }
}
