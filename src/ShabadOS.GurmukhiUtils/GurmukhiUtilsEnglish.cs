using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ShabadOS.GurmukhiUtils.Helpers;
using ShabadOS.GurmukhiUtils.Mappings;

namespace ShabadOS.GurmukhiUtils
{
    public static partial class GurmukhiUtils
    {
        /// <summary>
        ///     Transliterates a line from Unicode Gurmukhi to english.
        ///     Currently supports the `,`, `;`, `.` vishraam characters.
        /// </summary>
        /// <param name="text">The Gurmukhi Unicode text to transliterate.</param>
        /// <returns>The English transliteration of the provided Gurmukhi line.</returns>
        public static string ToEnglish(string text)
        {
            // Spacer characters
            var spaceChars = new[] {" "}.Concat(Mapping.VishraamMapping().Values);

            var joinedSpaceChars = string.Join("", spaceChars);

            // Escape characters and wrap into regex
            var spaceCharsRegex = $"([{Regex.Replace(joinedSpaceChars, @"[.*+?^${}()|[\]\\]", @"\$&")}])";

            var transliterationMapping = Mapping.TransliterationMapping();

            // Replacements for the initial input
            var replacements = new[] {
                new { Exp = "ey", Sub = "e" },                                                                                // No need for y on top
                new { Exp = "mÚ", Sub = "mhlw" },                                                                             // Mehla replacement
                new { Exp = "i(.)", Sub = "$1i" },                                                                            // Place sihari in correct position
                new { Exp = "(.)[i]([R®H§´ÍÏçœ˜†])", Sub = "$1$2i" },                                                         // Move sihari in front of pairin akhars
                new { Exp = @$"(\S[^ha])[iu]([{VishraamHelper.VishraamsStr}]|\b)", Sub = "$1$2" },                            // Remove trailing Aunkar (u) and Sihari (i) except when on Haha (h), Ooraa (a), or on a standalone akhar
                new { Exp = @$"(\b\S)h([^iIuUyYwWoONM§¨®´µÍÏçüœˆ˜†]\b|[{VishraamHelper.VishraamsStr}])", Sub = "$1yh$2" }     // Add y to three consonant letter words with haha in middle
            };

            // Rules required to add in an extra a letter - all must be true
            var extraRules = new List<Func<string, string, string, bool>>
            {
                // Current letter is alphanumeric
                (letter, _, _) => Regex.IsMatch(letter, "[a-zA-Z]"),
                // Case-insensitive current letter is not a vowel
                (letter, _, _) => !$"aeiou{joinedSpaceChars}ooaiee".Contains(letter.ToLower()),
                // Current letter is not a n-ending type sound
                (letter, _, _) => !new[]
                {
                    transliterationMapping["N"],
                    transliterationMapping["M"],
                    transliterationMapping["W"],
                    transliterationMapping["ƒ"],
                }.Contains(letter),
                // Current letter is not Ik or Oankaar
                (letter, _, _) => !new[]
                {
                    transliterationMapping["<"],
                    transliterationMapping[">"],
                    transliterationMapping["¡"],
                    transliterationMapping["Å"],
                    transliterationMapping["Æ"],
                    transliterationMapping["å"],
                }.Contains(letter),
                // Next letter is not i
                (_, nextLetter, _) => !nextLetter.Contains("i"),
                // Case-insensitive next letter is not in 
                (_, nextLetter, _) => !"@aeouyw".Contains(nextLetter.ToLower()),
                // Next letter is not in long string of extra characters (non-akhars)
                (_, nextLetter, _) => !$@"{joinedSpaceChars}[]IHR®ªÅÆÇÍÏÒØÚåæçüœ:".Contains(nextLetter),
                (_, nextLetter, nextNextLetter) => !nextLetter.Contains("a") && !nextNextLetter.Contains("a")
            };

            // The combined list of final replacements
            var finalReplacements = new[]
            {
                new {Exp = @"\(", Sub = ""},
                new {Exp = @"\)", Sub = ""},
                new {Exp = "aaa", Sub = "aa"},
                new {Exp = "eee", Sub = "ee"},
                new {Exp = "nn", Sub = "n"},
                new {Exp = "eeaa", Sub = "eea"},
                new {Exp = "eiaa", Sub = "eaa"},
                new {Exp = "eio", Sub = "eo"},
                new {Exp = "anm", Sub = "am"},
                new {Exp = $"ahi{spaceCharsRegex}", Sub = "eh$1"},
                new {Exp = $"yhi{spaceCharsRegex}", Sub = "yeh$1"},
                new {Exp = $"{spaceCharsRegex}tit{spaceCharsRegex}", Sub = "$1tith$2"},
                new {Exp = "uu", Sub = "au"},
                new {Exp = "aou", Sub = "au"},
                new {Exp = $"{spaceCharsRegex}au", Sub = "$1u"},
                new {Exp = $"{spaceCharsRegex}ei", Sub = "$1i"},
                new {Exp = $"eau{spaceCharsRegex}", Sub = "eo$1"},
                new {Exp = $"{spaceCharsRegex}n{spaceCharsRegex}", Sub = "$1na$2"},
                new {Exp = $"{spaceCharsRegex}t{spaceCharsRegex}", Sub = "$1ta$2"},
            };
            
            // Work out transliterated text
            var line = replacements
                // Carry out initial replacements
                .Aggregate(ToAsciiGurmukhi(text), (a, b) => Regex.Replace(a, b.Exp, b.Sub))
                .Select(x => x.ToString()) // split as string Enumerable
                .ToArray(); 

            // Transliterate each character
            var transliterated = line.Select((letter, index) =>
            {
                // Look ahead a few letters
                var nextLetter =  index + 1 < line.Length ? line[index + 1] : "";
                var nextNextLetter =  index + 2 < line.Length ? line[index + 2] : "";

                // Map letter using transliteration map
                var mappedLetter = transliterationMapping.TryGetValue(letter, out var l) ? l : letter;
              
                // Add in extra `a` if every rule is met
                if (extraRules.TrueForAll(fn => fn(mappedLetter, nextLetter, nextNextLetter))) mappedLetter += "a";
               
                return mappedLetter;
            });

            var transliteratedStr = string.Join("", transliterated);

            // Apply final replacements
            var final = finalReplacements
                .Aggregate(transliteratedStr, (a, b) => Regex.Replace(a, b.Exp, b.Sub));

            // Remove any triple a, and return
            return Regex.Replace(final,"aaa", "aa");
        }
    }
}