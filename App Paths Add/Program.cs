using System;
using System.IO;
using System.Linq;
using AGorshkov23.AppPaths.Core.Enums;
using Microsoft.Win32;

namespace AGorshkov23.AppPaths.Add
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            if (args.Length < 2)
            {
                ShowHelp();
                return (int) ExitCode.Success;
            }

            var shortName = args[0];
            var filePath = args[1];
            var fullPath = Path.GetFullPath(filePath);

            var currentVersion = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion", true);
            var registry = currentVersion.GetSubKeyNames().Contains("App Paths")
                ? currentVersion.OpenSubKey("App Paths", true)
                : currentVersion.CreateSubKey("App Paths");

            var key = registry.GetSubKeyNames().Contains(shortName)
                ? registry.OpenSubKey(shortName, true)
                : registry.CreateSubKey(shortName);

            key.SetValue("", fullPath, RegistryValueKind.String);
            return (int) ExitCode.Success;
        }

        private static void ShowHelp()
        {
            Console.WriteLine(Core.Properties.Resources.HelpAppPathsAdd);
        }
    }
}