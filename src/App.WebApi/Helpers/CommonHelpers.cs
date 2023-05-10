using System.Reflection;

namespace App.WebApi.Helpers;
public static class CommonHelpers
{
    public static string GetVersionNumber()
    {
        return Assembly.GetEntryAssembly()!
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
            .InformationalVersion;
    }

    public static string GetAppName() => Assembly.GetEntryAssembly()!.GetName().FullName;
}
