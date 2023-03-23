namespace Core.Dtos;

[ExcludeFromCodeCoverage]
public class ApiResponseDto<T>
{
    public T? Data { get; set; }
}