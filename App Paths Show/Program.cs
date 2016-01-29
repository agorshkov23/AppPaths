using System;
using System.Linq;
using AGorshkov23.AppPaths.Core.Enums;
using Microsoft.Win32;

namespace AGorshkov23.AppPaths.Show
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            if (args.Contains("--help"))
            {
                ShowHelp();
                return (int) ExitCode.Success;
            }

            var registry = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths");

            var keys = registry.GetSubKeyNames();
            foreach (var key in keys)
            {
                var subKey = registry.OpenSubKey(key);
                Console.WriteLine(key);
                var def = subKey.GetValue("", null) as string;
                if (def != null)
                    Console.WriteLine($"  Application path: {def}");
                Console.WriteLine();
            }

            return (int) ExitCode.Success;
        }

        private static void ShowHelp()
        {
            Console.WriteLine(Core.Properties.Resources.HelpAppPathsAdd);
        }
    }
}
