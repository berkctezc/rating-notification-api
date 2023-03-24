namespace Core.Logging;

[ExcludeFromCodeCoverage]
public class LogDetail<T>
{
    public LogDetail(string requestHost, string requestProtocol, string requestMethod, string requestPath, string requestPathAndQuery, int? responseStatusCode, string correlationId)
    {
        Timestamp = DateTime.Now;
        RequestHost = requestHost;
        RequestProtocol = requestProtocol;
        RequestMethod = requestMethod;
        RequestPath = requestPath;
        RequestPathAndQuery = requestPathAndQuery;
        ResponseStatusCode = responseStatusCode ?? 0;
        CorrelationId = correlationId;
    }

    private DateTime Timestamp { get; set; }

    public string MachineName => Environment.MachineName;

    public string Message { get; set; } = string.Empty;

    public string RequestHost { get; set; }

    public string RequestProtocol { get; set; }

    public string RequestMethod { get; set; }

    public string RequestPath { get; set; }

    public string RequestPathAndQuery { get; set; }

    public int ResponseStatusCode { get; set; }

    public string CorrelationId { get; set; }

    public Dictionary<string, object>? RequestHeaders { get; set; }

    public long? ElapsedMilliseconds { get; set; }

    public string RequestBody { get; set; } = string.Empty;

    public T? Exception { get; set; }
}