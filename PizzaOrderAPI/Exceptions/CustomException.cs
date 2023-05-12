using Microsoft.AspNetCore.Mvc.ModelBinding;

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

    public class InvalidException: CustomException
    {
        private readonly ModelStateDictionary _modelState;

        public ModelStateDictionary ModelState { get { return _modelState;} }

        public InvalidException(string entity, ModelStateDictionary validationResult): base($"The ${entity} is invalid")
        {
            _modelState = validationResult;
        }
    }
}
