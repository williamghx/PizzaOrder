using PizzaOrderAPI.Models;
using PizzaOrderAPI.Enums;
using System.Collections.Generic;

namespace PizzaOrderAPI.Extension
{
    public static class MenuExtension
    {
        public static void Update(this MenuItem menuItem, MenuUpdate menuUpdate)
        {
            if(menuUpdate.Price != null)
                menuItem.Price = (decimal)menuUpdate.Price;
        }

        public static void Update(this Pizza pizza, MenuUpdate menuUpdate)
        {
            if(menuUpdate.Pizza != null)
            {
                pizza.Name = menuUpdate.Pizza.Name;
                pizza.Description = menuUpdate.Pizza.Description;
            }
        }
    }
}
