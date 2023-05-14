using PizzaOrderAPI.Models;

namespace PizzaOrderAPI.Data
{
    public interface IUserRepository
    {
        User CreateUser(UserDTO newUser);
        User? LoginUser(string username, string password);
    }
}
