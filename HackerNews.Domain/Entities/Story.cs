using System.Text.Json.Serialization;
using HackerNews.Domain.Enum;

namespace HackerNews.Domain.Entities;

public class Story
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    
    [JsonPropertyName("score")]
    public int Score { get; set; }
    
    [JsonPropertyName("by")]
    public string By { get; set; }
    
    [JsonPropertyName("descendants")]
    public int Descendants { get; set; }
    
    [JsonPropertyName("time")]
    public int Time { get; set; }
    
    [JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StoryType Type { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("kids")]
    public IEnumerable<int> Kids { get; set; }
    
    [JsonPropertyName("deleted")]
    public bool Deleted { get; set; }
}