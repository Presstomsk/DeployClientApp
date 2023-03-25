using Microsoft.EntityFrameworkCore;
using OrdersApiAppSPD011.Data;
using OrdersApiAppSPD011.Model.Entity;

namespace OrdersApiAppSPD011.Service.ClientService
{
    public class DaoOrderProduct : IDao<OrderProduct>
    {
        private readonly ApplicationDbContext _context;

        public DaoOrderProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderProduct>> GetAllAsync()
        {
            return await _context.OrderProducts.AsNoTracking()                
                                               .Include(x => x.Product)
                                               .Include(x => x.Order)
                                               .ThenInclude(x => x.Client)
                                               .ToListAsync();
        }

        public async Task<OrderProduct?> GetAsync(int id)
        {
            var orderProduct = await _context.OrderProducts.AsNoTracking()
                                                           .Include(x => x.Product)
                                                           .Include(x => x.Order)
                                                           .ThenInclude(x => x.Client)                                                           
                                                           .SingleOrDefaultAsync(x => x.Id == id);
            return orderProduct;
        }

        public async Task<bool> AddAsync(OrderProduct orderProduct)
        {
            try
            {
                _context.Add(orderProduct);
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
                var orderProduct = await _context.OrderProducts.SingleOrDefaultAsync(x => x.Id == id);

                if (orderProduct == null) return false;

                _context.Remove(orderProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(OrderProduct orderProducts)
        {
            try
            {
                _context.Update(orderProducts);
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
