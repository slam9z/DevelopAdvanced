[How to get JSON POST data in ASP.NET Web Forms?](http://stackoverflow.com/questions/29175594/how-to-get-json-post-data-in-asp-net-web-forms)

##Question

I currently have some jquery that is POSTing data onto one of my web pages.
Right now I'm just trying to get it to post some JSON to test it out, but I can't figure out have to actually
 get the data in my back-end once it's posted.
I've always used Request.Params to get posted data, but it doesn't seem to be working this time.
This is the code I'm using to do the post:
// This data is just for testing purposes, doesn't actually do anything

```js
var person = {
    name: "Bob",
    address: "123 Main St.",
    phone: "555-5555"
}

var jqxhr = $.ajax({
    type: "POST",
    url: "/example/mypage.aspx",
    contentType: 'application/json; charset=utf-8',
    dataType: "json",
    timeout: 0,
    success: function () {
        alert("Success");
    },
    error: function (xhr, status, error) {
        alert(error);
    },
    data: person
});
```
##Answer

The post is definitely successful though, as I can see it using Fiddler, plus when I check Request.ContentLength
 it returns the right number of bytes that was posted.
But I can't find the actual data anywhere. Any ideas on what I'm doing wrong?
Thanks in advance.

Posting javascript object:
pass the plain object to the data option,
leave the contentType option alone. The default option is perfect.
Then you can access the property values of the object in the Request collection as if you have posted a form.


server side:

```cs
   string input;
    using(var reader = new StreamReader(Request.InputStream)){
            input = reader.ReadToEnd();
        }
Posting Json:
data: JSON.stringify(person),
contentType: "application/json"
server side:
string json;
using(var reader = new StreamReader(Request.InputStream)){
        json = reader.ReadToEnd();
    }
var person = Json.Decode(json);
```

Referenced from: http://www.mikesdotnetting.com/article/220/posting-data-with-jquery-ajax-in-asp-net-razor-web-pages