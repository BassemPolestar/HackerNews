using System.Net;
using HackerNews.Infrastructure.Services;
using HackerNews.Tests.Helpers;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;

namespace HackerNews.Tests.Services;

[TestFixture]
public class HackerNewsServiceTests
{
    private HttpClient _httpClient;
    private HackerNewsService _hackerNewsService;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient(new FakeHttpMessageHandler(async request =>
        {
            if (request.RequestUri.AbsoluteUri == "https://hacker-news.firebaseio.com/v0/beststories.json")
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("[123]")
                };
            }
            else if (request.RequestUri.AbsoluteUri == "https://hacker-news.firebaseio.com/v0/item/123.json")
            {
                return new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent("{\"title\":\"Test Story\",\"score\":100}")
                };
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }));
        
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "HackerNewsAPI");

        _hackerNewsService =
            new HackerNewsService(_httpClient, new MemoryCacheService(new MemoryCache(new MemoryCacheOptions())));
    }

    [Test]
    public async Task GetBestStoriesAsync_WithValidResponse_ReturnsBestStories()
    {
        // Act
        var stories = await _hackerNewsService.GetBestStoriesAsync(1);

        // Assert
        Assert.IsNotNull(stories);
        Assert.AreEqual(1, stories.Count());
        Assert.AreEqual("Test Story", stories.First().Title);
        Assert.AreEqual(100, stories.First().Score);
    }

    [Test]
    public async Task GetBestStoriesAsync_WithInvalidResponse_ReturnsEmptyList()
    {
        // Arrange
        _httpClient = new HttpClient(new FakeHttpMessageHandler(request => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound))));
        
        _hackerNewsService =
            new HackerNewsService(_httpClient, new MemoryCacheService(new MemoryCache(new MemoryCacheOptions())));

        // Act
        var stories = await _hackerNewsService.GetBestStoriesAsync(1);

        // Assert
        Assert.IsNotNull(stories);
        Assert.IsEmpty(stories);
    }

    [Test]
    public async Task GetBestStoriesAsync_WithException_ReturnsEmptyList()
    {
        // Arrange
        _httpClient = new HttpClient(new FakeHttpMessageHandler(request => throw new HttpRequestException("Request failed")));
        
        _hackerNewsService =
            new HackerNewsService(_httpClient, new MemoryCacheService(new MemoryCache(new MemoryCacheOptions())));

        // Act
        var exception = Assert.ThrowsAsync<HttpRequestException>(() => _hackerNewsService.GetBestStoriesAsync(1));
        
        
        // Assert
        Assert.That(exception.Message, Is.EqualTo("Request failed"));
    }
}