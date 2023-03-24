namespace Core.Middlewares;

public class LoggingMiddleware : IMiddleware
{
    private static readonly ILogger Logger = Log.ForContext<LoggingMiddleware>();

    private Stopwatch? _sw;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _sw = Stopwatch.StartNew();

        await next(context);

        _sw.Stop();

        await WriteLog(context);
    }

    private Task WriteLog(HttpContext context)
    {
        var request = context.Request;

        var logDetail = new LogDetail<Exception>(request.Host.Host, request.Protocol, request.Method, request.Path, request.GetEncodedPathAndQuery(), context.Response.StatusCode, Guid.NewGuid().ToString())
        {
            ElapsedMilliseconds = _sw!.ElapsedMilliseconds,
            RequestHeaders = request.Headers.ToDictionary(h => h.Key, h => (object) h.Value.ToString()),
            RequestBody = string.Empty
        };

        LogHelper<Exception>.GetLogDetail(Logger, logDetail).Information(LogHelper<Exception>.GetLogTemplateWithElapsed(logDetail));

        return Task.CompletedTask;
    }
}