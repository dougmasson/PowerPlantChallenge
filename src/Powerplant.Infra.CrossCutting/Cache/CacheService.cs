using Microsoft.Extensions.Caching.Distributed;
using Powerplant.Core.Domain.Interface.Cache;
using Serilog;
using System;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Powerplant.Infra.CrossCutting.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _cacheSettings;

        public CacheService(IDistributedCache distributedCaching)
        {
            _distributedCache = distributedCaching;
            _cacheSettings = new DistributedCacheEntryOptions();
        }

        public async Task<T> SetAsync<T>(string key, Func<Task<T>> factory)
        {
            var cacheEntryBytes = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrEmpty(cacheEntryBytes) == false)
            {
                return JsonSerializer.Deserialize<T>(cacheEntryBytes);
            }
            else
            {
                var factoryReturn = await factory();

                if (factoryReturn != null)
                {
                    int absoluteExpiration = 2;
                    _cacheSettings.SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpiration));

                    await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize(factoryReturn), _cacheSettings);

                    SetLog<T>(absoluteExpiration);
                }

                return factoryReturn;
            }
        }

        public async void Remove(string cacheKey)
        {
            await _distributedCache.RemoveAsync(cacheKey);
            Log.Information($"CACHE REMOVE: {cacheKey}");
        }

        private void SetLog<T>(int absoluteExpiration)
        {
            Type typeParameterType = typeof(T);

            Regex regex = new Regex(@"\[\[(.*?),");
            var match = regex.Match(typeParameterType.FullName);

            Log.Information($"CACHE ENTRY: { match.Groups[1].Value } | Absolute Expiration: { absoluteExpiration } minutes ");
        }

    }
}
