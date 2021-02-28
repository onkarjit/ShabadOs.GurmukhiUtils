using System.Linq;
using ShabadOS.GurmukhiUtils.Enums;
using Xunit;

namespace ShabadOS.GurmukhiUtils.Test
{
    public class StripTests
    {
        [Fact]
        public void StripAccentsTest()
        {
            var words = new[]
            {
                new {Input = "ਜ਼ਫ਼ੈਸ਼ਸਓ", Output = "ਜਫੈਸਸੳ"},
                new {Input = "Z^Svb", Output = "gKsvb"}
            };

            Assert.All(words, w => Assert.Equal(GurmukhiUtils.StripAccents(w.Input), w.Output));
        }

        [Fact]
        public void StripAllVishraamsTest()
        {
            var lines = new[]
            {
                new
                {
                    Input = "Anhd sbd vjwey, hir jIau Gir Awey; hir gux gwvhu nwrI ]",
                    Output = "Anhd sbd vjwey hir jIau Gir Awey hir gux gwvhu nwrI ]"
                },
                new
                {
                    Input = "Anhd sbd vjwey hir jIau Gir Awey hir gux gwvhu nwrI ]",
                    Output = "Anhd sbd vjwey hir jIau Gir Awey hir gux gwvhu nwrI ]"
                },
            };

            Assert.All(lines, w => Assert.Equal(GurmukhiUtils.StripVishraams(w.Input), w.Output));
        }

        [Fact]
        public void StripHeavyVishraamsTest()
        {
            var lines = new[]
            {
                new
                {
                    Input = "Anhd sbd vjwey, hir jIau Gir Awey; hir gux gwvhu nwrI ]",
                    Output = "Anhd sbd vjwey, hir jIau Gir Awey hir gux gwvhu nwrI ]"
                },
                new {Input = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ; ]", Output = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ ]"},
            };

            Assert.All(lines, w => Assert.Equal(GurmukhiUtils.StripVishraams(w.Input, Vishraam.Heavy), w.Output));
        }

        [Fact]
        public void StripMediumVishraamsTest()
        {
            var lines = new[]
            {
                new
                {
                    Input = "Anhd sbd vjwey, hir jIau Gir Awey; hir gux gwvhu nwrI ]",
                    Output = "Anhd sbd vjwey hir jIau Gir Awey; hir gux gwvhu nwrI ]"
                },
            };

            Assert.All(lines, w => Assert.Equal(GurmukhiUtils.StripVishraams(w.Input, Vishraam.Medium), w.Output));
        }

        [Fact]
        public void StripLightVishraamsTest()
        {
            var lines = new[]
            {
                new
                {
                    Input = "Anhd sbd vjwey, hir jIau Gir Awey; hir gux gwvhu nwrI ]",
                    Output = "Anhd sbd vjwey, hir jIau Gir Awey; hir gux gwvhu nwrI ]"
                },
                new {Input = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ; ]", Output = "ਸਬਦਿ ਮਰੈ ਸੋ ਮਰਿ ਰਹੈ; ]"},
            };

