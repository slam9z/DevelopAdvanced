[Is there a way to define a default value to be selected in an HTML Form of Radio Buttons?](http://stackoverflow.com/questions/8967795/is-there-a-way-to-define-a-default-value-to-be-selected-in-an-html-form-of-radio)



Just set it as checked:

```html
<body>
  <form>
    <input type="radio" name="amount" value="10"/> $10&#8194 
    <input type="radio" name="amount" value="25"/> $25&#8194 
    <input type="radio" name="amount" value="50" checked="checked" /> $50&#8194
    <input type="radio" name="amount" value="100"/> $100&#8194
    <input type="radio" name="amount" value="250"/> $250&#8194
    <input type="radio" name="amount" value="other"/> Other
  </form>
</body>
```

