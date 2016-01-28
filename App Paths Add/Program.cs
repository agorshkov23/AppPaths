using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace AGorshkov23.AppPaths.Add
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var shortName = args[0];
            var filePath = args[1];
            var fullPath = Path.GetFullPath(filePath);

            var registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths", true);
            var key = registry.GetSubKeyNames().Contains(shortName)
                ? registry.OpenSubKey(shortName, true)
                : registry.CreateSubKey(shortName);

            key.SetValue("", fullPath, RegistryValueKind.String);
        }
    }
}