[Permission Denied IE iFrame](http://stackoverflow.com/questions/2953158/permission-denied-ie-iframe)


##question

I have a site on A.com and an iframe on B.com which calls javascript from A.com. This works great in FF. 
In IE7 / 8 I am getting a 

Message: Access is denied.

message. I have checked the HTTP Traffic via Fiddler - and I can see that it isn't blocked in Fiddler ?
Any ideas what could be causing this and how to solve?
internet-explorer iframe permission-denied 


##answer

If both the pages are under your control (i.e. they belong to you and you cn alter the code), try this - 
http://www.tomhoppe.com/index.php/2008/03/cross-sub-domain-javascript-ajax-iframe-etc/
Set document.domain like this:

```html
<script type="text/javascript">
document.domain = 'tomhoppe.com';
</script> 
```


##answer

IFrames can communicate as long as they are "of the same origin" - so same domain and same protocol. 
Communication is blocked if they are of different origin.

HTML5 introduces a new communication mechanism. It may be worth looking at http://www.w3.org/TR/webmessaging/

http://en.wikipedia.org/wiki/Cross-document_messaging

It is also worth reading up on the security implications

https://www.owasp.org/index.php/HTML5_Security_Cheat_Sheet#Web_Messaging