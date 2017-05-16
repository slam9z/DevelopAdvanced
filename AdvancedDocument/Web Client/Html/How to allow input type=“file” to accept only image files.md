[How to allow <input type=“file”> to accept only image files](http://stackoverflow.com/questions/3828554/how-to-allow-input-type-file-to-accept-only-image-files)

##answer

You can't achieve this using standard input control. Common techniques include validating 
this on the server side or use some Flash upload control which allows more customizations.

Also bare in mind that verifying the file extension is a necessary but not a sufficient 
condition that a file is image. There's nothing preventing the user from renaming an 
executable to .jpg for example.

##answer

Use the accept parameter of the input tag. So to accept only PNGs, JPEGs and GIFs you can use 
the following code:

```html
<input type="file" name="myImage" accept="image/x-png, image/gif, image/jpeg" />
```

Or simply:

```html
<input type="file" name="myImage" accept="image/*" />
```

Note that this only provides a hint to the browser as to what file-types to display to the 
user, but this can be easily circumvented, so you should always validate the uploaded file o
n the server also.

It should work in IE 10+, Chrome, Firefox, Safari 6+, Opera 15+, but support is very sketchy
on mobiles (as of 2015) and by some reports this may actually prevent some mobile browsers
from uploading anything at all, so be sure to test your target platforms well.

For detailed browser support, see http://caniuse.com/#feat=input-file-accept