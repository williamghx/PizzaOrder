using PizzaOrderAPI.Models;
using PizzaOrderAPI.Enums;
using System.Collections.Generic;

namespace PizzaOrderAPI.Extension
{
    public static class StoreExtension
    {
        public static void Update(this Store store, StoreUpdate storeUpdate)
        {
            if(storeUpdate.Name != null)
                store.Name = storeUpdate.Name;
            if(storeUpdate.phone != null)
                store.Phone = storeUpdate.phone;
            if(storeUpdate.Location != null)
            {
                var location = storeUpdate.Location;
                if(location.StreetNo != null)
                    store.Location.StreetNo = location.StreetNo;
                if(location.StreetName != null)
                    store.Location.StreetName = location.StreetName;
                if(location.StreetType != null)
                    store.Location.StreetType = (StreetType)location.StreetType;
                if(location.Suburb != null)
                    store.Location.Suburb = location.Suburb;
                if(location.State != null)
                    store.Location.State = (State)location.State;
                if(location.Postcode != null)
                    store.Location.Postcode = location.Postcode; 
            }
        }

        public static bool Find(this Store store, StoreQuery query)
        {

            var q1 = store.Name?.Trim().ToLower() == query.Name?.Trim().ToLower();
            var q2 = store.Phone?.Trim().Replace(" ", "").Replace("(", "").Replace(")", "") == query.Phone?.Trim().Replace(" ", "").Replace("(", "").Replace(")", "");
            var q3 = store.Location.State == query.State;
            var q4 = store.Location.Postcode?.Trim() == query.Postcode?.Trim();
            var q5 = store.Location.Suburb?.Trim().ToLower() == query.Suburb?.Trim().ToLower();

            return q1 || q2 || q3 || q4 || q5;
        }
    }

    
}
