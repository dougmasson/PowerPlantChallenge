using System;
using System.Threading.Tasks;

namespace Powerplant.Core.Domain.Interface.Cache
{
    public interface ICacheService
    {
        Task<T> SetAsync<T>(string key, Func<Task<T>> factory);
        void Remove(string cacheKey);
    }
}
