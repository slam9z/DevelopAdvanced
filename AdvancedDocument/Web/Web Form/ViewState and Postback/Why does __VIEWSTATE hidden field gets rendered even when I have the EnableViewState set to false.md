[Why does __VIEWSTATE hidden field gets rendered even when I have the EnableViewState set to false](http://stackoverflow.com/questions/283082/why-does-viewstate-hidden-field-gets-rendered-even-when-i-have-the-enableviews)


##Question 

I saw that __VIEWSTATE field gets rendered even though I have set the EnableViewState="false" at the page level.
 This field is not rendered if I remove runat="server" tag for the form element. Can somebody please explain this?


##Best Answer

The __VIEWSTATE field is also used to store control state, which is not optional. Furthermore, t
he information contained in the view state is used to validate the postback, 
if I'm not mistaken (and validation is enabled, which is the default). 
So as long as you have the form with runat="server", you'll have a viewstate field.
 However, you should notice a much smaller field size if you disable all viewstate.
  
