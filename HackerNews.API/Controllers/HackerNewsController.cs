using HackerNews.Application.Models;
using HackerNews.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.API.Controllers;

public class HackerNewsController: ControllerBase
{
    private readonly IHackerNewsService _hackerNewsService;

    public HackerNewsController(IHackerNewsService hackerNewsService)
    {
        _hackerNewsService = hackerNewsService;
    }

    [HttpGet("best/{n}")]
    public async Task<ActionResult<IEnumerable<StoryModel>>> GetBestStories(int n)
    {
        var bestStories = await _hackerNewsService.GetBestStoriesAsync(n);
        return Ok(bestStories);
    }
}