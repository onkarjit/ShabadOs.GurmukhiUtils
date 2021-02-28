<h1>Gurmukhi Utils</h1>

This is a rewrite of Shabad OS's [gurmukhi-utils] to C#. Gurmukhi Utils are general utilities for working with Gurmukhi text data.

```c#
using ShabadOS.GurmukhiUtils;

var unicodeGurmukhi = "ਸੋ ਘਰੁ ਰਾਖੁ; ਵਡਾਈ ਤੋਇ ॥੧॥ ਰਹਾਉ ॥"
var asciiGurmukhi = "so Gru rwKu; vfweI qoie ]1] rhwau ]"

GurmukhiUtils.ToAsciiGurmukhi(unicodeGurmukhi) // => so Gru rwKu; vfweI qoie ]1] rhwau ]
GurmukhiUtils.ToUnicodeGurmukhi(asciiGurmukhi) // => ਸੋ ਘਰੁ ਰਾਖੁ; ਵਡਾਈ ਤੋਇ ॥੧॥ ਰਹਾਉ ॥
GurmukhiUtils.ToEnglish(asciiGurmukhi) // => so ghar raakh; vaddaaee toie |1| rahaau |
GurmukhiUtils.StripEndings(GurmukhiUtils.ToEnglish(asciiGurmukhi)) // => so ghar raakh; vaddaaee toie
```

**Table of Contents**

<!-- toc -->

