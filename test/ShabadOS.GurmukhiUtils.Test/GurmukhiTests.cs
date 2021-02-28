using Xunit;

namespace ShabadOS.GurmukhiUtils.Test
{
    public class GurmukhiTests
    {
        [Fact]
        public void ToUnicodeGurmukhi()
        {
            var words = new[]
            {
                new { Ascii = "Koj", Unicode = "ਖੋਜ" },
                new { Ascii = "ihr", Unicode = "ਹਿਰ" },
                new { Ascii = "imil´o", Unicode = "ਮਿਲੵਿੋ" },
                new { Ascii = "iBÎo", Unicode = "ਭ੍ਯਿੋ" },
                new { Ascii = "kul jn mDy imil´o swrg pwn ry ]", Unicode = "ਕੁਲ ਜਨ ਮਧੇ ਮਿਲੵਿੋ ਸਾਰਗ ਪਾਨ ਰੇ ॥" },
                new { Ascii = "qU pRB dwqw dwin miq pUrw hm Qwry ByKwrI jIau ]", Unicode = "ਤੂ ਪ੍ਰਭ ਦਾਤਾ ਦਾਨਿ ਮਤਿ ਪੂਰਾ ਹਮ ਥਾਰੇ ਭੇਖਾਰੀ ਜੀਉ ॥" },
                new { Ascii = "so bRhmu AjonI hY BI honI Gt BIqir dyKu murwrI jIau ]2]", Unicode = "ਸੋ ਬ੍ਰਹਮੁ ਅਜੋਨੀ ਹੈ ਭੀ ਹੋਨੀ ਘਟ ਭੀਤਰਿ ਦੇਖੁ ਮੁਰਾਰੀ ਜੀਉ ॥੨॥" },
                new { Ascii = "zny pyc dsqwr rw qwbdwd ]", Unicode = "ਜ਼ਨੇ ਪੇਚ ਦਸਤਾਰ ਰਾ ਤਾਬਦਾਦ ॥" },
                new { Ascii = "sauifsies ies iesxI Awid bKwin kY ]", Unicode = "ਸਉਡਿਸਇਸ ਇਸ ਇਸਣੀ ਆਦਿ ਬਖਾਨਿ ਕੈ ॥" },
                new { Ascii = "Azo gSqw hr z`rrw ^urSYd qwb ]96]", Unicode = "ਅਜ਼ੋ ਗਸ਼ਤਾ ਹਰ ਜ਼ੱਰਰਾ ਖ਼ੁਰਸ਼ੈਦ ਤਾਬ ॥੯੬॥" },
                new { Ascii = "hmw swieil luqi& hk prvrS ]", Unicode = "ਹਮਾ ਸਾਇਲਿ ਲੁਤਫ਼ਿ ਹਕ ਪਰਵਰਸ਼ ॥" },
                new { Ascii = "su bYiT iekMqR ]578]", Unicode = "ਸੁ ਬੈਠਿ ਇਕੰਤ੍ਰ ॥੫੭੮॥" },
                new { Ascii = "ieiq sRI bicqR nwtky mnu rwjw ko rwj smwpqM ]1]5]", Unicode = "ਇਤਿ ਸ੍ਰੀ ਬਚਿਤ੍ਰ ਨਾਟਕੇ ਮਨੁ ਰਾਜਾ ਕੋ ਰਾਜ ਸਮਾਪਤੰ ॥੧॥੫॥" },
                new { Ascii = "Fwknhwry pRBU hmwry jIA pRwn suKdwqy ]", Unicode = "ਢਾਕਨਹਾਰੇ ਪ੍ਰਭੂ ਹਮਾਰੇ ਜੀਅ ਪ੍ਰਾਨ ਸੁਖਦਾਤੇ ॥" },
                new { Ascii = "BuiKAw.", Unicode = "ਭੁਖਿਆ." },
                new { Ascii = "<> siq nwmu krqw purKu inrBau inrvYru; Akwl mUriq AjUnI sYBM gurpRswid ]", Unicode = "ੴ ਸਤਿ ਨਾਮੁ ਕਰਤਾ ਪੁਰਖੁ ਨਿਰਭਉ ਨਿਰਵੈਰੁ; ਅਕਾਲ ਮੂਰਤਿ ਅਜੂਨੀ ਸੈਭੰ ਗੁਰਪ੍ਰਸਾਦਿ ॥" },
                new { Ascii = "rwgu gauVI iQqMØI kbIr jI kMØI ]", Unicode = "ਰਾਗੁ ਗਉੜੀ ਥਿਤੰੀ ਕਬੀਰ ਜੀ ਕੰੀ ॥" },
                new { Ascii = "Awqmw bwsudyvis´ jy ko jwxY Byau ]", Unicode = "ਆਤਮਾ ਬਾਸੁਦੇਵਸੵਿ ਜੇ ਕੋ ਜਾਣੈ ਭੇਉ ॥" },
                new { Ascii = "Asmwn im´wny lhMg drIAw gusl krdn bUd ]", Unicode = "ਅਸਮਾਨ ਮੵਿਾਨੇ ਲਹੰਗ ਦਰੀਆ ਗੁਸਲ ਕਰਦਨ ਬੂਦ ॥" },
                new { Ascii = "durlBM eyk Bgvwn nwmh nwnk lbiD´M swDsMig ik®pw pRBM ]35]", Unicode = "ਦੁਰਲਭੰ ਏਕ ਭਗਵਾਨ ਨਾਮਹ ਨਾਨਕ ਲਬਧੵਿੰ ਸਾਧਸੰਗਿ ਕ੍ਰਿਪਾ ਪ੍ਰਭੰ ॥੩੫॥" },
                new { Ascii = "jyn klw sis sUr nK´qR joiq´M swsM srIr DwrxM ]", Unicode = "ਜੇਨ ਕਲਾ ਸਸਿ ਸੂਰ ਨਖੵਤ੍ਰ ਜੋਤੵਿੰ ਸਾਸੰ ਸਰੀਰ ਧਾਰਣੰ ॥" },
                new { Ascii = "bis´Mq iriKAM iqAwig mwnµ ]", Unicode = "ਬਸੵਿੰਤ ਰਿਖਿਅੰ ਤਿਆਗਿ ਮਾਨੰ ॥" },
                new { Ascii = "pRwq Bey inRp bIc sBw, kib sïwm khY ieh BWiq aucwrîo ]", Unicode = "ਪ੍ਰਾਤ ਭਏ ਨ੍ਰਿਪ ਬੀਚ ਸਭਾ, ਕਬਿ ਸਯਾਮ ਕਹੈ ਇਹ ਭਾਂਤਿ ਉਚਾਰ੍ਯੋ ॥" }
            };

            Assert.All(words, w => Assert.Equal(GurmukhiUtils.ToUnicodeGurmukhi(w.Ascii), w.Unicode));
        }
        
