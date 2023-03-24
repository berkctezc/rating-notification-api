namespace Core.Dtos;

[ExcludeFromCodeCoverage]
public class ApiErrorDto
{
    public ApiErrorDto(string message, ExceptionType exceptionType)
    {
        Message = message;
        Type = exceptionType.ToString();
    }

    public ApiErrorDto(string message, Dictionary<string, string> errors)
    {
        Message = message;
        Errors = errors;
        Type = ExceptionType.Validation.ToString();
    }

    public string Message { get; set; }

    public Dictionary<string, string>? Errors { get; set; }

    public string Type { get; set; }

    public enum ExceptionType : int
    {
        Undefined = 0,
        Validation = 1,
        Info = 2,
        System = 3
    }
}