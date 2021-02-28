using System.Collections.Generic;
using System.Linq;
using ShabadOS.GurmukhiUtils.Mappings;

namespace ShabadOS.GurmukhiUtils
{
    static partial class GurmukhiUtils
    {
        /// <summary>
        ///     Generates the first letters for a unicode Gurmukhi,
        ///     Hindi transliteration, or English transliteration string.
        ///     Includes any end-word vishraams, and line-end characters.
        /// </summary>
        /// <param name="line">The line to generate the first letters for.</param>
        /// <returns>The first letters of each word in the provided Gurmukhi line.</returns>
        public static string FirstLetters(string line)
        {
            var vowelFirstLetters = new Dictionary<string, string>()
            {
                {"ਇ", "ੲ"},
                {"ਈ", "ੲ"},
                {"ਏ", "ੲ"},
                {"ਉ", "ੳ"},
                {"ਊ", "ੳ"},
                {"ਆ", "ਅ"},
                {"ਐ", "ਅ"},
                {"ਔ", "ਅ"}
            };

            var lineEnumerable = line
                .Split(" ")
                // Remove any words that are excess spaces
                .Where(word => word.Trim().Length > 0)
                .Select(word =>
                {
                    var letter = word[0].ToString();
                    var lastLetter = word[^1].ToString();

                    // Capture the vishraam char, if it exists
                    var vishraamChar = Mapping.VishraamMapping().Values.Contains(lastLetter) ? lastLetter : "";

                    // Retrieve the unicode vowel-letter"s actual letter, with passthrough
                    var firstLetter = vowelFirstLetters.TryGetValue(letter, out var l) ? l : letter;

                    // Return base letter along with potential vishraam character
                    return $"{firstLetter}{vishraamChar}";
                });

            return string.Join("", lineEnumerable);
        }
    }
}