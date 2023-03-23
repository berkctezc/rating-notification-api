namespace Core.Exceptions;

[ExcludeFromCodeCoverage]
public class ConfigurationReadException : Exception
{
    public ConfigurationReadException(Exception? innerExp) :
        base(innerExp?.Message)
    {
    }
}