using ShabadOS.GurmukhiUtils.Mappings;

namespace ShabadOS.GurmukhiUtils.Helpers
{
    internal static class VishraamHelper
    {
        public static string VishraamsStr => string.Join("", Mapping.VishraamMapping().Values);
    }
}