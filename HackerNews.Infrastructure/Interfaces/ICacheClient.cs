namespace HackerNews.Infrastructure.Interfaces;

public interface ICacheClient
{
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> createItem, TimeSpan expirationTime);
}