            Assert.All(lines, w => Assert.Equal(GurmukhiUtils.StripVishraams(w.Input, Vishraam.Light), w.Output));
        }

        [Fact]
        public void StripMediumHeavyVishraamsTest()
        {
            var lines = new[]
            {
                new
                {
                    Input = "Anhd sbd vjwey, hir jIau Gir Awey; hir gux gwvhu nwrI ]",
                    Output = "Anhd sbd vjwey hir jIau Gir Awey hir gux gwvhu nwrI ]"
                },
                new {Input = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ; ]", Output = "ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ ]"},
            };

            Assert.All(lines, w => Assert.Equal(GurmukhiUtils.StripVishraams(w.Input, Vishraam.Heavy | Vishraam.Medium), w.Output));
        }
        
        [Fact]
        public void StripEndingsTest()
        {
            var gurmukhiPassages = new[]
            {
                new {Input = "ਸੋ ਘਰੁ ਰਾਖੁ; ਵਡਾਈ ਤੋਇ ॥੧॥ ਰਹਾਉ ॥", Output = "ਸੋ ਘਰੁ ਰਾਖੁ; ਵਡਾਈ ਤੋਇ"},
                new
                {
                    Input = "ਹੁਕਮੁ ਪਛਾਣਿ; ਤਾ ਖਸਮੈ ਮਿਲਣਾ ॥੧॥ ਰਹਾਉ ਦੂਜਾ ॥",
                    Output = "ਹੁਕਮੁ ਪਛਾਣਿ; ਤਾ ਖਸਮੈ ਮਿਲਣਾ"
                },
                new
                {
                    Input = "ਨਾਮੁ ਅਧਾਰੁ ਦੀਜੈ. ਨਾਨਕ ਕਉ; ਆਨਦ ਸੂਖ ਘਨੇਰੈ ॥੨॥੧੨॥ ਛਕੇ ੨ ॥",
                    Output = "ਨਾਮੁ ਅਧਾਰੁ ਦੀਜੈ. ਨਾਨਕ ਕਉ; ਆਨਦ ਸੂਖ ਘਨੇਰੈ"
                },
                new
                {
                    Input = "ਜਪਿ ਜਪਿ ਨਾਮੁ ਜੀਵਾ ਤੇਰੀ ਬਾਣੀ; ਨਾਨਕ ਦਾਸ ਬਲਿਹਾਰੇ ॥੨॥੧੮॥ ਛਕੇ ੩ ॥",
                    Output = "ਜਪਿ ਜਪਿ ਨਾਮੁ ਜੀਵਾ ਤੇਰੀ ਬਾਣੀ; ਨਾਨਕ ਦਾਸ ਬਲਿਹਾਰੇ"
                },
                new
                {
                    Input = "ਸਹਸ ਸਿਆਣਪ ਨਹ ਮਿਲੈ, ਮੇਰੀ ਜਿੰਦੁੜੀਏ; ਜਨ ਨਾਨਕ. ਗੁਰਮੁਖਿ ਜਾਤਾ ਰਾਮ ॥੪॥੬॥ ਛਕਾ ੧ ॥",
                    Output = "ਸਹਸ ਸਿਆਣਪ ਨਹ ਮਿਲੈ, ਮੇਰੀ ਜਿੰਦੁੜੀਏ; ਜਨ ਨਾਨਕ. ਗੁਰਮੁਖਿ ਜਾਤਾ ਰਾਮ"
                },
                new
                {
                    Input = "ਆਇ ਮਿਲੁ ਗੁਰਸਿਖ. ਆਇ ਮਿਲੁ; ਤੂ ਮੇਰੇ ਗੁਰੂ ਕੇ ਪਿਆਰੇ ॥ ਰਹਾਉ ॥",
                    Output = "ਆਇ ਮਿਲੁ ਗੁਰਸਿਖ. ਆਇ ਮਿਲੁ; ਤੂ ਮੇਰੇ ਗੁਰੂ ਕੇ ਪਿਆਰੇ"
                },
                new
                {
                    Input = "ਜਹਾ ਤੇ ਉਪਜਿਆ; ਫਿਰਿ ਤਹਾ ਸਮਾਵੈ ।੪੯।੧। ਇਕੁ ।",
                    Output = "ਜਹਾ ਤੇ ਉਪਜਿਆ; ਫਿਰਿ ਤਹਾ ਸਮਾਵੈ"
                },
                new
                {
                    Input = "ਇਸੁ ਬੰਦੇ ਸਿਰਿ ਜੁਲਮੁ ਹੋਤ ਹੈ; ਜਮੁ. ਨਹੀ ਹਟੈ ਗੁਸਾਈ ॥੪॥੯॥ ਦੁਤੁਕੇ",
                    Output = "ਇਸੁ ਬੰਦੇ ਸਿਰਿ ਜੁਲਮੁ ਹੋਤ ਹੈ; ਜਮੁ. ਨਹੀ ਹਟੈ ਗੁਸਾਈ"
                },
                new
                {
                    Input = "ਨਾਨਕ. ਮਨਿ ਤਨਿ ਚਾਉ ਏਹੁ; ਨਿਤ ਪ੍ਰਭ ਕਉ ਲੋੜੇ ॥੨੧॥੧॥ ਸੁਧੁ ਕੀਚੇ",
                    Output = "ਨਾਨਕ. ਮਨਿ ਤਨਿ ਚਾਉ ਏਹੁ; ਨਿਤ ਪ੍ਰਭ ਕਉ ਲੋੜੇ"
                },
                new
                {
                    Input =
                        "ਤਿਸੀ ਪ੍ਰਕਾਰ ਹੀ ਗੁਰੂ ਸੇਵਾ ਤਥਾ ਨਾਮ ਵਿਖੇ ਪਿੰਡ ਸਰੀਰ ਦੇ ਪਰਚੇ ਬਿਨਾਂ ਜੋ ਸਿੱਖਾਂ ਦੀ ਭਿਛ੍ਯਾ ਖਾਵੇ ਅਰਥਾਤ ਓਨਾਂ ਦੀ ਕਾਰ ਭੇਟ ਦਾ ਆਯਾ ਪਦਾਰਥ ਖਾਏ ਤਾਂ ਅੰਤ ਕਾਲ ਨੂੰ ਔਕੜ ਹੋਯਾ ਕਰਦੀ ਹੈ, ਤੇ ਜਮ ਲੋਕ ਨਰਕ ਨੂੰ ਜਾਣਾ ਪੈਂਦਾ ਹੈ ॥੫੧੭॥ ਪੜ੍ਹੋ ਵੀਚਾਰ ਕਬਿੱਤ ੫੦੬",
                    Output =
                        "ਤਿਸੀ ਪ੍ਰਕਾਰ ਹੀ ਗੁਰੂ ਸੇਵਾ ਤਥਾ ਨਾਮ ਵਿਖੇ ਪਿੰਡ ਸਰੀਰ ਦੇ ਪਰਚੇ ਬਿਨਾਂ ਜੋ ਸਿੱਖਾਂ ਦੀ ਭਿਛ੍ਯਾ ਖਾਵੇ ਅਰਥਾਤ ਓਨਾਂ ਦੀ ਕਾਰ ਭੇਟ ਦਾ ਆਯਾ ਪਦਾਰਥ ਖਾਏ ਤਾਂ ਅੰਤ ਕਾਲ ਨੂੰ ਔਕੜ ਹੋਯਾ ਕਰਦੀ ਹੈ, ਤੇ ਜਮ ਲੋਕ ਨਰਕ ਨੂੰ ਜਾਣਾ ਪੈਂਦਾ ਹੈ"
                },
                new
                {
                    Input =
                        "ਸ੍ਰੀ ਗੁਰੂ ਜੀ ਕਹਤੇ ਹੈਂ: ਜੋ ਰਾਤ ਦਿਨ ਨਾਮ ਜਪਤੇ ਹੈਂ ਐਸੇ ਗੁਰੋਂ ਕੇ ਮੁਖ ਸੇ ਵਾਹੁ ਵਾਹੁ ਬਾਣੀ ਪ੍ਰਾਪਤ ਹੋਤੀ ਹੈ॥੧॥",
                    Output =
                        "ਸ੍ਰੀ ਗੁਰੂ ਜੀ ਕਹਤੇ ਹੈਂ: ਜੋ ਰਾਤ ਦਿਨ ਨਾਮ ਜਪਤੇ ਹੈਂ ਐਸੇ ਗੁਰੋਂ ਕੇ ਮੁਖ ਸੇ ਵਾਹੁ ਵਾਹੁ ਬਾਣੀ ਪ੍ਰਾਪਤ ਹੋਤੀ ਹੈ"
                },
                new
                {
                    Input =
                        "ਹੇ ਨਾਨਕ! ਜੋ ਮਨੁੱਖ ਗੁਰੂ ਦੇ ਹੁਕਮ ਵਿਚ ਤੁਰਦਾ ਹੈ ਉਸ ਨੂੰ ਸਿਫ਼ਤ-ਸਾਲਾਹ ਦੀ ਦਾਤ ਮਿਲਦੀ ਹੈ, ਉਹ ਹਰ ਵੇਲੇ ਪ੍ਰਭੂ ਦਾ ਨਾਮ ਜਪਦਾ ਹੈ ॥੧॥",
                    Output =
                        "ਹੇ ਨਾਨਕ! ਜੋ ਮਨੁੱਖ ਗੁਰੂ ਦੇ ਹੁਕਮ ਵਿਚ ਤੁਰਦਾ ਹੈ ਉਸ ਨੂੰ ਸਿਫ਼ਤ-ਸਾਲਾਹ ਦੀ ਦਾਤ ਮਿਲਦੀ ਹੈ, ਉਹ ਹਰ ਵੇਲੇ ਪ੍ਰਭੂ ਦਾ ਨਾਮ ਜਪਦਾ ਹੈ"
                },
                new
                {
                    Input =
                        "ਸ੍ਰੀ ਗੁਰੂ ਜੀ ਕਹਤੇ ਹੈਂ: ਸੋ ਹਰਿ ਨਾਮ ਕਾ ਜੋ ਰਾਤ ਦਿਨ ਭਜਨ ਕਰਤਾ ਹੈ ਤਿਸ ਕੇ ਸਭ ਕਾਮ ਸਫਲ ਹੋਤੇ ਹੈਂ ਅਰਥਾਤ ਪੂਰਨ ਕਾਮ ਹੋਤਾ ਹੈ॥੨੦",
                    Output =
                        "ਸ੍ਰੀ ਗੁਰੂ ਜੀ ਕਹਤੇ ਹੈਂ: ਸੋ ਹਰਿ ਨਾਮ ਕਾ ਜੋ ਰਾਤ ਦਿਨ ਭਜਨ ਕਰਤਾ ਹੈ ਤਿਸ ਕੇ ਸਭ ਕਾਮ ਸਫਲ ਹੋਤੇ ਹੈਂ ਅਰਥਾਤ ਪੂਰਨ ਕਾਮ ਹੋਤਾ ਹੈ"
                },
                new {Input = "॥ ਜਪੁ ॥", Output = "ਜਪੁ"},
                new {Input = "ਸੋ ਦਰੁ ਰਾਗੁ ਆਸਾ ਮਹਲਾ ੧", Output = "ਸੋ ਦਰੁ ਰਾਗੁ ਆਸਾ ਮਹਲਾ"},
                new
                {
                    Input = "ਸੂਰਜੁ; ਏਕੋ ਰੁਤਿ ਅਨੇਕ ॥ ਨਾਨਕ ਕਰਤੇ ਕੇ ਕੇਤੇ ਵੇਸ ॥੨॥੨॥",
                    Output = "ਸੂਰਜੁ; ਏਕੋ ਰੁਤਿ ਅਨੇਕ ਨਾਨਕ ਕਰਤੇ ਕੇ ਕੇਤੇ ਵੇਸ"
                },
                new
                {
                    Input = "ਭਇਓ ਕ੍ਰਿਪਾਲੁ. ਦੀਨ ਦੁਖ ਭੰਜਨੁ; ਹਰਿ ਹਰਿ ਕੀਰਤਨਿ ਇਹੁ ਮਨੁ ਰਾਤਾ ॥ ਰਹਾਉ ਦੂਜਾ ॥੧॥੩॥",
                    Output = "ਭਇਓ ਕ੍ਰਿਪਾਲੁ. ਦੀਨ ਦੁਖ ਭੰਜਨੁ; ਹਰਿ ਹਰਿ ਕੀਰਤਨਿ ਇਹੁ ਮਨੁ ਰਾਤਾ"
                },
                new {Input = "ਕਿ ਹਰ ਹਸ਼ਤੋ ਸ਼ਸਤ ਆਮਦਾ ਚਾਕਰਸ਼ ।੧੪੮।", Output = "ਕਿ ਹਰ ਹਸ਼ਤੋ ਸ਼ਸਤ ਆਮਦਾ ਚਾਕਰਸ਼"}
            };

            var asciiPassages = gurmukhiPassages.Select(p =>
                new
                {
                    Input = GurmukhiUtils.ToAsciiGurmukhi(p.Input),
                    Output = GurmukhiUtils.ToAsciiGurmukhi(p.Output)
                });

            var translationPassages = new[]
            {
                new
                {
                    Input = "True Here And Now. O Nanak, Forever And Ever True. ||1||",
                    Output = "True Here And Now. O Nanak, Forever And Ever True."
                },
                new
                {
                    Input =
                        "By Guru's Grace, the supreme status is obtained, and the dry wood blossoms forth again in lush greenery. ||1||Pause||",
                    Output =
                        "By Guru's Grace, the supreme status is obtained, and the dry wood blossoms forth again in lush greenery.",
                },
                new
                {
                    Input =
                        "He puts on various ornaments and many decorations, but it is like dressing a corpse. ||Pause||",
                    Output = "He puts on various ornaments and many decorations, but it is like dressing a corpse."
                },
                new
                {
                    Input =
                        "Servant Nanak is devoted, dedicated, forever a sacrifice to You, Lord. Your Expanse has no limit, no boundary. ||4||5||",
                    Output =
                        "Servant Nanak is devoted, dedicated, forever a sacrifice to You, Lord. Your Expanse has no limit, no boundary."
                },
                new
                {
                    Input = "lifts him to the divine level of supreme consciousness. (103",
                    Output = "lifts him to the divine level of supreme consciousness."
                },
                new
                {
                    Input = "What is the purpose of my life (it became worthless) without your reminiscence.(1) (3)",
                    Output = "What is the purpose of my life (it became worthless) without your reminiscence."
                },
                new
                {
                    Input =
                        "Just one of his (blessed) looks, that prolongs life instantly, is enough for me.\" (2) (2)\nSometimes, he acts like a mystic, sometimes like a meditator, and other times like a carefree recluse;\nHe is our pilot steering our way; he operates in numerous different postures. (2) (3)",
                    Output =
                        "Just one of his (blessed) looks, that prolongs life instantly, is enough for me.\"\nSometimes, he acts like a mystic, sometimes like a meditator, and other times like a carefree recluse;\nHe is our pilot steering our way; he operates in numerous different postures."
                },
                new {Input = "always I live within the Khalsa. 519", Output = "always I live within the Khalsa."},
                new {Input = "Salutation to Thee O Abodeless Lord! 5", Output = "Salutation to Thee O Abodeless Lord!"},
                new
                {
                    Input = "Thy Virtues like Generosity are countless.91",
                    Output = "Thy Virtues like Generosity are countless."
                },
                new {Input = "It was Bikrami Samvat 1753", Output = "It was Bikrami Samvat"},
                new
                {
                    Input = "He also forgot the unmanifested Brahmin and said this583",
                    Output = "He also forgot the unmanifested Brahmin and said this"
                },
                new
                {
                    Input = "Somewhere Thou art manifesting the mode of Tamas in a kingly mood!  16. 106",
                    Output = "Somewhere Thou art manifesting the mode of Tamas in a kingly mood!"
                },
                new
                {
                    Input =
                        "He is Without blemish and stain and be visualised as consisting of Indestructible Glory .16.176",
                    Output = "He is Without blemish and stain and be visualised as consisting of Indestructible Glory ."
                },
                new
                {
                    Input = "He is the Sustainer of all the beings and creatures!9. 239",
                    Output = "He is the Sustainer of all the beings and creatures!"
                },
                new {Input = "Thee as the Abode of Dharma.2.254", Output = "Thee as the Abode of Dharma."},
                new
                {
                    Input = "He said to the warriors fighting near him,1069",
                    Output = "He said to the warriors fighting near him,"
                },
                new
                {
                    Input = "Through 8.4 million incarnations you have wandered",
                    Output = "Through 8.4 million incarnations you have wandered"
                },
                new
                {
                    Input =
                        "Noche y día oh, dice Nanak, quien medite y vibre en el Nombre del Señor, ve cómo todos sus esfuerzos dan fruto. (20)",
                    Output =
                        "Noche y día oh, dice Nanak, quien medite y vibre en el Nombre del Señor, ve cómo todos sus esfuerzos dan fruto."
                },
                new
                {
                    Input =
                        "A tal ser el Señor lo encuentra y nunca más lo abandona, ya que lo inmerge en Su Paz Maravillosa.  ()1",
                    Output =
                        "A tal ser el Señor lo encuentra y nunca más lo abandona, ya que lo inmerge en Su Paz Maravillosa."
                },
                new
                {
                    Input =
                        "Misteriosa es la manera de los verdaderos Discípulos, porque no solamente escuchan la Instrucción del Guru, sino que se encuentran totalmente sobrellevados por ella. 25",
                    Output =
                        "Misteriosa es la manera de los verdaderos Discípulos, porque no solamente escuchan la Instrucción del Guru, sino que se encuentran totalmente sobrellevados por ella."
                },
                new
                {
                    Input =
                        "El mundo ha nacido para morir y es siempre destruido, una, otra, y otra vez; solamente uno se vuelve Eterno aferrándose a los Pies del Guru. (4‑6‑13",
                    Output =
                        "El mundo ha nacido para morir y es siempre destruido, una, otra, y otra vez; solamente uno se vuelve Eterno aferrándose a los Pies del Guru."
                },
                new
                {
                    Input = "Toda la Maya es Tu Deleite. Nanak, Tu Esclavo, ofrece su ser en sacrificio a Ti. (4-2-9)",
                    Output = "Toda la Maya es Tu Deleite. Nanak, Tu Esclavo, ofrece su ser en sacrificio a Ti."
                },
                new
                {
                    Input = "ese hombre ha recibido la Aprobación de Dios.  (4-40-47)",
                    Output = "ese hombre ha recibido la Aprobación de Dios."
                },
                new
                {
                    Input =
                        "स्री गुरू जी कहते हैं: जो रात दिन नाम जपते हैं ऐसे गुरों के मुख से वाहु वाहु बाणी प्रापत होती है॥१॥",
                    Output =
                        "स्री गुरू जी कहते हैं: जो रात दिन नाम जपते हैं ऐसे गुरों के मुख से वाहु वाहु बाणी प्रापत होती है"
                },
                new
                {
                    Input = "नानक. मनि तनि चाउ एहु; नित प्रभ कउ लोड़े ॥२१॥१॥ सुधु कीचे",
                    Output = "नानक. मनि तनि चाउ एहु; नित प्रभ कउ लोड़े"
                }
            };

            var passages = gurmukhiPassages.Concat(asciiPassages).Concat(translationPassages);

            Assert.All(passages, p => Assert.Equal(GurmukhiUtils.StripEndings(p.Input), p.Output));
        }
    }
}