[How to show math equations in general github's markdown(not github's blog)](http://stackoverflow.com/questions/11256433/how-to-show-math-equations-in-general-githubs-markdownnot-githubs-blog)

##question 

fter my investigating, I found mathjax can do this. But when write some example in my markdown file, it can't show 
the correct equations:
I have added this in the head of markdown file:

```js
<script type="text/javascript" src="http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=default"></script>
```

And type the mathjax statement:

```
(E=mc^2)，$$x_{1,2} = \frac{-b \pm \sqrt{b^2-4ac}}{2b}.$$
```
But github show nothing for the math symbols! please help me, thanks! Tell me how to show math symbols in general
github markdown.


##answer


GitHub markdown parsing is performed by the SunDown (ex libUpSkirt) library.

The motto of the library is "Standards compliant, fast, secure markdown processing library in C". The important word
 being "secure" there, considering your question :). 

Indeed, allowing javascript to be executed would be a bit off of the MarkDown standard text-to-HTML contract.

Moreover, everything that looks like a HTML tag is either escaped or stripped out.

Tell me how to show math symbols in general github markdown.

Your best bet would be to find a website similar to yuml.me which can generate on-the-fly images from by parsing the
provided URL querystring.

###Update

I've found some sites providing users with such service: codedogs.com (no longer seems to support embedding) or iTex2Img.
You may want to try them out. Of course, others may exist and some Google-fu will help you find them.

given the following markdown syntax
![equation](http://www.sciweavers.org/tex2img.php?eq=1%2Bsin%28mc%5E2%29&bc=White&fc=Black&im=jpg&fs=12&ff=arev&edit=)
it will display the following image

Note: In order for the image to be properly displayed, you'll have to ensure the querystring part of the url is percent 
encoded. You can easily find online tools to help you with that task, such as www.url-encode-decode.com