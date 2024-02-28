using HackerNews.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;

namespace HackerNews.Tests.Services;

[TestFixture]
public class MemoryCacheServiceTests
{
    private MemoryCacheService _memoryCacheService;
    private IMemoryCache _memoryCache;

    [SetUp]
    public void Setup()
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        _memoryCacheService = new MemoryCacheService(_memoryCache);
    }

    [Test]
    public async Task GetOrCreateAsync_WithExistingItem_ReturnsCachedItem()
    {
        // Arrange
        var key = "testKey";
        var expectedItem = "cachedItem";
        _memoryCache.Set(key, expectedItem, TimeSpan.FromMinutes(1));

        // Act
        var result = await _memoryCacheService.GetOrCreateAsync(
            key,
            () => Task.FromResult("shouldNotBeCalled"),
            TimeSpan.FromSeconds(10)); // This should not be called

        // Assert
        Assert.AreEqual(expectedItem, result);
    }

    [Test]
    public async Task GetOrCreateAsync_WithNonExistingItem_ReturnsCreatedItem()
    {
        // Arrange
        var key = "testKey";
        var expectedItem = "createdItem";

        // Act
        var result = await _memoryCacheService.GetOrCreateAsync(
            key,
            () => Task.FromResult(expectedItem),
            TimeSpan.FromSeconds(10));

        // Assert
        Assert.AreEqual(expectedItem, result);
    }

    [Test]
    public async Task GetOrCreateAsync_WithExpiredItem_ReturnsCreatedItem()
    {
        // Arrange
        var key = "testKey";
        var expectedItem = "createdItem";
        _memoryCache.Set(key, expectedItem, TimeSpan.FromTicks(1)); // Set cache expiration to 1 tick

        // Act
        var result = await _memoryCacheService.GetOrCreateAsync(
            key,
            () => Task.FromResult(expectedItem),
            TimeSpan.FromMilliseconds(10)); // Give some time for cache to expire

        // Assert
        Assert.AreEqual(expectedItem, result);
    }
}