using FluentValidation;

namespace WebApp.Validation
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        protected static string GenerateMessage(string property, string message)
        {
            return string.Format(message, property);
        }
    }
}