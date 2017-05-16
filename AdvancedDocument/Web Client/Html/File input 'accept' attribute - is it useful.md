[File input 'accept' attribute - is it useful?](http://stackoverflow.com/questions/181214/file-input-accept-attribute-is-it-useful)

##answer


The accept attribute is incredibly useful. It is a hint to browsers to only show files that are
 allowed for the current input. While it can typically be overridden by users, it helps narrow 
 down the results for users by default, so they can get exactly what they're looking for without 
 having to sift through a hundred different file types.

Usage

Note: These examples were written based on the current specification and may not actually
 work in all (or any) browsers. The specification may also change in the future, which could 
 break these examples.

```css
h1 { font-size: 1em; margin:1em 0; }
h1 ~ h1 { border-top: 1px solid #ccc; padding-top: 1em; }
```

```html
<h1>Match all image files (image/*)</h1>
<p><label>image/* <input type="file" accept="image/*"></label></p>

<h1>Match all video files (video/*)</h1>
<p><label>video/* <input type="file" accept="video/*"></label></p>

<h1>Match all audio files (audio/*)</h1>
<p><label>audio/* <input type="file" accept="audio/*"></label></p>

<h1>Match all image files (image/*) and files with the extension ".someext"</h1>
<p><label>.someext,image/* <input type="file" accept=".someext,image/*"></label></p>

<h1>Match all image files (image/*) and video files (video/*)</h1>
<p><label>image/*,video/* <input type="file" accept="image/*,video/*"></label></p>
```