[“X-UA-Compatible” content=“IE=9; IE=8; IE=7; IE=EDGE”](http://stackoverflow.com/questions/14611264/x-ua-compatible-content-ie-9-ie-8-ie-7-ie-edge)

## answer

For versions of Internet Explorer 8 and above, this:

```html
<meta http-equiv="X-UA-Compatible" content="IE=9; IE=8; IE=7" />
```

Forces the browser to render as that particular version's standards. It is not supported for IE7 and below.

If you separate with semi-colon, it sets compatibility levels for different versions. For example:

```html
<meta http-equiv="X-UA-Compatible" content="IE=7; IE=9" />
```

Renders IE7 and IE8 as IE7, but IE9 as IE9. It allows for different levels of backwards compatibility. In real life, though, you should only chose one of the options:

```html
<meta http-equiv="X-UA-Compatible" content="IE=8" />
```

This allows for much easier testing and maintenance. Although generally the more useful version of this is using Emulate:

```html
<meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
```

For this:

```html
<meta http-equiv="X-UA-Compatible" content="IE=Edge" />
```

It forces the browser the render at whatever the most recent version's standards are. Just like using the latest version of jQuery on Google's CDN, this is the most recent, but also can potentially break your code since its not a fixed version.

Last, but not least, consider adding this little tidbit:

```html
<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
```

Adding "chrome=1" will allow the site to render in ChromeFrame for those (intelligent) users who have it, without affecting anyone else.

For more information, there is plenty to read here, and if you want to learn about ChromeFrame (which I recommend) you can learn about its implementation here.

UPDATE

Since the time of this post, ChromeFrame maintenance has been discontinued. That said, keeping this code will not harm or slow anything, and for those people that are still using ChromeFrame because they had it installed prior to the discontinuing, it will still work.
