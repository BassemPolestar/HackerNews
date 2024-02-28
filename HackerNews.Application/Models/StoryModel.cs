namespace HackerNews.Application.Models;

public class StoryModel
{
    public string Title { get; set; }

    public int Score { get; set; }

    public string By { get; set; }

    public int Time { get; set; }
    
    public string Type { get; set; }

    public string Url { get; set; }
}