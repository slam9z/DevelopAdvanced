[New line in text area](http://stackoverflow.com/questions/8627902/new-line-in-text-area)


I think you are confusing the syntax of different languages.

    &#10; is (the HtmlEncoded value of ASCII 10 or) the linefeed character literal in a HTML string. It does NOT render as a line break in HTML (see notes at bottom).

    \n is the linefeed character literal (ASCII 10) in a Javascript string.

    <br/> is a line break in HTML. Many other elements, eg <p>, <div>, etc also render line breaks unless overridden with some styles.

Hopefully the following illustration will make it clearer:

T.innerText = "Position of LF: " + t.value.indexOf("\n");

```js
p1.innerHTML = t.value;
p2.innerHTML = t.value.replace("\n", "<br/>");
p3.innerText = t.value.replace("\n", "<br/>");

<textarea id="t">Line 1&#10;Line 2</textarea>

<p id='T'></p>
<p id='p1'></p>
<p id='p2'></p>
<p id='p3'></p>
```