namespace WebApp.Core.Caching
{
    public interface ICachingService
    {
        T Get<T>(string key);

        void Set(string key, object value);

        void Delete(string key);

        void Refresh(string key, object value);

        bool Exists(string key);
    }
}