using System.IO;
using System.Linq;

namespace Solutions
{
    internal static class FileExtensions
    {
        public static string[] ReadAllLines(this string fileName)
        {
            string[] lines = File.ReadAllLines(fileName)
                                 .Select(x => x.Trim())
                                 .Where(x => !string.IsNullOrEmpty(x)).ToArray();
            return lines;
        }
    }
}
