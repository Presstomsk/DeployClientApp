using OrdersApiAppSPD011.Model.Entity;

namespace OrdersApiAppSPD011.Service.ClientService
{
    public interface IDao<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<bool> AddAsync(T order);

        Task<bool> UpdateAsync(T order);

        Task<bool> DeleteAsync(int id);

        Task<T?> GetAsync(int id);
    }
}
