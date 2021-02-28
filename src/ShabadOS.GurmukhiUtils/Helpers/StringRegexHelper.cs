using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShabadOS.GurmukhiUtils.Helpers
{
    internal static class StringRegexHelper
    {
        public static string GetRegexGroup(IEnumerable<string> characters) =>
            $"({string.Join("|", characters.Select(Regex.Escape))})";
        
        public static string GetRegexClass(IEnumerable<string> characters) =>
            EscapeCharsInsideClass($"{string.Join("", characters.Select(Regex.Escape))}");

        // There are characters that C# Regex.Escape doesn't escape.
        // Inside a character class, only 4 chars require escaping, ^, -, ] and \.
        // However we don't really need to escape \ in GurmukhiUtils.
        private static string EscapeCharsInsideClass(params string[] values) =>
            $"[{string.Concat(values).Replace("^", @"\^").Replace("-", @"\-").Replace("]", @"\]")}]";
    }
}