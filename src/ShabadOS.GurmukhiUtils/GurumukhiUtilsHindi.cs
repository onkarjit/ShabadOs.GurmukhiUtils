using System.Linq;
using System.Text.RegularExpressions;
using ShabadOS.GurmukhiUtils.Mappings;

namespace ShabadOS.GurmukhiUtils
{
    public static partial class GurmukhiUtils
    {
        /// <summary>
        /// Transliterates Unicode Gurmukhi text to Hindi (Devanagari script).
        /// </summary>
        /// <param name="text">The Unicode Gurmukhi text to convert.</param>
        /// <returns>A Hindi transliteration of the provided Unicode Gurmukhi string.</returns>
        public static string ToHindi(string text)
        {
            // Make each mapping an escaped global regex
            var mappings = Mapping.DevanagariMapping()
                .Select(map => new {
                    Exp = Regex.Escape(map.Key),
                    Sub = map.Value
                })
                .ToList();
            
            // Replacement rules for initial input
            var replacement = new {Exp = "(ੰ|ਂ)(ੀ)", Sub = "$2$1"};

            // Add replacement to replacements
            mappings.Insert(0, replacement);
            
            // Transliterates Unicode Gurmukhi text to Hindi (Devanagari script).
            return mappings.Aggregate(text, (a, b) =>
                Regex.Replace( a, b.Exp, b.Sub));
        }
    }
}