        [Fact]
        public void ToAsciiGurmukhi()
        {
            var words = new[]
            {
                new { Unicode = "ਖੋਜ", Ascii = "Koj" },
                new { Unicode = "ਹਿਰ", Ascii = "ihr" },
                new { Unicode = "ਮਿਲੵਿੋ", Ascii = "imil´o" },
                new { Unicode = "ਭ੍ਯਿੋ", Ascii = "iBÎo" },
                new { Unicode = "ਕੁਲ ਜਨ ਮਧੇ ਮਿਲੵਿੋ ਸਾਰਗ ਪਾਨ ਰੇ ॥", Ascii = "kul jn mDy imil´o swrg pwn ry ]" },
                new { Unicode = "ਤੂ ਪ੍ਰਭ ਦਾਤਾ ਦਾਨਿ ਮਤਿ ਪੂਰਾ ਹਮ ਥਾਰੇ ਭੇਖਾਰੀ ਜੀਉ ॥", Ascii = "qU pRB dwqw dwin miq pUrw hm Qwry ByKwrI jIau ]" },
                new { Unicode = "ਸੋ ਬ੍ਰਹਮੁ ਅਜੋਨੀ ਹੈ ਭੀ ਹੋਨੀ ਘਟ ਭੀਤਰਿ ਦੇਖੁ ਮੁਰਾਰੀ ਜੀਉ ॥੨॥", Ascii = "so bRhmu AjonI hY BI honI Gt BIqir dyKu murwrI jIau ]2]" },
                new { Unicode = "ਜ਼ਨੇ ਪੇਚ ਦਸਤਾਰ ਰਾ ਤਾਬਦਾਦ ॥", Ascii = "zny pyc dsqwr rw qwbdwd ]" },
                new { Unicode = "ਸਉਡਿਸਇਸ ਇਸ ਇਸਣੀ ਆਦਿ ਬਖਾਨਿ ਕੈ ॥", Ascii = "sauifsies ies iesxI Awid bKwin kY ]" },
                new { Unicode = "ਅਜ਼ੋ ਗਸ਼ਤਾ ਹਰ ਜ਼ੱਰਰਾ ਖ਼ੁਰਸ਼ੈਦ ਤਾਬ ॥੯੬॥", Ascii = "Azo gSqw hr z`rrw ^urSYd qwb ]96]" },
                new { Unicode = "ਹਮਾ ਸਾਇਲਿ ਲੁਤਫ਼ਿ ਹਕ ਪਰਵਰਸ਼ ॥", Ascii = "hmw swieil luqi& hk prvrS ]" },
                new { Unicode = "ਸੁ ਬੈਠਿ ਇਕੰਤ੍ਰ ॥੫੭੮॥", Ascii = "su bYiT iekMqR ]578]" },
                new { Unicode = "ਇਤਿ ਸ੍ਰੀ ਬਚਿਤ੍ਰ ਨਾਟਕੇ ਮਨੁ ਰਾਜਾ ਕੋ ਰਾਜ ਸਮਾਪਤੰ ॥੧॥੫॥", Ascii = "ieiq sRI bicqR nwtky mnu rwjw ko rwj smwpqM ]1]5]" },
                new { Unicode = "ਢਾਕਨਹਾਰੇ ਪ੍ਰਭੂ ਹਮਾਰੇ ਜੀਅ ਪ੍ਰਾਨ ਸੁਖਦਾਤੇ ॥", Ascii = "Fwknhwry pRBU hmwry jIA pRwn suKdwqy ]" },
                new { Unicode = "ਮੰਤ੍ਰੁ", Ascii = "mMqRü" },
                new { Unicode = "ਤਿਸੁ ਵਿਣੁ ਸਭੁ ਅਪਵਿਤ੍ਰੁ ਹੈ ਜੇਤਾ ਪੈਨਣੁ ਖਾਣੁ ॥", Ascii = "iqsu ivxu sBu ApivqRü hY jyqw pYnxu Kwxu ]" },
                new { Unicode = "ਸੋਢੀ ਸ੍ਰਿਸ੍ਟਿ ਸਕਲ ਤਾਰਣ ਕਉ ਅਬ ਗੁਰ ਰਾਮਦਾਸ ਕਉ ਮਿਲੀ ਬਡਾਈ ॥੩॥", Ascii = "soFI isRis† skl qwrx kau Ab gur rwmdws kau imlI bfweI ]3]" },
                new { Unicode = "ਭੰਜਨ ਗੜ੍ਹਣ ਸਮਥੁ ਤਰਣ ਤਾਰਣ ਪ੍ਰਭੁ ਸੋਈ ॥", Ascii = "BMjn gVHx smQu qrx qwrx pRBu soeI ]" },
                new { Unicode = "ਰਾਗੁ ਗਉੜੀ ਥਿਤੰੀ ਕਬੀਰ ਜੀ ਕੰੀ ॥", Ascii = "rwgu gauVI iQqµØI kbIr jI kµØI ]" },
                new { Unicode = "ਆਤਮਾ ਬਾਸੁਦੇਵਸੵਿ ਜੇ ਕੋ ਜਾਣੈ ਭੇਉ ॥", Ascii = "Awqmw bwsudyvis´ jy ko jwxY Byau ]" },
                new { Unicode = "ਅਸਮਾਨ ਮੵਿਾਨੇ ਲਹੰਗ ਦਰੀਆ ਗੁਸਲ ਕਰਦਨ ਬੂਦ ॥", Ascii = "Asmwn im´wny lhMg drIAw gusl krdn bUd ]" },
                new { Unicode = "ਦੁਰਲਭੰ ਏਕ ਭਗਵਾਨ ਨਾਮਹ ਨਾਨਕ ਲਬਧੵਿੰ ਸਾਧਸੰਗਿ ਕ੍ਰਿਪਾ ਪ੍ਰਭੰ ॥੩੫॥", Ascii = "durlBM eyk Bgvwn nwmh nwnk lbiD´M swDsMig ik®pw pRBM ]35]" },
                new { Unicode = "ਜੇਨ ਕਲਾ ਸਸਿ ਸੂਰ ਨਖੵਤ੍ਰ ਜੋਤੵਿੰ ਸਾਸੰ ਸਰੀਰ ਧਾਰਣੰ ॥", Ascii = "jyn klw sis sUr nK´qR joiq´M swsM srIr DwrxM ]" },
                new { Unicode = "ਬਸੵਿੰਤ ਰਿਖਿਅੰ ਤਿਆਗਿ ਮਾਨੰ ॥", Ascii = "bis´Mq iriKAM iqAwig mwnµ ]" },
                new { Unicode = "ਸ਼ ਖ਼ ਗ਼ ਜ਼ ਫ਼ ਲ਼", Ascii = "S ^ Z z & L" }
            };
            
            Assert.All(words, w => Assert.Equal(GurmukhiUtils.ToAsciiGurmukhi(w.Unicode), w.Ascii));
        }
        
        [Fact]
        public void IsGurmukhi()
        {
            var words = new[]
            {
                new { Gurmukhi = "ਗੁਰਮੁਖੀ", IsGurmukhi = true  },
                new { Gurmukhi = "ਮੈਂ ਗੁਰਮੁਖੀ ਵਿਚ ਲਿਖ ਰਿਹਾ ਹਾਂ।", IsGurmukhi = true  },
                new { Gurmukhi = "ਲੜੀਵਾਰ​ਗੁਰਬਾਣੀ", IsGurmukhi = true  }, // Has U+200B, Zero Width Space
                new { Gurmukhi = "मैं हिंदी में लिख रहा हूँ।", IsGurmukhi = false  },
                new { Gurmukhi = "میں شاہ رخ میں لکھ رہا ہوں۔", IsGurmukhi = false  },
                new { Gurmukhi = "ਗੁਰਮੁਖੀ & English", IsGurmukhi = true  },
                new { Gurmukhi = "English & ਗੁਰਮੁਖੀ", IsGurmukhi = false  }
            };

            Assert.All(words, w => Assert.Equal(GurmukhiUtils.IsGurmukhi(w.Gurmukhi), w.IsGurmukhi));
        }
    }
}