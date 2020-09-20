namespace WebApp.Model.Results
{
    public class ServiceResult
    {
        #region ctor

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public ServiceResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        #endregion

        #region factory methods

        public static ServiceResult Success()
        {
            return new ServiceResult(true, null);
        }

        public static ServiceResult Success(string message)
        {
            return new ServiceResult(true, message);
        }

        public static ServiceResult Error()
        {
            return new ServiceResult(false, null);
        }

        public static ServiceResult Error(string message)
        {
            return new ServiceResult(false, message);
        }

        #endregion
    }
}