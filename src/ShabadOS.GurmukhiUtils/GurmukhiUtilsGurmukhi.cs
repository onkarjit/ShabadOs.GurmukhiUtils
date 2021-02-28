using System.Linq;
using System.Text.RegularExpressions;
using ShabadOS.GurmukhiUtils.Mappings;

namespace ShabadOS.GurmukhiUtils
{
    public static partial class GurmukhiUtils
    {
        /// <summary>
        ///     Converts ASCII text used in the GurmukhiAkhar font to Unicode.
        /// </summary>
        /// <param name="text">The ASCII text to convert</param>
        /// <returns>A unicode representation of the provided ASCII Gurmukhi string.</returns>
        public static string ToUnicodeGurmukhi(string text)
        {
            var mappings = Mapping.GurmukhiMapping()
                .Select(map => new {
                    Exp = Regex.Escape(map.Key),
                    Sub = map.Value
                })
                .ToArray();
            
            // Replacement rules for converting Gurmukhi ASCII to unicode.
            var replacements = new[] {
                new { Exp = "i(.)", Sub = "$1i" },                        // Switch around sihari position
                new { Exp = "®", Sub = "R" },                             // Use only one type of pair R-sound
                new { Exp = "([iMµyY])([R®H§ÍÏçœ˜†])", Sub = "$2$1" },    // Switch around position of pair R, y etc sounds
                new { Exp = "([MµyY])([uU])", Sub = "$2$1" },             // Switch around lava/dulava/tipee with aunkar/dulankar
                new { Exp = "`([wWIoOyYR®H§´ÍÏçœ˜†uU])", Sub = "$1" },    // Place adhak at end when vowels are either side
                new { Exp = "i([´Î])", Sub = "$1i" }                      // Swap i with ´ or Î
            };
            
            // Precompute the replacements and mappings
            var finalReplacements = replacements.Concat(mappings);

            // Converts ASCII text used in the GurmukhiAkhar font to Unicode.
            return finalReplacements.Aggregate(text, (a, b) =>
                Regex.Replace( a, b.Exp, b.Sub));
        }
        
        /// <summary>
        ///     Converts Gurmukhi unicode text to ASCII, used GurmukhiAkhar font.
        /// </summary>
        /// <param name="text">The unicode text to convert.</param>
        /// <returns> An ASCII representation of the provided unicode Gurmukhi string.</returns>
        public static string ToAsciiGurmukhi(string text)
        {
            // Make each mapping an escaped global regex, and flip around the mappings
            var mappings = Mapping.GurmukhiMapping()
                .Where(map => !string.IsNullOrEmpty(map.Value)) // Do not include empty sub -> expr mappings
                .Select(map => new
                {
                    Exp = Regex.Escape(map.Value),
                    Sub = map.Key
                });

            // Replacement for Akhar + Nutka with single character
            var nuktaMappings = new[] {
                new { Exp = "sæ", Sub = "S" },
                new { Exp = "Kæ", Sub = "^" },
                new { Exp = "gæ", Sub = "Z" },
                new { Exp = "jæ", Sub = "z" },
                new { Exp = "Pæ", Sub = "&" },
                new { Exp = "læ", Sub = "L" }     
            };
            
            // Replacement rules for correcting converted unicode.
            var postReplacements = new[] {
                new { Exp = "(.)i", Sub = "i$1" },                       // Switch position of sihari
                new { Exp = "wN", Sub = "W" },                           // Replace bindi, khana with single khana with bindi char
                new { Exp = "(.)i([R®H§´ÍÏçœ˜†Î])", Sub = "i$1$2" },     // Switch sihari position when pair akhars exist
                new { Exp = "kR", Sub = "k®" },                          // Replace K pair rara with correct rara
                new { Exp = "([nl])M", Sub = "$1µ" },                    // Replace tippi with correct tippi char
                new { Exp = "i([nl])µ", Sub = "i$1M" },                  // Replace tippi with correct tippi char in sihari case
                new { Exp = "([NMˆµ])I", Sub = "$1ØI" },                 // Add spacer char between bindi/tippi and bihari
                new { Exp = "NØI", Sub = "ˆØI" },                        // Use bindi on top of spacer char
                new { Exp = "MØI", Sub = "µØI" },                        // Use centered tippi on top of spacer char
                new { Exp = "([@R®H´ÍÏçœ˜†])u", Sub = "$1ü" },           // Use lower aunkar when char is pair akhar
                new { Exp = "([@R®H´ÍÏçœ˜†])U", Sub = "$1¨" }            // Use lower dulankaar when char is pair akhar
            };
            
            // Precompute the replacements and mappings
            var finalReplacements = mappings.Concat(nuktaMappings).Concat(postReplacements).ToList();
            
            // Move nukta from sihari
            finalReplacements.Insert(0, new { Exp = "(.)ਿ਼", Sub = "$1਼ਿ" });

            // Converts Gurmukhi unicode text to ASCII, used GurmukhiAkhar font.
            return finalReplacements.Aggregate(text, (a, b) =>
                Regex.Replace( a, b.Exp, b.Sub));
        }

        // Checks if Unicode Text is in Gurmukhi Block (U+0A00 - U+0A7F)
        private static bool CheckCharCode(char character) => character >= 2560 && character <= 2687;
            
        /// <summary>
        ///     Checks if first char in string is part of the Gurmukhi Unicode block.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <param name="exhaustive">If `true`, checks if the whole string is Unicode Gurmukhi.</param>
        /// <returns>True if Unicode Gurmukhi, false if other.</returns>
        public static bool IsGurmukhi(string text, bool exhaustive = false)
        {
            var filteredChars = new[] {
                " ",
                "\u200B",
                "॥",
            }.Concat(Mapping.VishraamMapping().Values);

            return exhaustive
                ? text.ToCharArray()
                    .Where(i => !filteredChars.Contains(i.ToString()))
                    .All(CheckCharCode)
                : CheckCharCode(text[0]);
        }
    }
}