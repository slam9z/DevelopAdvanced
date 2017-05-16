[Cross-subdomain ajax request denied even when document.domain is set correctly](http://stackoverflow.com/questions/7735955/cross-subdomain-ajax-request-denied-even-when-document-domain-is-set-correctly)

##answer

document.domain doesn't work with AJAX. It is intended for cross domain iframe and window communication. 
In your case you are violating the same origin policy (last line of the table) so you need to use either JSONP
or server side bridge.
Here's a very [nice guide](http://usejquery.com/blog/jquery-cross-domain-ajax-guide)
which illustrates different techniques for achieving cross domain AJAX requests.