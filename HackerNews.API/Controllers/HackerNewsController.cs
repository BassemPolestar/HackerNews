using Microsoft.AspNetCore.Mvc;
using HackerNews.Domain;
using HackerNews.Domain.Entities;
using HackerNews.Infrastructure.Services;

namespace HackerNews.API.Controllers;

public class HackerNewsController: ControllerBase
{
    private readonly HackerNewsService _hackerNewsService;

    public HackerNewsController(HackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("best/{n}")]
    public async Task<ActionResult<IEnumerable<Story>>> GetBestStories(int n)
    {
        var bestStoryIds = await _hackerNewsService.GetBestStoryIdsAsync();
        var tasks = bestStoryIds.Take(n).Select(id => _hackerNewsService.GetStoryAsync(id));
        var stories = await Task.WhenAll(tasks);
        var sortedStories = stories.OrderByDescending(s => s.Score);
        return Ok(sortedStories);
    }
}