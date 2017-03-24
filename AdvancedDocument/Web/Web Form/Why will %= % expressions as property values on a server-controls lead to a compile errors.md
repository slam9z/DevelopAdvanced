[Why will <%= %> expressions as property values on a server-controls lead to a compile errors?](http://stackoverflow.com/questions/370201/why-will-expressions-as-property-values-on-a-server-controls-lead-to-a-co)


## answer
	

This:

```xml
<asp:Button runat="server" id="Button1" visible='<%= true %>' />
```

Does not evaluate to this:

```xml
<asp:Button runat="server" id="Button1" visible='true' />
```
<%= %> outputs directly to the response stream, and the asp markup is not part of the response stream. Its a mistake to assume the <%= %> operators are performing any kind of preprocessing on the asp markup.

As an aside, it helps to think about the ASP.NET lifecycle with respect to the <%# %> and <%= %> operators.

1. <%# %> has semantics more in common with assigning a value to an object. In the ASP.NET lifecycle, the <%# %> operators are evaluated before the page writes the first byte to the response buffer.

2. <%= %> means the same thing as Response.Write. We need to perform all of our databinding and form processing first, and output HTML to the response buffer at the very end of the ASP.NET lifecycle.

