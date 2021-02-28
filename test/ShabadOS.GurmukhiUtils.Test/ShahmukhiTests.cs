using Xunit;

namespace ShabadOS.GurmukhiUtils.Test
{
    public class ShahmukhiTests
    {
        [Fact]
        public void ToShahmukhi()
        {
            var words = new[]
            {
                new { Gurmukhi = "ਖੋਜ", Shahmukhi = "کھوج" },
                new { Gurmukhi = "ਤੂ ਪ੍ਰਭ ਦਾਤਾ ਦਾਨਿ ਮਤਿ ਪੂਰਾ ਹਮ ਥਾਰੇ ਭੇਖਾਰੀ ਜੀਉ ॥", Shahmukhi = "تُو پربھ داتا دان مت پُورا هم تھارے بھےکھاری جیاُ ۔۔" },
                new { Gurmukhi = "ਸੋ ਬ੍ਰਹਮੁ ਅਜੋਨੀ ਹੈ ਭੀ ਹੋਨੀ ਘਟ ਭੀਤਰਿ ਦੇਖੁ ਮੁਰਾਰੀ ਜੀਉ ॥੨॥", Shahmukhi = "سو برهم اجونی هَے بھی هونی گھٹ بھیتر دےکھ مُراری جیاُ ۔۔۲۔۔" },
                new { Gurmukhi = "ਜ਼ਨੇ ਪੇਚ ਦਸਤਾਰ ਰਾ ਤਾਬਦਾਦ ॥", Shahmukhi = "زنے پےچ دستار را تابداد ۔۔" },
                new { Gurmukhi = "ਸਉਡਿਸਇਸ ਇਸ ਇਸਣੀ ਆਦਿ ਬਖਾਨਿ ਕੈ ॥", Shahmukhi = "ساُڈِسِاس ِاس ِاسنی آد بکھان کَے ۔۔" },
                new { Gurmukhi = "ਅਜ਼ੋ ਗਸ਼ਤਾ ਹਰ ਜ਼ੱਰਰਾ ਖ਼ੁਰਸ਼ੈਦ ਤਾਬ ॥੯੬॥", Shahmukhi = "ازو گشتا هر زّررا خُرشَےد تاب ۔۔۹۶۔۔" },
                new { Gurmukhi = "ਹਮਾ ਸਾਇਲਿ ਲੁਤਫ਼ਿ ਹਕ ਪਰਵਰਸ਼ ॥", Shahmukhi = "هما ساِال لُتف هک پرورش ۔۔" },
                new { Gurmukhi = "ਸੁ ਬੈਠਿ ਇਕੰਤ੍ਰ ॥੫੭੮॥", Shahmukhi = "سُ بَےٹھ ِاکںتر ۔۔۵۷۸۔۔" },
                new { Gurmukhi = "ਇਤਿ ਸ੍ਰੀ ਬਚਿਤ੍ਰ ਨਾਟਕੇ ਮਨੁ ਰਾਜਾ ਕੋ ਰਾਜ ਸਮਾਪਤੰ ॥੧॥੫॥", Shahmukhi = "ِات سری بچِتر ناٹکے من راجا کو راج سماپتں ۔۔۱۔۔۵۔۔" },
                new { Gurmukhi = "ਤਿਸੁ ਵਿਣੁ ਸਭੁ ਅਪਵਿਤ੍ਰੁ ਹੈ ਜੇਤਾ ਪੈਨਣੁ ਖਾਣੁ ॥", Shahmukhi = "تِس وِن سبھ اپوِتر هَے جےتا پَےنن کھان ۔۔" },
                new { Gurmukhi = "ਸੋਢੀ ਸ੍ਰਿਸ੍ਟਿ ਸਕਲ ਤਾਰਣ ਕਉ ਅਬ ਗੁਰ ਰਾਮਦਾਸ ਕਉ ਮਿਲੀ ਬਡਾਈ ॥੩॥", Shahmukhi = "سوڈھی سرِسٹ سکل تارن کاُ اب گُر رامداس کاُ مِلی بڈاای ۔۔۳۔۔" },
                new { Gurmukhi = "ਰਾਗੁ ਗਉੜੀ ਥਿਤੰੀ ਕਬੀਰ ਜੀ ਕੰੀ ॥", Shahmukhi = "راگ گاُڑی تھِتںی کبیر جی کںی ۔۔" },
                new { Gurmukhi = "ਸੁ ਸਿਰਿ ਹਰਿ ਹਿਰ ਕਰਹਿ ਹਿਰੁ ਰਹੁ ਸੁਹੰਗ ਕੂਕਰੁ ਸਰਸੁ ਜੁ ਜੁਜੁ ਏਹਿ ਭਿ ਦਾਤਿ ॥", Shahmukhi = "سُ سِر هر هِر کرهِ هِر رهُ سُهںگ کُوکر سرس جُ جُج اےهِ بھِ دات ۔۔" },
                new { Gurmukhi = "ਹੁਕਮੈ ਅੰਦਰਿ. ਸਭੁ ਕੋ; ਬਾਹਰਿ ਹੁਕਮ. ਨ ਕੋਇ ॥", Shahmukhi = "هُکمَے اںدر. سبھ کو; باهر هُکم. ن کوِا ۔۔" },
            };
            
            Assert.All(words, w => Assert.Equal(GurmukhiUtils.ToShahmukhi(w.Gurmukhi), w.Shahmukhi));
        }
    }
}