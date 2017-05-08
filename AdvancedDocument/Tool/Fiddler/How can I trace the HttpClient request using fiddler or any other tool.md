[How can I trace the HttpClient request using fiddler or any other tool?](http://stackoverflow.com/questions/22500299/how-can-i-trace-the-httpclient-request-using-fiddler-or-any-other-tool)


## answer1

> 一个笨拙的方法，但是有用。

you are connecting with a url like <http://localhost:1234> change it to <http://localhost.fiddler:1234/> and the requests from HttpClient should then become visible in Fiddler.
