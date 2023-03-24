namespace Domain.Constants;

[ExcludeFromCodeCoverage]
public static class OperationSystemConstants
{
    public static string Personal(string path)
        => RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}\\{path}"
            : $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}/{path}";
}