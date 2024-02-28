using HackerNews.Domain.Entities;

namespace HackerNews.Infrastructure.Interfaces;

public interface IHackerNewsService
{
    Task<IEnumerable<Story>> GetBestStoriesAsync(int n);
}