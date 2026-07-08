using System;
using System.Net.Http;
using System.Text;

namespace NLog.Slack;

public class SlackClient
{
    private static readonly HttpClient _httpClient = CreateHttpClient();

    public event Action<Exception> Error;

    public void Send(string url, string data)
    {
        try
        {
            using var content = new StringContent(data, Encoding.UTF8, "application/json");
            using var request = new HttpRequestMessage { Method = HttpMethod.Post, RequestUri = new Uri(url), Content = content };
            using var response = _httpClient.Send(request);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            OnError(e);
        }
    }

    private void OnError(Exception obj)
    {
        Error?.Invoke(obj);
    }

    private static HttpClient CreateHttpClient()
    {
        var handler = new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2), // To pick up DNS changes reasonably quickly
            PooledConnectionIdleTimeout = TimeSpan.FromMinutes(1),
            MaxConnectionsPerServer = 2
        };

        return new HttpClient(handler, disposeHandler: true);
    }
}