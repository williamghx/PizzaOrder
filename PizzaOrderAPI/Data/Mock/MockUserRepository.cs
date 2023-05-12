using PizzaOrderAPI.Models;

namespace PizzaOrderAPI.Data.Mock
{
    public class MockUserRepository: IUserRepository
    {
        private readonly IList<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "Test1",
                Password = "password1"
            },
            new User
            {
                Id = 2,
                UserName = "Test2",
                Password = "password2"
            }
        };

        public bool LoginUser(string username, string password)
        {
            return _users.Any(u => u.UserName == username && u.Password == password);
        }
    }
}
