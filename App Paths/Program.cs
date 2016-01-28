using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AGorshkov23.AppPaths
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var runningProcess = Process.GetCurrentProcess().MainModule.FileName;
            var runningDirectory = Path.GetDirectoryName(runningProcess);

            if (args[0] == "add")
            {
                Process.Start($"{runningDirectory}\\ap-add.exe", string.Join(" ", args.Skip(1).Select(arg => $"\"{arg}\"")));
            }
        }
    }
}