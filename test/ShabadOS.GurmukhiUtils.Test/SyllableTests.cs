using Xunit;

namespace ShabadOS.GurmukhiUtils.Test
{
    public class SyllableTests
    {
        [Fact]
        public void ToSyllabicSymbols()
        {
            var words = new[]
            {
                new {Gurmukhi = "ਕਸਰਤ", Syllabic = "1111"},
                new {Gurmukhi = "ਬਰਕਤ", Syllabic = "1111"},
                new {Gurmukhi = "ਮਿਹਨਤ", Syllabic = "1111"},
                new {Gurmukhi = "ਮੁਜਰਮ", Syllabic = "1111"},
                new {Gurmukhi = "ਗੁਰਮੁਖ", Syllabic = "1111"},
                new {Gurmukhi = "ਕਾਕੀਏ", Syllabic = "222"},
                new {Gurmukhi = "ਕਾਕੀ\u0A0F", Syllabic = "222"},
                new {Gurmukhi = "ਕਾਕੀ\u0A72\u0A47", Syllabic = "222"},
                new {Gurmukhi = "ਤੌਲੀਆ", Syllabic = "222"},
                new {Gurmukhi = "ਤੋਰੀਆ", Syllabic = "222"},
                new {Gurmukhi = "ਚੀਰੀਏ", Syllabic = "222"},
                new {Gurmukhi = "ਤੰਬੂਰਾ", Syllabic = "222"},
                new {Gurmukhi = "ਲੰਮਾਈ", Syllabic = "222"},
                new {Gurmukhi = "ਬੱਚੀਏ", Syllabic = "222"},
                new {Gurmukhi = "ਸਾਗ", Syllabic = "21"},
                new {Gurmukhi = "ਦੌੜ", Syllabic = "21"},
                new {Gurmukhi = "ਪੀੜ", Syllabic = "21"},
                new {Gurmukhi = "ਹਿੱਲ", Syllabic = "21"},
                new {Gurmukhi = "ਵਿਚੋਂ", Syllabic = "12"},
                new {Gurmukhi = "ਤਲਾ", Syllabic = "12"},
                new {Gurmukhi = "ਸਿਰੀ", Syllabic = "12"},
                new {Gurmukhi = "ਗੁਆ", Syllabic = "12"},
                new {Gurmukhi = "ਸਰਲ", Syllabic = "111"},
                new {Gurmukhi = "ਹਿਕਮਤ", Syllabic = "1111"},
                new {Gurmukhi = "ਉਜਰਤ", Syllabic = "1111"},
                new {Gurmukhi = "ਸਾਰ", Syllabic = "21"},
                new {Gurmukhi = "ਸਾਰਾ ਸਾਰੇ", Syllabic = "22 22"},
                new {Gurmukhi = "ਕੂਲਾ ਕੂਲੀ", Syllabic = "22 22"},
                new {Gurmukhi = "ਗੁਲਾਈ", Syllabic = "122"},
                new {Gurmukhi = "ਚੁੜਾਈ", Syllabic = "122"},
                new {Gurmukhi = "ਜੋੜੀਆਂ", Syllabic = "222"},
                new {Gurmukhi = "ਵੱਟਿਆਂ", Syllabic = "212"},
                new {Gurmukhi = "v`itAW", Syllabic = "212"},
                new {Gurmukhi = "v~itAW", Syllabic = "212"},
                new {Gurmukhi = "ਵੱਟੀਆਂ", Syllabic = "222"},
                new {Gurmukhi = "ਅੰਮ੍ਰਿਤ", Syllabic = "211"},
                new {Gurmukhi = "ਅੰਮ੍ਰਿਤਸਰ", Syllabic = "21111"},
                new {Gurmukhi = "ਦੇਵਿੰਦਰ", Syllabic = "2211"},
                new {Gurmukhi = "ਕਿਰਪਾਲਤਾ", Syllabic = "11212"},
                new {Gurmukhi = "ਪ੍ਰਭੂ ਪ੍ਰੇਮੀ ਪੜ੍ਹ ਚੜ੍ਹ ਦ੍ਵੈਤ", Syllabic = "12 22 11 11 21"},
                new {Gurmukhi = "pRBU pRymI pVH cVH dÍYq", Syllabic = "12 22 11 11 21"},
                new {Gurmukhi = "AYsw", Syllabic = "22"},
                new {Gurmukhi = "ਹਫ਼ਤੇ ਵਿਚ 7 ਦਿਨ", Syllabic = "112 11 11"},
                new {Gurmukhi = "ieMdR ieMdRwx", Syllabic = "21 221"}
            };
            
            Assert.All(words, w => Assert.Equal(GurmukhiUtils.ToSyllabicSymbols(w.Gurmukhi), w.Syllabic));
        }
        
        [Fact]
        public void CountSyllables()
        {
            var tests = new[]
            {
                new { Gurmukhi = "ਅੰਮ੍ਰਿਤਸਰ", SyllableCount = 6 },
                new { Gurmukhi = "ਸਾਰਾ ਸਾਰੇ", SyllableCount = 8 },
                new { Gurmukhi = "ਪ੍ਰਭੂ ਪ੍ਰੇਮੀ ਪੜ੍ਹ ਚੜ੍ਹ ਦ੍ਵੈਤ", SyllableCount = 14 }
            };
            
            Assert.All(tests, t => Assert.Equal(GurmukhiUtils.CountSyllables(t.Gurmukhi), t.SyllableCount));
        }
    }
}