using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;

public interface IClientRepository
{
  public Task<Client> Add(Client client);
  public Task Delete(Guid id);
  public Task Update(Client client);
  public Task<Client?> Get(Guid id);
  public Task<Client?> Login(LoginData logindata);
  public Task<Client?> GetByCpf(string cpf);
  public Task<IEnumerable<Client>> GetAll();
}