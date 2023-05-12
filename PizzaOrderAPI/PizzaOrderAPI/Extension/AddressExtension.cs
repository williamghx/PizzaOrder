using PizzaOrderAPI.Models;

namespace PizzaOrderAPI.Extension
{
    public static class AddressExtension
    {
        public static string GetFullAddress(this Address address)
        {
            return $"{address.StreetNo} {address.StreetName} {address.StreetType.ToString()}, {address.Suburb}, {address.State.ToString()} {address.Postcode}";
        }
    }
}
