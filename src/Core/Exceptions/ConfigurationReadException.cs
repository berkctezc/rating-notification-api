namespace Core.Exceptions;

public class ConfigurationReadException : Exception
{
    public ConfigurationReadException(Exception? innerExp) :
        base(innerExp?.Message)
    {
    }
}