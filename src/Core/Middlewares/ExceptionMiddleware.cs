using ValidationException = FluentValidation;

namespace Core.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private static readonly ILogger Logger = Log.ForContext<ExceptionMiddleware>();

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException.ValidationException validationException)
        {
            ApiErrorDto apiErrorDto;

            if (validationException.Errors.Any())
            {
                apiErrorDto = new ApiErrorDto("request_model_is_invalid", validationException.Errors
                    .ToLookup(s => s.PropertyName, s => s.ErrorMessage)
                    .ToDictionary(s => s.Key, s => s.First()));

                var logMessage = string.Join('|',
                    validationException.Errors.Select(s => $"{s.PropertyName}:{s.ErrorMessage}"));

                Logger.Warning("Validation failed. Errors: {@ValidationErrors}", logMessage);
            }
            else
            {
                apiErrorDto = new ApiErrorDto(validationException.Message, ApiErrorDto.ExceptionType.Validation);

                Logger.Warning("Validation failed with messages {@ValidationMessages}",
                    validationException.Message);
            }

            await WriteWarningLog(context, validationException);

            await ClearResponseAndBuildErrorDto(context, apiErrorDto).ConfigureAwait(false);
        }
        catch (HttpRequestException exception)
        {
            await WriteErrorLog(context, exception, (int?) exception.StatusCode);

            await ClearResponseAndBuildErrorDto(context, new ApiErrorDto($"{exception.Message}", ApiErrorDto.ExceptionType.System), (int?) exception.StatusCode).ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            await WriteErrorLog(context, exception, null);

            //todo: seperate 400 and 500
            await ClearResponseAndBuildErrorDto(context, new ApiErrorDto($"{exception.Message}", ApiErrorDto.ExceptionType.System), 500).ConfigureAwait(false);
        }
    }

    private static Task ClearResponseAndBuildErrorDto(HttpContext context, ApiErrorDto errorDto, int? statusCode = StatusCodes.Status400BadRequest)
    {
        var error = JsonSerializer.Serialize(errorDto);

        context.Response.Clear();
        context.Response.StatusCode = statusCode ?? 0;
        context.Response.ContentType = "application/json";
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

        return context.Response.WriteAsync(error, Encoding.UTF8);
    }

    private static Task WriteErrorLog<T>(HttpContext context, T exception, int? statusCode = StatusCodes.Status400BadRequest)
    {
        var request = context.Request;
        var logDetail = new LogDetail<T>(request.Host.Host, request.Protocol,
            request.Method, request.Path, request.GetEncodedPathAndQuery(), statusCode, Guid.NewGuid().ToString())
        {
            RequestHeaders = request.Headers.ToDictionary(h => h.Key, h => (object) h.Value.ToString()),
            RequestBody = string.Empty,
            Exception = exception
        };

        LogHelper<T>.GetLogDetail(Logger, logDetail).Error(LogHelper<T>.GetLogTemplateError(logDetail));

        return Task.CompletedTask;
    }

    private static Task WriteWarningLog<T>(HttpContext context, T exception, int statusCode = StatusCodes.Status400BadRequest)
    {
        var request = context.Request;

        var logDetail = new LogDetail<T>(request.Host.Host, request.Protocol, request.Method, request.Path, request.GetEncodedPathAndQuery(), statusCode, Guid.NewGuid().ToString())
        {
            RequestHeaders = request.Headers.ToDictionary(h => h.Key, h => (object) h.Value.ToString()),
            RequestBody = string.Empty,
            Exception = exception
        };

        LogHelper<T>.GetLogDetail(Logger, logDetail).Warning(LogHelper<T>.GetLogTemplateError(logDetail));

        return Task.CompletedTask;
    }
}