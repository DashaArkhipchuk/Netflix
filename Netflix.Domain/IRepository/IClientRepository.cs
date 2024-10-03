namespace Netflix.Domain.IRepository
{
    public interface IClientRepository
    {
        Task<Client?> GetClientByEmailAsync(string email);

        Task<Client?> GetClientByIdAsync(Guid id);
        void Add(Client client);

    }
}
