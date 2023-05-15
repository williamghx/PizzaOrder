using PizzaOrderAPI.Enums;
using PizzaOrderAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Fixtures
{
    public static class StoresFixture
    {
        public static List<Store> Stores = new List<Store>
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
    }
}
