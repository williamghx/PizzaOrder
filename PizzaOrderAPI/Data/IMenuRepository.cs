using PizzaOrderAPI.Models;

namespace PizzaOrderAPI.Data
{
    public interface IMenuRepository
    {
        Task<IEnumerable<MenuItem>> GetMenuByStore(int storeId);
        Task<MenuItem?> GetMenuItem(int id);
        Task<MenuItem> AddMenuItem(MenuUpdate menuUpdate);
        Task<MenuItem> UpdateStore(int id, MenuUpdate menuUpdate);
        Task<IEnumerable<Pizza>> GetAvailablePizzas(int storeId);
        Task<IEnumerable<Topping>> GetToppings();
    }
}
