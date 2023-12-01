using System.IO;

namespace Solutions
{
    internal static class FileExtensions
    {
        public static string[] ReadAllLines(this string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            return lines;
        }
    }
}
