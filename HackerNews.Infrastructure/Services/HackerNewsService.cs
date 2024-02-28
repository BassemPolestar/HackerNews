using System.Text.Json;
using HackerNews.Domain;
using HackerNews.Domain.Entities;
using HackerNews.Infrastructure.Interfaces;

namespace HackerNews.Infrastructure.Services;

public class HackerNewsService : IHackerNewsService
{
    private readonly HttpClient _httpClient;
    private readonly ICacheService _cacheService;

    public HackerNewsService(HttpClient httpClient, ICacheService cacheService)
    {
        _httpClient = httpClient;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Story>> GetBestStoriesAsync(int n)
    {
        return await _cacheService.GetOrCreateAsync<IEnumerable<Story>>(
            $"BestStories_{n}",
            async () =>
            {
                var storyIds = await GetBestStoryIdsAsync();
                var tasks = storyIds?.Take(n).Select(GetStoryAsync);
                if (tasks != null)
                {
                    var stories = await Task.WhenAll(tasks);
                    return stories.OrderByDescending(s => s.Score);
                }

                return new List<Story>();
            },
            TimeSpan.FromMinutes(10)); // Cache for 10 minutes
    }

    private async Task<IEnumerable<int>?> GetBestStoryIdsAsync()
    {
        var response = await _httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
        if (!response.IsSuccessStatusCode)
            return default;
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<int>>(content);
    }

    private async Task<Story?> GetStoryAsync(int storyId)
    {
        var response = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
        if (!response.IsSuccessStatusCode)
            return default;
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Story>(content);
    }
}