[XML](https://en.wikipedia.org/wiki/XML)

## Escaping

XML provides escape facilities for including characters that are problematic to include directly. For example:

* The characters "<" and "&" are key syntax markers and may never appear in content outside a CDATA section.[14]
* Some character encodings support only a subset of Unicode. For example, it is legal to encode an XML document in ASCII, but ASCII lacks code points for Unicode characters such as "é".
* It might not be possible to type the character on the author's machine.
* Some characters have glyphs that cannot be visually distinguished from other characters, such as the non-breaking space (&#xa0;) " " and the space (&#x20;) " ", and the Cyrillic capital letter A (&#x410;) "А" and the Latin capital letter A (&#x41;) "A".
There are five predefined entities:

* &lt; represents "<";
* &gt; represents ">";
* &amp; represents "&";
* &apos; represents "'";
* &quot; represents '"'.

All permitted Unicode characters may be represented with a numeric character reference. Consider the Chinese character "中", whose numeric code in Unicode is hexadecimal 4E2D, or decimal 20,013. A user whose keyboard offers no method for entering this character could still insert it in an XML document encoded either as &#20013; or &#x4e2d;. Similarly, the string "I <3 Jörg" could be encoded for inclusion in an XML document as I &lt;3 J&#xF6;rg.


&#0; is not permitted, however, because the null character is one of the control characters excluded from XML, even when using a numeric character reference.[15] An alternative encoding mechanism such as Base64 is needed to represent such characters.