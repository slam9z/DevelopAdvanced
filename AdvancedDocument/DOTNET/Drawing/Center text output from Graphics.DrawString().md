[Center text output from Graphics.DrawString()](http://stackoverflow.com/questions/7991/center-text-output-from-graphics-drawstring)

To align a text use the following:

```cs
StringFormat sf = new StringFormat();
sf.LineAlignment = StringAlignment.Center;
sf.Alignment = StringAlignment.Center;
e.Graphics.DrawString("My String", this.Font, Brushes.Black, ClientRectangle, sf);
```

Please note that the text here is aligned in the given bounds. In this sample this is the ClientRectangle. 