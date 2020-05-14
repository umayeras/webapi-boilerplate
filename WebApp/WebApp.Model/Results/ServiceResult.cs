namespace WebApp.Model
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

        public static ServiceResult Success(string message = null)
        {
            return new ServiceResult(true, message);
        }

        public static ServiceResult Error(string message = null)
        {
            return new ServiceResult(false, message);
        }

        #endregion
    }
}
