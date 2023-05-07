namespace PizzaOrderAPI.Data
{
    public interface IUserRepository
    {
        bool LoginUser(string username, string password);
    }
}
