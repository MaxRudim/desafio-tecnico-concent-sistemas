using Microsoft.EntityFrameworkCore;
using ConcentKitchen.Models;

namespace ConcentKitchen.Repository;
public class ClientRepository : IClientRepository
{
    protected readonly KitchenContext _context;
    public ClientRepository(KitchenContext context)
    {
        _context = context;
    }

    public async Task<Client> Add(Client client)
    {
        _context.Add(client);

        await _context.SaveChangesAsync();

        return client;
    }
    public async Task Delete(Guid id)
    {

        var result = _context.Clients!.Single(p => p.ClientId == id);

        _context.Remove(result);

        await _context.SaveChangesAsync();
    }

    public async Task Update(Client client)
    {
        _context.ChangeTracker.Clear();

        _context.Update(client);

        await _context.SaveChangesAsync();
    }   

    public async Task<Client?> Get(Guid id)
    {
        var client = await _context.Clients!.AsNoTracking().FirstOrDefaultAsync(a => a.ClientId == id);

        return client;
    }

    public async Task<Client?> GetByCpf(string cpf)
    {
        var client = await _context.Clients!.AsNoTracking().FirstOrDefaultAsync(a => a.Cpf == cpf);

        return client;
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        var client = await _context.Clients!.ToListAsync();

        return client;
    }

}