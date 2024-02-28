using HackerNews.Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Infrastructure.Services;

public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem, TimeSpan expirationTime)
    {
        if (!_memoryCache.TryGetValue(key, out T item))
        {
            item = await createItem();

            if (item != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(expirationTime);

                _memoryCache.Set(key, item, cacheEntryOptions);
            }
        }

        return item;
    }
}
