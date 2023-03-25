using Microsoft.EntityFrameworkCore;
using OrdersApiAppSPD011.Data;
using OrdersApiAppSPD011.Model.Entity;

namespace OrdersApiAppSPD011.Service.ClientService
{
    public class DaoProduct : IDao<Product>
    {
        private readonly ApplicationDbContext _context;

        public DaoProduct(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product?> GetAsync(int id)
        {
            var product = await _context.Products.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<bool> AddAsync(Product product)
        {
            try
            {
                _context.Add(product);
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
                var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == id);

                if (product == null) return false;

                _context.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            try
            {
                _context.Update(product);
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
