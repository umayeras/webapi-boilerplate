using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using WebApp.Validation.Abstract;

namespace WebApp.Validation
{
    public class RequestValidator : IRequestValidator
    {
        #region ctor

        private readonly IValidatorFactory validatorFactory;

        public RequestValidator(IValidatorFactory validatorFactory)
        {
            this.validatorFactory = validatorFactory;
        }

        #endregion

        public ValidationResult Validate<T>(T request) where T : class
        {
            if (request == null)
            {
                return ValidationResult.Error("InvalidRequest");
            }

            var validator = validatorFactory.GetValidator<T>();
            var result = validator.Validate(request);

            return result.IsValid 
                ? ValidationResult.Success 
                : ValidationError(result.Errors);
        }

        private static ValidationResult ValidationError(IEnumerable<ValidationFailure> errors)
        {
            var errorArray = errors.Select(error => error.ErrorMessage).ToArray();
            var errorMessage = string.Join("\n", errorArray);

            return ValidationResult.Error(errorMessage);
        }
    }
}