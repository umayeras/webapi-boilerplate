namespace WebApp.Model
{
    public sealed class ServiceDataResult : ServiceResult
    {
        #region ctor

        public object Data { get; set; }

        public ServiceDataResult(bool isSuccess, string message, object data) : base(isSuccess, message)
        {
            Data = data;
        }

        #endregion

        #region factory methods

        public static ServiceDataResult Success(object data, string message = null)
        {
            return new ServiceDataResult(true, message, data);
        }

        public new static ServiceDataResult Error(string message = null)
        {
            return new ServiceDataResult(false, message, null);
        }

        #endregion
    }
}
