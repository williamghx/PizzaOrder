namespace PizzaOrderAPI.Auth
{
    public interface IJwtAuthenticationManager
    {
        string? Authenticate(string username, string password);
        (string?, string?) GetUser();
    }
}
