using System.Linq;
using System.Text.RegularExpressions;
using ShabadOS.GurmukhiUtils.Helpers;
using ShabadOS.GurmukhiUtils.Mappings;

namespace ShabadOS.GurmukhiUtils
{
    static partial class GurmukhiUtils
    {
        /// <summary>
        /// Transliterates Unicode Gurmukhi text to the Shahmukhi script.
        /// </summary> 
        /// <param name="text">The Unicode Gurmukhi text to convert.</param>
        /// <returns>A Shahmukhi transliteration of the provided Unicode Gurmukhi string.</returns>
        public static string ToShahmukhi(string text)
        {
            // Make each mapping an escaped global regex
            var mappings = Mapping.ShahmukhiMapping()
                .Select(map => new
                {
                    Exp = Regex.Escape(map.Key),
                    Sub = map.Value
                }).ToList();
            
            // Replacement rule to remove trailing ੁ and ਿ for pronunciation
            mappings.Insert(0, new { Exp = $@"(\S[^ਹ])([ਿੁ])([\s{VishraamHelper.VishraamsStr}])", Sub = "$1$3" });

            // Converts Unicode Gurmukhi text to the Shahmukhi script.
            return mappings.Aggregate(text, (a, b) =>
                Regex.Replace( a, b.Exp, b.Sub));
        }
    }
}