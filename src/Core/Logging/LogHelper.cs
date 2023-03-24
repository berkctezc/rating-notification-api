namespace Core.Logging;

[ExcludeFromCodeCoverage]
public class LogHelper<T>
{
    public static string GetLogTemplateError(LogDetail<T>? logDetail)
        => $"ERROR : HTTP {logDetail?.RequestMethod} {logDetail?.RequestPathAndQuery} responded {logDetail?.ResponseStatusCode}";

    public static string GetLogTemplateWithElapsed(LogDetail<T>? logDetail)
        => $"HTTP {logDetail?.RequestMethod} {logDetail?.RequestPathAndQuery} responded {logDetail?.ResponseStatusCode} in {logDetail?.ElapsedMilliseconds} ms";

    public static ILogger GetLogDetail(ILogger logger, LogDetail<T>? logDetail)
    {
        if (logDetail is null)
            return logger;

        logger = logger
            .ForContext(nameof(logDetail.MachineName), logDetail.MachineName)
            .ForContext(nameof(logDetail.RequestHost), logDetail.RequestHost)
            .ForContext(nameof(logDetail.RequestProtocol), logDetail.RequestProtocol)
            .ForContext(nameof(logDetail.RequestMethod), logDetail.RequestMethod)
            .ForContext(nameof(logDetail.ResponseStatusCode), logDetail.ResponseStatusCode.ToString())
            .ForContext(nameof(logDetail.RequestPath), logDetail.RequestPath)
            .ForContext(nameof(logDetail.RequestPathAndQuery), logDetail.RequestPathAndQuery)
            .ForContext(nameof(logDetail.CorrelationId), logDetail.CorrelationId);
        if (logDetail.RequestHeaders is not null && logDetail.RequestHeaders.Any())
            logger = logger.ForContext(nameof(logDetail.RequestHeaders), logDetail.RequestHeaders, true);

        if (logDetail.ElapsedMilliseconds is not null)
            logger = logger.ForContext(nameof(logDetail.ElapsedMilliseconds), logDetail.ElapsedMilliseconds.ToString());

        if (!string.IsNullOrEmpty(logDetail.RequestBody))
            logger = logger.ForContext(nameof(logDetail.RequestBody), logDetail.RequestBody);

        if (logDetail.Exception is not null)
            logger = logger.ForContext(nameof(logDetail.Exception), logDetail.Exception, true);

        return logger;
    }
}