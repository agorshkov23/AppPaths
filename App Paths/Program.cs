﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using AGorshkov23.AppPaths.Core.Enums;

namespace AGorshkov23.AppPaths
{
    internal class Program
    {
        private static readonly string ProcessPath = Process.GetCurrentProcess().MainModule.FileName;
        private static readonly string ProcessDirectory = Path.GetDirectoryName(ProcessPath);

        private static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return (int) ExitCode.Success;
            }

            switch (args[0])
            {
                case "add":
                    return (int) ExecuteProcess(BuildProcess("ap-add.exe"));
                case "show":
                    return (int) ExecuteProcess(BuildProcess("ap-show.exe"));
            }
            ShowHelp();
            return (int) ExitCode.Success;
        }

        private static ExitCode ExecuteProcess(Process process)
        {
            process.Start();
            Console.Out.Write(process.StandardOutput.ReadToEnd());
            process.WaitForExit();
            if (process.ExitCode == 0)
                Console.WriteLine(@"The operation completed successfully.");
            return (int)ExitCode.Success;
        }

        private static Process BuildProcess(string fileName)
        {
            return new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    FileName = $"{ProcessDirectory}\\{fileName}",
                    Arguments = string.Join(" ", fileName.Skip(1).Select(arg => $"\"{arg}\""))
                }
            };
        }

        private static void ShowHelp()
        {
            Console.WriteLine(Core.Properties.Resources.HelpAppPaths);
        }
    }
}