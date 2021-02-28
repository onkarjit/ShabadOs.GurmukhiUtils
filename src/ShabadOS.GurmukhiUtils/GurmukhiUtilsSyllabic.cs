using System.Linq;
using System.Text.RegularExpressions;
using ShabadOS.GurmukhiUtils.Helpers;

namespace ShabadOS.GurmukhiUtils
{
    public static partial class GurmukhiUtils
    {
        /// <summary>
        /// Represents text in syllables according to Sanskrit prosody, Pingala, Matra/Meter/Morae
        /// </summary>
        /// <param name="text">The string to convert</param>
        /// <returns>A syllabic representation of 1"s (laghu/light/short) and 2"s (guru/heavy/long).</returns>
        public static string ToSyllabicSymbols(string text)
        {
            // These have no impact on weight, therfore remove before processing
            var zeroWeightSigns = new[]
            {
                "੍ਰ",
                "ੵ",
                "ੑ",
                "੍ਵ",
                "੍ਹ",
                "੍ਟ",
                "੍ਨ",
                "੍ਯ",
                "੍ਚ",
                "੍ਤ",
                "੍",
                "ਿ",
                "ੁ",
                "ਂ",
                "ਃ",
                "।",
                "॥",
                "☬",
                "਼",
                "❁",
            };

            // For the rest, need to analyze the string
            // Base characters counts as one syllable (light)
            // Base characters with any number of long (deeragh) sounds counts as two syllables (heavy)
            var syllableSymbols = new
            {
                Light = "1",
                Deeragh = "s",
                HeavySequence = "1s",
                Heavy = "2",
            };

            // The list of base characters for analysis
            var baseCharacters = new[]
            {
                "ਇ",
                "ਉ",
                "ਙ",
                "ੳ",
                "ਅ",
                "ਬ",
                "ਭ",
                "ਚ",
                "ਛ",
                "ਦ",
                "ਧ",
                "ੲ",
                "ਡ",
                "ਢ",
                "ਗ",
                "ਘ",
                "ਹ",
                "ਜ",
                "ਝ",
                "ਕ",
                "ਖ",
                "ਲ",
                "ਲ਼",
                "ਮ",
                "ਨ",
                "ਪ",
                "ਫ",
                "ਤ",
                "ਥ",
                "ਰ",
                "ਸ",
                "ਸ਼",
                "ਟ",
                "ਠ",
                "ਵ",
                "ੜ",
                "ਣ",
                "ਯ",
                "ਜ਼",
                "ਗ਼",
                "ਖ਼",
                "ਫ਼",
                "ਞ",
            };

            // The list of long sound characters for analysis
            var deeraghModifiers = new[]
            {
                "ੰ",
                "੍ਹੂ",
                "ੀ",
                "ੂ",
                "ੇ",
                "ੈ",
                "ੋ",
                "ੌ",
                "ਾਂ",
                "ਾ",
                "ੱ",
            };

            // Some symbols represent multiple characters in a single unicode entity.
            // These should be considered unsplittable and unmodifiable by other vowels/signs.
            // The following represent a base character and deeragh in one unicode entity point.
            // Since author is unaware of whether these can be further modified with deeragh
            // they"ll be processed as a heavy sequence (base char + deeragh) in the mapping.
            // If that is not the case and these characters cannot have further deeragh modifiers,
            // then they can be safely mapped directly to syllableSymbol.heavy
            var twoUnitSyllables = new[] {"ਊ", "ਓ", "ਈ", "ਏ", "ਐ", "ਆ", "ਔ"};

            // Create a map for each character to a syllableSymbol, for further processing/analysis
            var syllabicMapping = new[]
                {
                    new {Symbol = syllableSymbols.Light, GroupedCharacters = baseCharacters},
                    new {Symbol = syllableSymbols.Deeragh, GroupedCharacters = deeraghModifiers},
                    new {Symbol = syllableSymbols.HeavySequence, GroupedCharacters = twoUnitSyllables}
                }
                .SelectMany(sm =>
                    sm.GroupedCharacters.Select(c => new {Character = c, sm.Symbol}))
                // Add any missing mappings which do not fit in the above patterns
                // These rules are "as is", and are not modified in the reducer above
                .Append(new {Character = "ੴ", Symbol = "21 2221"}) // Ik Oankaar / ਇੱਕ ਓਅੰਕਾਰ
                .Append(new {Character = " ", Symbol = " "}) // Preserve spacing between words
                .ToDictionary(k => k.Character, v => v.Symbol);

            // Create Regex rules for replacements
            var zeroWeightSignsRegex = new Regex(StringRegexHelper.GetRegexGroup(zeroWeightSigns));
            var multipleSpaceCharsRegex = new Regex(" +");
            var multipleDeeraghSymbolsRegex = new Regex("s+");
            var heavySequenceRegex = new Regex("1s");

            text = ToUnicodeGurmukhi(text);

            var result = string
                .Join("", zeroWeightSignsRegex
                    .Replace(text, "")
                    .ToCharArray()
                    .Select(value =>
                        syllabicMapping.TryGetValue(value.ToString(), out var val) ? val : " "));

            result = multipleSpaceCharsRegex.Replace(result, " ");
            result = multipleDeeraghSymbolsRegex.Replace(result, syllableSymbols.Deeragh);
            result = heavySequenceRegex.Replace(result, syllableSymbols.Heavy);

            return result;
        }

        /// <summary>
        ///     Calculates the number of syllables according to Sanskrit prosody, Pingala, Matra/Meter/Morae
        /// </summary>
        /// <param name="text">The string to analyze</param>
        /// <returns>An integer adding up all the 1's (laghu/light/short) and 2's (guru/heavy/long).</returns>
        public static int CountSyllables(string text) =>
            ToSyllabicSymbols(text)
                .ToCharArray()
                .Aggregate(0, (a, b) => (int.TryParse(b.ToString(), out var r) ? r : 0) + a);
    }
}