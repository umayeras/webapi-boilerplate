using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceStack.Redis;
using WebApp.Core.Constants;

namespace WebApp.Core.Caching
{
    public class CachingService : ICachingService
    {
        #region ctor

        private IConfiguration Configuration { get; }
        private readonly ILogger<CachingService> logger;

        public CachingService(IConfiguration configuration,ILogger<CachingService> logger)
        {
            Configuration = configuration;
            this.logger = logger;
        }

        #endregion

        private RedisCacheProviderStatus ServiceStatus()
        {
            try
            {
                var redisSettings = Configuration.GetSection(SectionNames.RedisSettings).Get<RedisSettings>();
                var manager = new RedisManagerPool(redisSettings.Uri);

                using var client = manager.GetClient();
                var isClientAvailable = client.Ping();

                return !isClientAvailable
                    ? RedisCacheProviderStatus.NotAvailable
                    : RedisCacheProviderStatus.Available;
            }
            catch (Exception ex)
            {
                logger.LogError(nameof(ServiceStatus), ex.Message);
                return RedisCacheProviderStatus.NotAvailable;
            }
        }

        public T Get<T>(string key)
        {
            var serviceStatus = ServiceStatus();
            if (serviceStatus == RedisCacheProviderStatus.NotAvailable)
            {
                return JsonConvert.DeserializeObject<T>(string.Empty);
            }

            using var client = new RedisClient();
            var cachedDataString = client.GetValue(key);

            return JsonConvert.DeserializeObject<T>(cachedDataString);
        }

        public void Set(string key, object value)
        {
            var serviceStatus = ServiceStatus();
            if (serviceStatus == RedisCacheProviderStatus.NotAvailable)
            {
                return;
            }

            using var client = new RedisClient();
            var keyExists = Exists(key);
            if (keyExists)
            {
                return;
            }

            client.Set(key, value);
        }

        public void Delete(string key)
        {
            var serviceStatus = ServiceStatus();
            if (serviceStatus == RedisCacheProviderStatus.NotAvailable)
            {
                return;
            }

            using var client = new RedisClient();
            client.Delete(key);
        }

        public void Refresh(string key, object value)
        {
            var serviceStatus = ServiceStatus();
            if (serviceStatus == RedisCacheProviderStatus.NotAvailable)
            {
                return;
            }

            Delete(key);
            Set(key, value);
        }

        public bool Exists(string key)
        {
            var serviceStatus = ServiceStatus();
            if (serviceStatus == RedisCacheProviderStatus.NotAvailable)
            {
                return false;
            }

            using var client = new RedisClient();
            return client.SearchKeys(key).Count > 0;
        }
    }
}