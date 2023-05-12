using PizzaOrderAPI.Exceptions;
using PizzaOrderAPI.Models;
using PizzaOrderAPI.Extension;

namespace PizzaOrderAPI.Data.Mock
{
    public class MockMenuRepository : IMenuRepository
    {
        private readonly IList<Pizza> _pizzas;
        private readonly IList<Topping> _toppings;
        private readonly IList<MenuItem> _menu;
        private readonly IStoreRepository _storeRepository;
        public MockMenuRepository(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;

            _pizzas = new List<Pizza>
            {
                new Pizza
                {
                    Id = 1,
                    Name = "Capricciosa",
                    Description = "Cheese, Ham, Mushrooms, Olives"
                },
                new Pizza{
                    Id = 2,
                    Name = "Mexicana",
                    Description = "Cheese, Salami, Capsicum, Chilli"
                },
                new Pizza
                {
                    Id = 3,
                    Name = "Margherita",
                    Description = " Cheese, Spinach, Ricotta, Cherry Tomatoes"
                },
                new Pizza
                {
                    Id = 4,
                    Name = "Vegetarian",
                    Description = "Cheese, Mushrooms, Capsicum, Onion, Olives"
                }
            };

            _toppings = new List<Topping>
            {
                new Topping
                {
                    Id = 1,
                    Name = "Cheese",
                    Price = 1
                },
                new Topping
                {
                    Id = 2,
                    Name = "Capsicum",
                    Price = 1
                },
                new Topping
                {
                    Id = 3,
                    Name = "Salami",
                    Price = 1
                },
                new Topping
                {
                    Id = 4,
                    Name = "Olives",
                    Price = 1
                }
            };

            _menu = new List<MenuItem>
            {
                new MenuItem
                {
                    Id = 1,
                    StoreId = 1,
                    PizzaId = 1,
                    Price = 20
                },
                new MenuItem
                {
                    Id = 2,
                    StoreId = 1,
                    PizzaId = 2,
                    Price = 18
                },
                new MenuItem
                {
                    Id = 3,
                    StoreId = 1,
                    PizzaId = 3,
                    Price = 22
                },
                new MenuItem
                {
                    Id = 4,
                    StoreId = 2,
                    PizzaId = 1,
                    Price = 25
                },
                new MenuItem
                {
                    Id = 5,
                    StoreId = 2,
                    PizzaId = 4,
                    Price = 17
                }
            };
        }

        public async Task<MenuItem> AddMenuItem(MenuUpdate menuUpdate)
        {
            if(menuUpdate.StoreId == null)
            {
                throw new CustomException("The storeId is not provided in the request.");
            }

            if((await _storeRepository.GetStore((int)menuUpdate.StoreId)) == null)
            {
                throw new CustomException("The specifed store does not exist.");
            }

            if(menuUpdate.Price == null)
            {
                throw new CustomException("The price is not provided.");
            }

            if(menuUpdate.Pizza != null && !string.IsNullOrEmpty(menuUpdate.Pizza.Name))
            {
                if(_pizzas.Any(p => p.Name.Trim().ToLower() == menuUpdate.Pizza.Name.Trim().ToLower()))
                    throw new CustomException("The pizza name already exists.");

                menuUpdate.Pizza.Id = _pizzas.Max(p => p.Id) + 1;
                _pizzas.Add(menuUpdate.Pizza);
                    
                var newMenuItem = new MenuItem
                {
                    Id = _menu.Max(m => m.Id) + 1,
                    StoreId = (int)menuUpdate.StoreId,
                    PizzaId = menuUpdate.Pizza.Id,
                    Price = (decimal)menuUpdate.Price
                };
                _menu.Add(newMenuItem);
                newMenuItem.Store = await _storeRepository.GetStore(newMenuItem.StoreId);
                newMenuItem.Pizza = _pizzas.FirstOrDefault(p => p.Id == newMenuItem.PizzaId);
                return newMenuItem;
            }
            else if(menuUpdate.PizzaId != null)
            {
                if(!_pizzas.Any(p => p.Id == menuUpdate.PizzaId))
                    throw new CustomException("The specified pizza does not exist.");
                if(_menu.Any(m => m.StoreId == menuUpdate.StoreId && m.PizzaId == menuUpdate.PizzaId))
                    throw new CustomException("The menu item with the specified store id and pizza id already exists.");

                var newMenuItem = new MenuItem
                {
                    Id = _menu.Max(m => m.Id) + 1,
                    StoreId = (int)menuUpdate.StoreId,
                    PizzaId = (int)menuUpdate.PizzaId,
                    Price = (decimal)menuUpdate.Price
                };
                _menu.Add(newMenuItem);
                newMenuItem.Store = await _storeRepository.GetStore(newMenuItem.StoreId);
                newMenuItem.Pizza = _pizzas.FirstOrDefault(p => p.Id == newMenuItem.PizzaId);
                return newMenuItem;
            }
            else
            {
                throw new CustomException("Neither the existing pizza id or a new pizza is specified in the request.");
            }
        }

        public async Task<IEnumerable<MenuItem>> GetMenuByStore(int storeId)
        {
            var menu = _menu.Where(m => m.StoreId == storeId).Select(async m => 
                new MenuItem
                {
                    Id = m.Id,
                    StoreId = m.StoreId,
                    PizzaId = m.PizzaId,
                    Store = await _storeRepository.GetStore(m.StoreId),
                    Pizza = _pizzas.FirstOrDefault(p => p.Id == m.PizzaId),
                    Price = (decimal)m.Price
                }
            );
            return await Task.WhenAll(menu);
        }

        public async Task<MenuItem?> GetMenuItem(int id)
        {
            var menuItem = _menu.FirstOrDefault(m => m.Id == id);
            if(menuItem != null)
                menuItem = new MenuItem
                {
                    Id= menuItem.Id,
                    StoreId = menuItem.StoreId,
                    PizzaId = menuItem.PizzaId,
                    Store = await _storeRepository.GetStore(menuItem.StoreId),
                    Pizza = _pizzas.FirstOrDefault(p => p.Id == menuItem.PizzaId)
                };
            return menuItem;
        }

        public Task<IEnumerable<Topping>> GetToppings()
        {
            IEnumerable<Topping> toppings = _toppings;
            return Task.FromResult(toppings);
        }

        public async Task<MenuItem> UpdateStore(int id, MenuUpdate menuUpdate)
        {
            if(!_menu.Any(m => m.Id == id))
            {
                throw new NotFoundException("Menu Item");
            }

            var menuItem = _menu.FirstOrDefault(m => m.Id == id);
            menuItem.Update(menuUpdate);
            var pizza = _pizzas.FirstOrDefault(p => p.Id == menuItem.PizzaId);
            pizza.Update(menuUpdate);
            return new MenuItem
            {
                Id = menuItem.Id,
                StoreId = menuItem.StoreId,
                PizzaId = menuItem.PizzaId,
                Price = menuItem.Price,
                Pizza = pizza,
                Store = await _storeRepository.GetStore(menuItem.StoreId)
            };
        }
    }
}
