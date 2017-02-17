[What causing this “Invalid length for a Base-64 char array”](http://stackoverflow.com/questions/858761/what-causing-this-invalid-length-for-a-base-64-char-array)



After urlDecode processes the text, it replaces all '+' chars with ' ' ... thus the error. You should simply call this statement to make it base 64 compatible again:

```cs
sEncryptedString = sEncryptedString.Replace(' ', '+');
```