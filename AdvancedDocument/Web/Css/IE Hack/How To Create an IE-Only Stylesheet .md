[How To Create an IE-Only Stylesheet ](https://css-tricks.com/how-to-create-an-ie-only-stylesheet/)

Note that IE 10 and up DO NOT support conditional comments at all.

> 好像只有加载的时候有用！

# Target ALL VERSIONS of IE

```html
<!--[if IE]>
	<link rel="stylesheet" type="text/css" href="all-ie-only.css" />
<![endif]-->
```

# Target everything EXCEPT IE

```html
<!--[if !IE]><!-->
	<link rel="stylesheet" type="text/css" href="not-ie.css" />
 <!--<![endif]-->
```

# Target IE 7 ONLY

```html
<!--[if IE 7]>
	<link rel="stylesheet" type="text/css" href="ie7.css">
<![endif]-->
```

# Target IE 6 ONLY

```html
<!--[if IE 6]>
	<link rel="stylesheet" type="text/css" href="ie6.css" />
<![endif]-->
```

# Target IE 5 ONLY

```html
<!--[if IE 5]>
	<link rel="stylesheet" type="text/css" href="ie5.css" />
<![endif]-->
```

# Target IE 5.5 ONLY

```html
<!--[if IE 5.5000]>
<link rel="stylesheet" type="text/css" href="ie55.css" />
<![endif]-->
```

# Target IE 6 and LOWER

```html
<!--[if lt IE 7]>
	<link rel="stylesheet" type="text/css" href="ie6-and-down.css" />
<![endif]-->
```

```html
<!--[if lte IE 6]>
	<link rel="stylesheet" type="text/css" href="ie6-and-down.css" />
<![endif]-->
```

# Target IE 7 and LOWER

```html
<!--[if lt IE 8]>
	<link rel="stylesheet" type="text/css" href="ie7-and-down.css" />
<![endif]-->

<!--[if lte IE 7]>
	<link rel="stylesheet" type="text/css" href="ie7-and-down.css" />
<![endif]-->
```

# Target IE 8 and LOWER

```html
<!--[if lt IE 9]>
	<link rel="stylesheet" type="text/css" href="ie8-and-down.css" />
<![endif]-->

<!--[if lte IE 8]>
	<link rel="stylesheet" type="text/css" href="ie8-and-down.css" />
<![endif]-->
```


```html
# Target IE 6 and HIGHER

<!--[if gt IE 5.5]>
	<link rel="stylesheet" type="text/css" href="ie6-and-up.css" />
<![endif]-->

<!--[if gte IE 6]>
	<link rel="stylesheet" type="text/css" href="ie6-and-up.css" />
<![endif]-->
```

# Target IE 7 and HIGHER

```html
<!--[if gt IE 6]>
	<link rel="stylesheet" type="text/css" href="ie7-and-up.css" />
<![endif]-->

<!--[if gte IE 7]>
	<link rel="stylesheet" type="text/css" href="ie7-and-up.css" />
<![endif]-->
```

# Target IE 8 and HIGHER

```html
<!--[if gt IE 7]>
	<link rel="stylesheet" type="text/css" href="ie8-and-up.css" />
<![endif]-->

<!--[if gte IE 8]>
	<link rel="stylesheet" type="text/css" href="ie8-and-up.css" />
<![endif]-->
```