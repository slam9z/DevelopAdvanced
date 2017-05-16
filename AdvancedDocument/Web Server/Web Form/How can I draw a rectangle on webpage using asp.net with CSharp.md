﻿[How can I draw a rectangle on webpage using asp.net with c#? ](http://stackoverflow.com/questions/15295193/how-can-i-draw-a-rectangle-on-webpage-using-asp-net-with-c)


perhaps something like this:

```c#
<%@ Page Language="C#" ContentType="image/jpeg" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Text" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>

<%

Response.Clear();
int height = 100;
int width = 200;
Random r = new Random();
int x = r.Next(75);

Bitmap bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
Graphics g = Graphics.FromImage(bmp);

g.TextRenderingHint = TextRenderingHint.AntiAlias;
g.Clear(Color.Orange);
g.DrawRectangle(Pens.White, 1, 1, width-3, height-3);
g.DrawRectangle(Pens.Gray, 2, 2, width-3, height-3);
g.DrawRectangle(Pens.Black, 0, 0, width, height);
g.DrawString("The Code Project", new Font("Arial", 12, FontStyle.Italic), 
SystemBrushes.WindowText, new PointF(x,50) );

bmp.Save(Response.OutputStream, ImageFormat.Jpeg);
g.Dispose();
bmp.Dispose();
Response.End();

%>

```
Source:
[Source drawing Web Graphics](http://www.codeproject.com/Articles/1827/Web-Graphics-On-The-Fly-in-ASP-NET)


使用FillRectangle画填充的Rectangle。
