using HackerNews.Application.Models;

namespace HackerNews.Application.Services;

public interface IHackerNewsService
{
    Task<IEnumerable<StoryModel>> GetBestStoriesAsync(int n);
}