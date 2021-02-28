using System.Linq;
using Xunit;

namespace ShabadOS.GurmukhiUtils.Test
{
    public class FirstLettersTest
    {
        [Fact]
        public void FirstLettersGurmukhi()
        {
            var words = new[]
            {
                new {Gurmukhi = "ਗੁਰਮੁਖਿ  ਲਾਧਾ ਮਨਮੁਖਿ   ਗਵਾਇਆ ॥", FirstLetters = "ਗਲਮਗ॥"},
                new {Gurmukhi = "ਗੁਰਮੁਖਿ ਲਾਧਾ ਮਨਮੁਖਿ ਗਵਾਇਆ ॥", FirstLetters = "ਗਲਮਗ॥"},
                new {Gurmukhi = "ਜਿਨਿ ਹਰਿ ਸੇਵਿਆ ਤਿਨਿ ਸੁਖੁ ਪਾਇਆ ॥", FirstLetters = "ਜਹਸਤਸਪ॥"},
                new {Gurmukhi = "ਗ਼ੈਰਿ ਹਮਦਿ ਹੱਕ ਨਿਆਇਦ ਬਰ ਜ਼ਬਾਨਮ ਹੀਚ ਗਾਹ", FirstLetters = "ਗ਼ਹਹਨਬਜ਼ਹਗ"},
                new {Gurmukhi = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ; ਫਿਰਿ. ਮਰੈ ਨ, ਦੂਜੀ ਵਾਰ ॥", FirstLetters = "ਸਮ.ਸਮਰ;ਫ.ਮਨ,ਦਵ॥"},
                new {Gurmukhi = "ਇਕਨਾ. ਹੁਕਮੀ ਬਖਸੀਸ; ਇਕਿ, ਹੁਕਮੀ ਸਦਾ ਭਵਾਈਅਹਿ ॥", FirstLetters = "ੲ.ਹਬ;ੲ,ਹਸਭ॥"},
                new
                {
                    Gurmukhi = GurmukhiUtils.ToUnicodeGurmukhi("ik hr hSqo Ssq Awmdw cwkrS [148["),
                    FirstLetters = "ਕਹਹਸ਼ਅਚ।"
                }
            };

            Assert.All(words, w => Assert.Equal(GurmukhiUtils.FirstLetters(w.Gurmukhi), w.FirstLetters));
        }

        [Fact]
        public void FirstLettersHindi()
        {
            var words = new[]
            {
                new {Gurmukhi = "ਗੁਰਮੁਖਿ ਲਾਧਾ ਮਨਮੁਖਿ ਗਵਾਇਆ ॥", FirstLetters = "गलमग॥"},
                new {Gurmukhi = "ਜਿਨਿ ਹਰਿ ਸੇਵਿਆ ਤਿਨਿ ਸੁਖੁ ਪਾਇਆ ॥", FirstLetters = "जहसतसप॥"},
                new {Gurmukhi = "ਗ਼ੈਰਿ ਹਮਦਿ ਹੱਕ ਨਿਆਇਦ ਬਰ ਜ਼ਬਾਨਮ ਹੀਚ ਗਾਹ", FirstLetters = "ग़हहनबज़हग"},
                new {Gurmukhi = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ; ਫਿਰਿ. ਮਰੈ ਨ, ਦੂਜੀ ਵਾਰ ॥", FirstLetters = "सम.समर;फ.मन,दव॥"}
            }.Select(o => new {Hindi = GurmukhiUtils.ToHindi(o.Gurmukhi), o.FirstLetters});

            Assert.All(words, w => Assert.Equal(GurmukhiUtils.FirstLetters(w.Hindi), w.FirstLetters));
        }
        
        [Fact]
        public void FirstLettersEnglish()
        {
            var words = new[]
            {
                new { Gurmukhi = "ਗੁਰਮੁਖਿ ਲਾਧਾ ਮਨਮੁਖਿ ਗਵਾਇਆ ॥", FirstLetters = "glmg|" },
                new { Gurmukhi = "ਜਿਨਿ ਹਰਿ ਸੇਵਿਆ ਤਿਨਿ ਸੁਖੁ ਪਾਇਆ ॥", FirstLetters = "jhstsp|" },
                new { Gurmukhi = "ਗ਼ੈਰਿ ਹਮਦਿ ਹੱਕ ਨਿਆਇਦ ਬਰ ਜ਼ਬਾਨਮ ਹੀਚ ਗਾਹ", FirstLetters = "ghhnbzhg" },
                new { Gurmukhi = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ; ਫਿਰਿ. ਮਰੈ ਨ, ਦੂਜੀ ਵਾਰ ॥", FirstLetters = "sm.smr;f.mn,dv|" }
            }.Select(o => new {English = GurmukhiUtils.ToEnglish(o.Gurmukhi), o.FirstLetters});

            Assert.All(words, w => Assert.Equal(GurmukhiUtils.FirstLetters(w.English), w.FirstLetters));
        }
    }
}