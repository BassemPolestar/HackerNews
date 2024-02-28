using HackerNews.Application.Models;
using HackerNews.Infrastructure.Interfaces;

namespace HackerNews.Application.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly IHackerNewsClient _hackerNewsClient;
    
    public HackerNewsService(IHackerNewsClient hackerNewsClient)
    {
        _hackerNewsClient = hackerNewsClient;
    }
    
    public async Task<IEnumerable<StoryModel>> GetBestStoriesAsync(int n)
    {
        var response = await _hackerNewsClient.GetBestStoriesAsync(n);
        return response.Select(story => new StoryModel
        {
            By = story.By,
            Score = story.Score,
            Title = story.Title,
            Url = story.Url,
            Type = story.Type.ToString(),
            Time = story.Time
        });
    }
}