- [Introduction](#introduction)
- [Usage](#usage)
- [API](#api)
    * [CountSyllables(text) ⇒ int](#countsyllablestext--int)
    * [FirstLetters(line) ⇒ string](#firstlettersline--string)
    * [IsGurmukhi(text, [exhaustive]) ⇒ bool](#isgurmukhitext-exhaustive--bool)
    * [StripAccents(text) ⇒ string](#stripaccentstext--string)
    * [StripEndings(text) ⇒ string](#stripendingstext--string)
    * [StripVishraams(text, [vishraams]) ⇒ string](#stripvishraamstext-vishraams--string)
    * [ToAsciiGurmukhi(text) ⇒ string](#toasciigurmukhitext--string)
    * [ToEnglish(line) ⇒ string](#toenglishline--string)
    * [ToHindi(text) ⇒ string](#tohinditext--string)
    * [ToShahmukhi(text) ⇒ string](#toshahmukhitext--string)
    * [ToSyllabicSymbols(text) ⇒ string](#tosyllabicsymbolstext--string)
    * [ToUnicodeGurmukhi(text) ⇒ string](#tounicodegurmukhitext--string)
- [Contributing](#contributing)
- [Acknowledgements](#acknowledgements)
- [License](#license)

<!-- tocstop -->

## Introduction

Gurmukhi Utils is a library for converting, analyzing, and testing gurmukhi strings.

## Usage

Gurmukhi Utils C# rewrite is available as [ShabadOS.GurmukhiUtils](https://www.nuget.org/packages/ShabadOS.GurmukhiUtils) on [NuGet](https://www.nuget.org/).

1. Install [ShabadOS.GurmukhiUtils](https://www.nuget.org/packages/ShabadOS.GurmukhiUtils) package via NuGet.
2. Add `using ShabadOS.GurmukhiUtils`
3. Done!

## API

### CountSyllables(text) ⇒ <code>int</code>
Calculates the number of syllables according to Sanskrit prosody, Pingala, Matra/Meter/Morae.

**Returns**: <code>int</code> - An integer adding up all the 1"s (laghu/light/short) and 2"s (guru/heavy/long).

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The string to analyze |

**Example**
```C#
CountSyllables("ਪ੍ਰਭੂ ਪ੍ਰੇਮੀ ਪੜ੍ਹ ਚੜ੍ਹ ਦ੍ਵੈਤ")
// expected output: 14
```
### FirstLetters(line) ⇒ <code>string</code>
Generates the first letters for a unicode Gurmukhi,
Hindi transliteration, or English transliteration string.
Includes any end-word vishraams, and line-end characters.

**Returns**: <code>string</code> - The first letters of each word in the provided Gurmukhi line.

| Param | Type | Description |
| --- | --- | --- |
| line | <code>string</code> | The line to generate the first letters for. |

**Example** *(Gurmukhi first letters)*
```c#
FirstLetters("ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ; ਫਿਰਿ. ਮਰੈ ਨ, ਦੂਜੀ ਵਾਰ ॥") // => ਸਮ.ਸਮਰ;ਫ.ਮਨ,ਦਵ॥
```
**Example** *(Hindi first letters)*
```c#
FirstLetters("गुरमुखि लाधा मनमुखि गवाइआ ॥") // => गलमग॥
```
**Example** *(English first letters)*
```c#
FirstLetters("sabad marai. so mar rahai; fir. marai na, doojee vaar |") // => sm.smr;f.mn,dv|
```
### IsGurmukhi(text, [exhaustive]) ⇒ <code>bool</code>
Checks if first char in string is part of the Gurmukhi Unicode block.

**Returns**: <code>bool</code> - True if Unicode Gurmukhi, false if other.

| Param | Type | Default | Description |
| --- | --- | --- | --- |
| text | <code>string</code> |  | The text to check. |
| [exhaustive] | <code>bool</code> | `false` | If `true`, checks if the whole string is Unicode Gurmukhi. |

**Example**
```C#
IsGurmukhi("ਗੁਰਮੁਖੀ") // => true
IsGurmukhi("gurmuKI") // => false
```
### StripAccents(text) ⇒ <code>string</code>
Removes accents from ASCII/Unicode Gurmukhi letters with their base letter.
Useful for generalising search queries.

**Returns**: <code>string</code> - A simplified version of the provided Gurmukhi string.

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The text to convert. |

**Example**
```C#
StripAccents("ਜ਼ਫ਼ੈਸ਼ਸਓ") // => ਜਫੈਸਸੳ
StripAccents("Z^Svb") // => gKsvb
```
### StripEndings(text) ⇒ <code>string</code>
Strips line endings from any Gurmukhi or translation string.
Accepts both Unicode and ASCII input.
Useful for generating accurate first letters or modifying non-Gurbani for better display.
*Not* designed for headings or Sirlekhs.

**Returns**: <code>string</code> - A ending-less version of the text.

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The text to stip endings from. |

**Example** *(Line ending phrases)*
```C#
StripEndings("ਸੋ ਘਰੁ ਰਾਖੁ; ਵਡਾਈ ਤੋਇ ॥੧॥ ਰਹਾਉ ॥") // => ਸੋ ਘਰੁ ਰਾਖੁ; ਵਡਾਈ ਤੋਇ
StripEndings("ਹੁਕਮੁ ਪਛਾਣਿ; ਤਾ ਖਸਮੈ ਮਿਲਣਾ ॥੧॥ ਰਹਾਉ ਦੂਜਾ ॥") // => ਹੁਕਮੁ ਪਛਾਣਿ; ਤਾ ਖਸਮੈ ਮਿਲਣਾ
StripEndings("ਜਨ ਨਾਨਕ. ਗੁਰਮੁਖਿ ਜਾਤਾ ਰਾਮ ॥੪॥੬॥ ਛਕਾ ੧ ॥") // => ਜਨ ਨਾਨਕ. ਗੁਰਮੁਖਿ ਜਾਤਾ ਰਾਮ
```
**Example** *(English Translations)*
```c#
StripEndings("O Nanak, Forever And Ever True. ||1||") // => O Nanak, Forever And Ever True.
StripEndings("lush greenery. ||1||Pause||") // => lush greenery.
StripEndings("always I live within the Khalsa. 519") // => always I live within the Khalsa.
StripEndings("without your reminiscence.(1) (3)") // => without your reminiscence.
```
**Example** *(Spanish Translations)*
```C#
StripEndings("ofrece su ser en sacrificio a Ti. (4-2-9)") // => ofrece su ser en sacrificio a Ti.
```
### StripVishraams(text, [vishraams]) ⇒ <code>string</code>
Removes the specified vishraams from a string.

**Returns**: <code>string</code> - A vishraam-less Gurmukhi string.

| Param | Type | Default | Description |
| --- | --- | --- | --- |
| text | <code>string</code> | | The text to remove vishraams from. |
| [vishraams] | <code>Vishraam</code> | Vishraam.Heavy &#124; Vishraam.Medium &#124; Vishraam.Light | The vishraams to remove. Defaults to all. |

**Example** *(Text only, default options)*
```C#
StripVishraams("ਸਬਦਿ ਮਰੈ. ਸੋ ਮਰਿ ਰਹੈ;") // => "ਸਬਦਿ ਮਰੈ ਸੋ ਮਰਿ ਰਹੈ
StripVishraams("sbid mrY. so mir rhY; iPir.") // => sbid mrY so mir rhY iPir
```
**Example** *(Heavy vishraams only)*
```C#
StripVishraams("sbid mrY. so mir rhY; iPir.", Vishraam.Heavy) // => sbid mrY. so mir rhY iPir.
```
**Example** *(Medium vishraams only)*
```C#
StripVishraams("Anhd sbd vjwey,", Vishraam.Medium) // => Anhd sbd vjwey
```
**Example** *(Light vishraams only)*
```C#
StripVishraams("sbid mrY. so, mir rhY; iPir.", Vishraam.Light) // => sbid mrY so mir rhY; iPir
```

**Example** *(Heavy and Medium vishraams only)*
```C#
StripVishraams("sbid mrY. so, mir rhY; iPir.", Vishraam.Heavy | Vishraam.Medium) // => sbid mrY. so mir rhY iPir.
```
### ToAsciiGurmukhi(text) ⇒ <code>string</code>
Converts Gurmukhi unicode text to ASCII, used GurmukhiAkhar font.

**Returns**: <code>string</code> - An ASCII representation of the provided unicode Gurmukhi string.

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The unicode text to convert. |

**Example**
```C#
ToAsciiGurmukhi("ਹਮਾ ਸਾਇਲਿ ਲੁਤਫ਼ਿ ਹਕ ਪਰਵਰਸ਼ ॥") // => hmw swieil luqi& hk prvrS ]
ToAsciiGurmukhi("ਸੁ ਬੈਠਿ ਇਕੰਤ੍ਰ ॥੫੭੮॥") // => su bYiT iekMqR ]578]
```
### ToEnglish(line) ⇒ <code>string</code>
Transliterates a line from Unicode Gurmukhi to english.
Currently supports the `,`, `;`, `.` vishraam characters.

**Returns**: <code>string</code> - The English transliteration of the provided Gurmukhi line.

| Param | Type | Description |
| --- | --- | --- |
| line | <code>string</code> | The Gurmukhi Unicode line to transliterate. |

**Example**
```C#
ToEnglish("ਹੁਕਮੀ ਹੁਕਮੁ ਚਲਾਏ ਰਾਹੁ ॥") // => hukamee hukam chalaae raahu ||
```
**Example**
```C#
ToEnglish("ਭਾਂਡਾ ਭਾਉ ਅੰਮ੍ਰਿਤੁ ਤਿਤੁ ਢਾਲਿ ॥") // => bhaa(n)ddaa bhaou anmrit tit dtaal ||
```
### ToHindi(text) ⇒ <code>string</code>
Transliterates Unicode Gurmukhi text to Hindi (Devanagari script).

**Returns**: <code>string</code> - A Hindi transliteration of the provided Unicode Gurmukhi string.

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The Unicode Gurmukhi text to convert. |

**Example**
```C#
ToHindi("ਕੁਲ ਜਨ ਮਧੇ ਮਿਲੵੋਿ ਸਾਰਗ ਪਾਨ ਰੇ ॥") // => कुल जन मधे मिल्यो सारग पान रे ॥
ToHindi("ਸੁ ਬੈਠਿ ਇਕੰਤ੍ਰ ॥੫੭੮॥") // => सु बैठ इकंत्र ॥५७८॥
```
### ToShahmukhi(text) ⇒ <code>string</code>
Transliterates Unicode Gurmukhi text to the Shahmukhi script.

**Returns**: <code>string</code> - A Shahmukhi transliteration of the provided Unicode Gurmukhi string.

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The Unicode Gurmukhi text to convert. |

**Example**
```C#
ToShahmukhi("ਹਮਾ ਸਾਇਲਿ ਲੁਤਫ਼ਿ ਹਕ ਪਰਵਰਸ਼ ॥") // => هما ساِال لُتف هک پرورش ۔۔
ToShahmukhi("ਸੁ ਬੈਠਿ ਇਕੰਤ੍ਰ ॥੫੭੮॥") // => سُ بَےٹھ ِاکںتر ۔۔۵۷۸۔۔
```
### ToSyllabicSymbols(text) ⇒ <code>string</code>
Represents text in syllables according to Sanskrit prosody, Pingala, Matra/Meter/Morae

**Returns**: <code>string</code> - A syllabic representation of 1"s (laghu/light/short) and 2"s (guru/heavy/long).

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The string to convert |

**Example**
```C#
ToSyllabicSymbols( "ਪ੍ਰਭੂ ਪ੍ਰੇਮੀ ਪੜ੍ਹ ਚੜ੍ਹ ਦ੍ਵੈਤ" )
// expected output: "12 22 11 11 21"
```
### ToUnicodeGurmukhi(text) ⇒ <code>string</code>
Converts ASCII text used in the GurmukhiAkhar font to Unicode.

**Returns**: <code>string</code> - A unicode representation of the provided ASCII Gurmukhi string.

| Param | Type | Description |
| --- | --- | --- |
| text | <code>string</code> | The ASCII text to convert. |

**Example**
```C#
ToUnicodeGurmukhi("kul jn mDy imil´o swrg pwn ry ]") // => ਕੁਲ ਜਨ ਮਧੇ ਮਿਲੵੋਿ ਸਾਰਗ ਪਾਨ ਰੇ ॥
ToUnicodeGurmukhi("su bYiT iekMqR ]578]") // => ਸੁ ਬੈਠਿ ਇਕੰਤ੍ਰ ॥੫੭੮॥
```

## Contributing

There are multiple ways to contribute whether you are a user or developer. For example:

- [Submit bugs and feature requests][new-issue-url].
- Review documentation and make pull requests for anything from typos to new content.
- Give feedback on the onboarding process to make it easier for others to join the project.

## Acknowledgements
All credits go to the [maintainers] of [gurmukhi-utils] for creating this wonderful tool which aids in creating Gurmukhi related tools and services.

[@Onkarjit](https://github.com/onkarjit/) is the maintainer of this repository which is a C# rewrite of [gurmukhi-utils].

## License

Licensed under the [GPL v3](LICENSE.MD).


[new-issue-url]: https://github.com/onkarjit/GurmukhiUtils/issues/new
[gurmukhi-utils]: https://github.com/shabados/gurmukhi-utils
[maintainers]: https://github.com/shabados/gurmukhi-utils#people