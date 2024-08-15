namespace Netflix.Domain.IRepository
{
    public interface IClientRepository
    {
        Client? GetClientByEmail(string email);
        void Add(Client client);

    }
}
