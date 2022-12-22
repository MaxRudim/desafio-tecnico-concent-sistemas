using ConcentKitchen.Models;
using ConcentKitchen.Repository;
using ConcentKitchen.Middlewares;

namespace ConcentKitchen.Services;
public class ClientService : IClientService
{
    private readonly IClientRepository _repository;
    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task<Client> Login(LoginData loginData)
    {
        try
        {
            var client = await _repository.Login(loginData);
            if (client == null) throw new InvalidOperationException("Este cliente não existe");

            return client;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }
    }

    public async Task<Client> CreateClient(Client client)
    {
        try
        {
            var clientExist = await _repository.GetByCpf(client.Cpf);
            if (clientExist is not null) throw new InvalidOperationException("Este cliente já existe");

            var validCpf = ValidaCPF.IsCpf(client.Cpf);
            if (validCpf == false) throw new InvalidOperationException("Cpf inválido");

            var output = await _repository.Add(client);
            return output;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }

    public async Task DeleteClient(Guid id)
    {
        try
        {
            var clientExist = await _repository.Get(id);
            if (clientExist is null) throw new InvalidOperationException("Este cliente não existe");

            await _repository.Delete(id);
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<IEnumerable<Client>> GetAllClients()
    {
        try
        {
            var clients = await _repository.GetAll();
            if (!clients.Any()) throw new InvalidOperationException("Não existem clientes cadastrados");

            return clients;
        }
        catch (InvalidOperationException ex)
        {
          throw ex;
        }
    }
    
    public async Task<Client> GetClient(string id)
    {
        try
        {
            var client = await _repository.Get(new Guid(id));
            if (client == null) throw new InvalidOperationException("O cliente não existe");

            return client;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }
    }

    public async Task<Client> UpdateClient(Client client)
    {
        try
        {
            var clientExist = await _repository.Get(client.ClientId);
            if (clientExist == null) throw new InvalidOperationException("O cliente não existe");

            clientExist.Name = client.Name;
            clientExist.TableNumber = client.TableNumber;

            await _repository.Update(clientExist);

            var updatedClient = await _repository.Get(client.ClientId);
            return updatedClient!;
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }

    }
}