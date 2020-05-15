using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using WebApp.Validation.Abstract;
using ValidationResult = WebApp.Validation.Abstract.ValidationResult;

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

            var result = ValidateCore(request);

            if (result.IsValid)
            {
                return ValidationResult.Success;
            }

            return ValidationError(result.Errors);
        }

        FluentValidation.Results.ValidationResult ValidateCore<T>(T model) where T : class
        {
            var validator = validatorFactory.GetValidator<T>();

            return validator.Validate(model);
        }

        static ValidationResult ValidationError(IEnumerable<ValidationFailure> errors)
        {
            var errorArray = errors.Select(error => error.ErrorMessage).ToArray();
            var errorMessage = string.Join("\n", errorArray);

            return ValidationResult.Error(errorMessage);
        }
    }
}
