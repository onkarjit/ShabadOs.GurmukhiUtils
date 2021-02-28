using Xunit;

namespace ShabadOS.GurmukhiUtils.Test
{
    public class EnglishTests
    {
        [Fact]
        public void ToEnglish()
        {
            var words = new[]
            {
                  new { Gurmukhi = "ਕਿਵ ਸਚਿਆਰਾ ਹੋਈਐ ਕਿਵ ਕੂੜੈ ਤੁਟੈ ਪਾਲਿ ॥", English = "kiv sachiaaraa hoeeai kiv koorrai tuttai paal |" },
                  new { Gurmukhi = "ਕਨਕੰ ਦੁਤਿ ਉਜਲ ਅੰਗ ਸਨੇ ॥", English = "kanakan dut ujal ang sane |" },
                  new { Gurmukhi = "ਗੁਨ ਮ੍ਰਿਗਹਾ ਕੋ ਚਿਤ ਬੀਚ ਲੀਆ ॥੩੮੫॥", English = "gun mrigahaa ko chit beech leea |385|" },
                  new { Gurmukhi = "ਗਣ ਗੰਧ੍ਰਬ ਭੂਤ ਪਿਸਾਚ ਤਬੈ ॥੩੮੮॥", English = "gan gandhrab bhoot pisaach tabai |388|" },
                  new { Gurmukhi = "ਅਬ੍ਰਯਕਤ ਅੰਗ ॥", English = "abrayakat ang |" },
                  new { Gurmukhi = "ਜਗੰਨਾਥ ਜਗਦੀਸੁਰ ਕਰਤੇ ਸਭ ਵਸਗਤਿ ਹੈ ਹਰਿ ਕੇਰੀ ॥", English = "jaganaath jagadeesur karate sabh vasagat hai har keree |" },
                  new { Gurmukhi = "ਆਦਿ ਸਚੁ ਜੁਗਾਦਿ ਸਚੁ ॥", English = "aad sach jugaad sach |" },
                  new { Gurmukhi = "ਸਹਸ ਸਿਆਣਪਾ ਲਖ ਹੋਹਿ ਤ ਇਕ ਨ ਚਲੈ ਨਾਲਿ ॥", English = "sehas siaanapaa lakh hohi ta ik na chalai naal |" },
                  new { Gurmukhi = "ੴ ਸਤਿ ਨਾਮੁ ਕਰਤਾ ਪੁਰਖੁ ਨਿਰਭਉ ਨਿਰਵੈਰੁ ਅਕਾਲ ਮੂਰਤਿ ਅਜੂਨੀ ਸੈਭੰ ਗੁਰ ਪ੍ਰਸਾਦਿ ॥", English = "ik oankaar sat naam karataa purakh nirbhau niravair akaal moorat ajoonee saibhan gur prasaad |" },
                  new { Gurmukhi = "ਹੁਕਮੀ ਹੁਕਮੁ ਚਲਾਏ ਰਾਹੁ ॥", English = "hukamee hukam chalaae raahu |" },
                  new { Gurmukhi = "ਤਿਨ ਕੇ ਨਾਮ ਅਨੇਕ ਅਨੰਤ ॥", English = "tin ke naam anek anant |" },
                  new { Gurmukhi = "ਭਾਂਡਾ ਭਾਉ ਅੰਮ੍ਰਿਤੁ ਤਿਤੁ ਢਾਲਿ ॥", English = "bhaanddaa bhaau amrit tith dtaal |" },
                  new { Gurmukhi = "ਕ੍ਰਿਪਾ", English = "kripaa" },
                  new { Gurmukhi = "ਮਃ", English = "mahalaa" },
                  new { Gurmukhi = "ਜਿਸ ਨੋ ਕ੍ਰਿਪਾ ਕਰਹਿ ਤਿਨਿ ਨਾਮੁ ਰਤਨੁ ਪਾਇਆ ॥", English = "jis no kripaa kareh tin naam ratan paaeaa |" },
                  new { Gurmukhi = "ਆਵਣੁ ਵੰਞਣੁ ਡਾਖੜੋ ਛੋਡੀ ਕੰਤਿ ਵਿਸਾਰਿ ॥੪॥", English = "aavan vanyan ddaakharro chhoddee kant visaar |4|" },
                  new { Gurmukhi = "ਘੜੀ ਮੂਰਤ ਸਿਮਰਤ ਪਲ ਵੰਞਹਿ ਜੀਵਣੁ ਸਫਲੁ ਤਿਥਾਈ ਜੀਉ ॥੧॥", English = "gharree moorat simarat pal vanyeh jeevan safal tithaaee jeeo |1|" },
                  new { Gurmukhi = "ਹਰਿ ਹਰਿ ਹਰਿ ਗੁਨ ਗਾਵਹੁ ॥", English = "har har har gun gaavahu |" },
                  new { Gurmukhi = "ਹੁਕਮੈ ਅੰਦਰਿ. ਸਭੁ ਕੋ; ਬਾਹਰਿ ਹੁਕਮ. ਨ ਕੋਇ ॥", English = "hukamai andar. sabh ko; baahar hukam. na koe |" },
                  new { Gurmukhi = "ਸਹਜ; ਸਸਹਜ ਅਨਹਦ ਰਹਤ ਕਹਤ ਪਹਰ, ਸਹਸ ਮਹਲ ਟਹਲ ਕਹਨਨ ਕਹਨ", English = "sehaj; sasahaj anahad rehat kehat pehar, sehas mehal ttehal kahanan kehan" },
                  new { Gurmukhi = "ਸਭ ਭਇਓ ਪਰਾਇਓ", English = "sabh bheo paraaeo" },
                  new { Gurmukhi = "ਆਸਾ ਮਹਲਾ ੫ ਪੰਚਪਦੇ₃ ॥", English = "aasaa mahalaa 5 panchapade₃ |" },
                  new { Gurmukhi = "ਹਰਿ", English = "har" },
                  new { Gurmukhi = "ਸਚੁ", English = "sach" },
                  new { Gurmukhi = "ਰਾਹੁ", English = "raahu" },
                  new { Gurmukhi = "ਭਾਉ", English = "bhaau" },
                  new { Gurmukhi = "ਸਤਿਗੁਰੁ ਸਤਿਗੁਰੁ ਸਚੁ; ਸਚੁ ਹਰਿ ਹਰਿ ਹਿੰਙੁ", English = "satigur satigur sach; sach har har hing" },
                  new { Gurmukhi = "ਸੁ ਉ ਜੁ", English = "su u ju" }
            };
            
            Assert.All(words, w => Assert.Equal(GurmukhiUtils.ToEnglish(w.Gurmukhi), w.English));
        }
    }
}