namespace HackerNews.Infrastructure.Interfaces;

public interface ICacheService
{
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem, TimeSpan expirationTime);
}