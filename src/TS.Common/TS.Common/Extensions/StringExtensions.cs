using System.Linq;

namespace TS.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Removes from the string line whitespaces and tabs
        /// </summary>
        /// <param name="line">Line</param>
        /// <returns>Formatted line</returns>
        public static string? RemoveWhitespace(this string line)
        {
            if (line == null)
            {
                return null;
            }
            
            return string.Concat(line.Where(symbol => !char.IsWhiteSpace(symbol)));
        }
    }
}
