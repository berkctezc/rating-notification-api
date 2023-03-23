namespace Core.Middlewares;

public class RateLimitingMiddleware : IMiddleware
{
    private readonly ConcurrentDictionary<string, LinkedList<DateTime>> _requests = new();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var (limit, windowSize) = (10, TimeSpan.FromMinutes(1));

        var ipAddress = context.Connection.RemoteIpAddress!.ToString();

        if (!_requests.TryGetValue(ipAddress, out var requestList))
        {
            requestList = new LinkedList<DateTime>();
            _requests.TryAdd(ipAddress, requestList);
        }

        var now = DateTime.UtcNow;
        requestList.AddLast(now);

        while (requestList.Count > limit)
        {
            if (now - requestList.First!.Value > windowSize)
                requestList.RemoveFirst();
            else
            {
                context.Response.StatusCode = 429;
                return;
            }
        }

        await next(context);
    }
}