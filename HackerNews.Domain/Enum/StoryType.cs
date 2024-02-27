using System.Runtime.Serialization;

namespace HackerNews.Domain.Enum;

public enum StoryType
{
    [EnumMember(Value = "job")]
    Job,
    [EnumMember(Value = "story")]
    Story,
    [EnumMember(Value = "comment")]
    Comment,
    [EnumMember(Value = "poll")]
    Poll,
    [EnumMember(Value = "pollopt")]
    Pollopt
}