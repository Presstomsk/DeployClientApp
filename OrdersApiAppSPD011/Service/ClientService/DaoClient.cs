using Microsoft.EntityFrameworkCore;
using OrdersApiAppSPD011.Data;
using OrdersApiAppSPD011.Model.Entity;

namespace OrdersApiAppSPD011.Service.ClientService;

public class DaoClient : IDao<Client>
{
    private readonly ApplicationDbContext _context;

    public DaoClient(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await _context.Clients.AsNoTracking().ToListAsync();
    }

    public async Task<Client?> GetAsync(int id)
    {
        var client = await _context.Clients.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        return client;
    }

    public async Task<bool> AddAsync(Client client)
    {
        try
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var client = await _context.Clients.SingleOrDefaultAsync(x => x.Id == id);

            if (client == null) return false;          
            
            _context.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(Client client)
    {
        try
        {
            _context.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}