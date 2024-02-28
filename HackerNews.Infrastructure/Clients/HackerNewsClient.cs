using System.Text.Json;
using HackerNews.Domain.Entities;
using HackerNews.Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HackerNews.Infrastructure.Clients;

public class HackerNewsClient : IHackerNewsClient
{
    private readonly HttpClient _httpClient;
    private readonly ICacheClient _cacheClient;
    private readonly string _baseUrl;

    public HackerNewsClient(HttpClient httpClient, ICacheClient cacheClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _cacheClient = cacheClient;
        _baseUrl = configuration["HackerNewsApi:BaseUrl"];
    }

    public async Task<IEnumerable<Story>> GetBestStoriesAsync(int n)
    {
        return await _cacheClient.GetOrCreateAsync<IEnumerable<Story>>(
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
        var response = await _httpClient.GetAsync($"{_baseUrl}beststories.json");
        if (!response.IsSuccessStatusCode)
            return default;
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<int>>(content);
    }

    private async Task<Story?> GetStoryAsync(int storyId)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}item/{storyId}.json");
        if (!response.IsSuccessStatusCode)
            return default;
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<Story>(content);
    }
}