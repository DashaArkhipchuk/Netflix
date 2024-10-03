using Microsoft.EntityFrameworkCore;
using Netflix.Domain;
using Netflix.Domain.IRepository;

namespace Netflix.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly NetflixProjectContext _dbContext;

        public ClientRepository(NetflixProjectContext dbContext) => _dbContext = dbContext;

        public void Add(Client client)
        {
            _dbContext.Clients.Add(client);
            _dbContext.SaveChanges();
        }

        public async Task<Client?> GetClientByEmailAsync(string email)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(client => client.Email == email);
        }

        public async Task<Client?> GetClientByIdAsync(Guid id)
        {
            return await _dbContext.Clients.Include(c=>c.Actor).Include(c=>c.CastingDirector).ThenInclude(d=>d.CastingDirectorType).SingleOrDefaultAsync(client => client.Id == id);
        }
    }
}
