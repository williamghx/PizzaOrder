using PizzaOrderAPI.Models;

namespace PizzaOrderAPI.Data
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllStores();
        Task<Store?> GetStore(int id);
        Task<Store> AddStore(Store store);
        Task<Store> UpdateStore(int id, StoreUpdate storeUpdate);
        Task<IEnumerable<Store>> GetStores(StoreQuery query);
    }
}
