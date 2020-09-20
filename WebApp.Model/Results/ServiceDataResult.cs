namespace WebApp.Model.Results
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

        public static ServiceDataResult Success(object data)
        {
            return new ServiceDataResult(true, null, data);
        }
        
        public static ServiceDataResult Success(object data, string message)
        {
            return new ServiceDataResult(true, message, data);
        }

        public new static ServiceDataResult Error()
        {
            return new ServiceDataResult(false, null, null);
        }
        
        public new static ServiceDataResult Error(string message)
        {
            return new ServiceDataResult(false, message, null);
        }

        #endregion
    }
}