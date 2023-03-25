using Microsoft.EntityFrameworkCore;
using OrdersApiAppSPD011.Data;
using OrdersApiAppSPD011.Model.Entity;

namespace OrdersApiAppSPD011.Service.ClientService
{
    public class DaoOrder : IDao<Order>
    {
        private readonly ApplicationDbContext _context;

        public DaoOrder(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(x => x.Client).AsNoTracking().ToListAsync();
        }

        public async Task<Order?> GetAsync(int id)
        {
            var order = await _context.Orders.Include(x => x.Client).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return order;
        }

        public async Task<bool> AddAsync(Order order)
        {
            try
            {
                _context.Add(order);
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
                var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);

                if (order == null) return false;                

                _context.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
