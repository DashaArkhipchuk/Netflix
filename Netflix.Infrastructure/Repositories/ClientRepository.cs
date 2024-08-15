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

        public Client? GetClientByEmail(string email)
        {
            return _dbContext.Clients.SingleOrDefault(client => client.Email == email);
        }
    }
}
