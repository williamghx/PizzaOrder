using PizzaOrderAPI.Models;
using PizzaOrderAPI.Enums;
using PizzaOrderAPI.Extension;
using PizzaOrderAPI.Exceptions;

namespace PizzaOrderAPI.Data.Mock
{
    public class MockStoreRepository : IStoreRepository
    {
        private readonly IList<Store> _stores = new List<Store>
        {
            new Store
            {
                Id = 1,
                Name = "Preston Pizzeria",
                Location = new Address
                {
                    Id = 1,
                    StreetNo = "1",
                    StreetName = "Test",
                    StreetType = StreetType.St,
                    Suburb = "Preston",
                    State = State.QLD,
                    Postcode = "4000"
                },
                Phone = "0712345678"
            },
            new Store
            {
                Id = 2,
                Name = "Southbank Pizzeria",
                Location = new Address
                {
                    Id = 2,
                    StreetNo = "2",
                    StreetName = "Test",
                    StreetType = StreetType.Pl,
                    Suburb = "Southbank",
                    State = State.QLD,
                    Postcode = "4001"
                },
                Phone = "0787654321"
            }
        };

        public Task<Store> AddStore(Store store)
        {
            store.Id = _stores.Select(st => st.Id).Max() + 1;
            var modelState = store.Validate(_stores);
            if (modelState.IsValid)
            {
                store.Location.Id = _stores.Select(st => st.Location.Id).Max() + 1;
                _stores.Add(store);
            }
            else
            {
                throw new InvalidException("Store", modelState);
            }
            
            return Task.FromResult(store);
        }

        public Task<IEnumerable<Store>> GetAllStores()
        {
            IEnumerable<Store> res = _stores;
            return Task.FromResult(res);
        }

        public Task<Store?> GetStore(int id)
        {
            return Task.FromResult(_stores.FirstOrDefault(store => store.Id == id));
        }

        public Task<IEnumerable<Store>> GetStores(StoreQuery query)
        {
            return Task.FromResult(_stores.Where(st => st.Find(query)));
        }

        public Task<Store> UpdateStore(int id, StoreUpdate storeUpdate)
        {
            var store = _stores.FirstOrDefault(st => st.Id == id);
            if(store == null)
                throw new CustomException($"Store with id of {id} is not found.");
            store.Update(storeUpdate);
            return Task.FromResult(store);
        }
    }
}
