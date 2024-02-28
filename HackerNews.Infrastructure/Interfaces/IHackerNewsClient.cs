using HackerNews.Domain.Entities;

namespace HackerNews.Infrastructure.Interfaces;

public interface IHackerNewsClient
{
    Task<IEnumerable<Story>> GetBestStoriesAsync(int n);
}