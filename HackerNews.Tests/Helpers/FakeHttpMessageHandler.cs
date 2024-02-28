using System.Net;

namespace HackerNews.Tests.Helpers;

public class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, Task<HttpResponseMessage>> _sendAsyncFunc;

    public FakeHttpMessageHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsyncFunc)
    {
        _sendAsyncFunc = sendAsyncFunc;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        return await _sendAsyncFunc(request);
    }
}