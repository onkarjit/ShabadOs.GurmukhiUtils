using System;
using System.Linq;
using System.Text.RegularExpressions;
using ShabadOS.GurmukhiUtils.Enums;
using ShabadOS.GurmukhiUtils.Helpers;
using ShabadOS.GurmukhiUtils.Mappings;

namespace ShabadOS.GurmukhiUtils
{
    public static partial class GurmukhiUtils
    {
        /// <summary>
        ///     Removes accents from ASCII/Unicode Gumrukhi letters with their base letter.
        ///     Useful for generalising search queries.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>A simplified version of the provided Gurmukhi string.</returns>
        public static string StripAccents(string text)
        {
            var baseLetterMap = Mapping.AccentMapping();

            // Adds ASCII mapping for each AccentMapping
            foreach (var (key, value) in baseLetterMap.ToArray())
                baseLetterMap.TryAdd(ToAsciiGurmukhi(key), ToAsciiGurmukhi(value));

            // Removes accents from ASCII/Unicode Gumrukhi letters with their base letter.
            return string.Join("",
                text.Select(c =>
                    baseLetterMap.TryGetValue(c.ToString(), out var character) ? character : c.ToString()));
        }

        /// <summary>
        ///    Strips line endings from any Gurmukhi or translation string.
        ///    Accepts both Unicode and ASCII input.
        ///    Useful for generating accurate first letters or modifying non-Gurbani for better display.
        ///    *Not* designed for headings or Sirlekhs.
        /// </summary>
        /// <param name="text">The text to stip endings from.</param>
        /// <returns>A ending-less version of the text.</returns>
        public static string StripEndings(string text)
        {
            // Line endings in both ASCII, Unicode, and English
            var endingClass = StringRegexHelper.GetRegexClass(new[] {"।", "॥", "]", "[", "|"});
            // Sometimes translation line endings begin with these characters, before numbers
            var optionalEndingClass = StringRegexHelper.GetRegexClass(new[] {"("});
            // Remove any broken endings
            var brokenEndingClass = StringRegexHelper.GetRegexGroup(new[] {"()"});

            // All numbers in ASCII, Unicode
            var numbers = Enumerable.Range(0, 10).ToArray()
                .Select(i => i.ToString())
                .ToArray();

            var numberClass = StringRegexHelper.GetRegexClass(
                numbers
                    .Concat(numbers.Select(ToUnicodeGurmukhi))
                    .Concat(numbers.Select(ToUnicodeGurmukhi).Select(ToHindi))
            );

            // Rahao in English, ASCII, Unicode
            var pauseGroup = StringRegexHelper
                .GetRegexGroup(new[] {"ਰਹਾਉ", ToAsciiGurmukhi("ਰਹਾਉ"), "Pause"});

            // Matchers to strip out of input string
            var matchers = new[]
            {
                // Any pause (ending + pause word) => match the rest of the line
                $" ?{endingClass} ?{pauseGroup}.*",
                // Any ending followed by any number => match the rest of the line
                $" ?({endingClass}|{optionalEndingClass}){numberClass}.*",
                // Any sequence at the end of a line with numbers, periods, and spaces beginning with a number
                $" ?{numberClass}({numberClass}|[. ])*$",
                // Clean up any lingering ending characters
                $" ?{brokenEndingClass}",
                $" ?{endingClass}",
            };

            return matchers.Aggregate(text, (a, b) =>
                Regex.Replace(a, b, "").Trim());
        }

        /// <summary>
        ///     Removes the specified vishraams from a string.
        /// </summary>
        /// <param name="text">The text to remove vishraams from.</param>
        /// <param name="vishraams">The vishraams to remove. Defaults to all.</param>
        /// <returns>A vishraam-less Gurmukhi string.</returns>
        public static string StripVishraams(string text,
            Vishraam vishraams = Vishraam.Heavy | Vishraam.Medium | Vishraam.Light)
        {
            var vishraamMapping = Mapping.VishraamMapping();
            
            var vishraamOptions = Enum.GetValues(vishraams.GetType())
                .Cast<Enum>()
                .Where(vishraams.HasFlag)
                .Select(value => vishraamMapping[value.ToString()].ToString());
            
            return Regex.Replace(text, StringRegexHelper.GetRegexClass(vishraamOptions), "");
        }
    }
}