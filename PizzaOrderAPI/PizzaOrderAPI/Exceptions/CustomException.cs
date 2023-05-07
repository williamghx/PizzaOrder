namespace PizzaOrderAPI.Exceptions
{
    public class CustomException: Exception
    {
        public CustomException(string message):base(message) 
        {

        }
    }

    public class NotFoundException: CustomException
    {
        public NotFoundException(string resource): base($"The specified {resource} is not found")
        {

        }
    }
}
