using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AGorshkov23.AppPaths
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            var runningProcess = Process.GetCurrentProcess().MainModule.FileName;
            var runningDirectory = Path.GetDirectoryName(runningProcess);

            if (args[0] == "add")
            {
                var process = new Process
                {
                    StartInfo =
                    {
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        FileName = $"{runningDirectory}\\ap-add.exe",
                        Arguments = string.Join(" ", args.Skip(1).Select(arg => $"\"{arg}\""))
                    }
                };

                process.Start();
                Console.Out.Write(process.StandardOutput.ReadToEnd());
                process.WaitForExit();
            }
            else
            {
                ShowHelp();
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Showing ap help...");
        }
    }
}