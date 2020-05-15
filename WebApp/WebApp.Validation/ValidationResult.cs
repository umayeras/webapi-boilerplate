namespace WebApp.Validation
{
    public class ValidationResult
    {
        #region ctor

        public bool IsValid { get; }

        public string ErrorMessage { get; }

        ValidationResult(bool isValid, string errorMessage = null)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        #endregion

        static readonly ValidationResult successResult = new ValidationResult(true);

        public static ValidationResult Success => successResult;

        public static ValidationResult Error(string errorMessage) => new ValidationResult(false, errorMessage);
    }
}
