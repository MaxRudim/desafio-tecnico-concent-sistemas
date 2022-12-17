using ConcentKitchen.Models;

namespace ConcentKitchen.Services;

public interface IClientService
{
    public Task<Client> CreateClient(Client client);
    public Task DeleteClient(Guid id);
    public Task<Client> UpdateClient(Client client);
    public Task<IEnumerable<Client>> GetAllClients();
    public Task<Client> GetClient(string id);

}