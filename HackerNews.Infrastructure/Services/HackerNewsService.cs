using System.Text.Json;
using HackerNews.Domain;

namespace HackerNews.Infrastructure.Services;

public class HackerNewsService
{
    private readonly HttpClient _httpClient;

    public HackerNewsService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<int>?> GetBestStoryIdsAsync()
    {
        var response = await _httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<int>>(content);
    }

    public async Task<Story?> GetStoryAsync(int storyId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Story>(content);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}