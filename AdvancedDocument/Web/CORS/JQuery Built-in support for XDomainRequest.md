[Built-in support for XDomainRequest ](https://bugs.jquery.com/ticket/8283)

##Summary of the XDomainRequest issue: 

* IE 6, 7, 8, and 9 do not support XHR2 CORS. It is not possible to make generalized cross-domain requests in these browsers. 
* IE 8, 9 support an ActiveX control called XDomainRequest that only allows limited cross-domain requests compared to XHR2 CORS. 
* IE 10 supports XHR2 CORS. 
* jQuery does not include XDomainRequest support because there are ​numerous and serious limitations to XDR. Many reasonable
 $.ajax requests would fail, including any cross-domain request made on IE6 and IE7 which are otherwise supported by jQuery. 
Developrers would be confused that their content types and headers were ignored, or that IE8 users couldn't use XDR if the 
user was using InPrivate browsing for example. 

* Even the crippled XDR can be useful if it is used by a knowledgeable developer. A jQuery team member has made an ​XDR ajax 
transport available. You must be aware of XDR limitations by ​reading this blog post or ask someone who has dealt with XDR
 problems and can mentor you through its successful use. 

* For further help and other solutions, ask on the jQuery Forum, StackOverflow, or search "jQuery xdr transport". Requests 
posted here will be deleted. 