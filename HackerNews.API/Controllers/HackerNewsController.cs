using Microsoft.AspNetCore.Mvc;
using HackerNews.Domain.Entities;
using HackerNews.Infrastructure.Interfaces;

namespace HackerNews.API.Controllers;

public class HackerNewsController: ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("best/{n}")]
    public async Task<ActionResult<IEnumerable<Story>>> GetBestStories(int n)
    {
        var bestStories = await _hackerNewsService.GetBestStoriesAsync(n);
        return Ok(bestStories);
    }
}