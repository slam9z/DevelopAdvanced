[How to refresh an IFrame using Javascript?](http://stackoverflow.com/questions/2064850/how-to-refresh-an-iframe-using-javascript)

##Question


I have a webpage with an IFrame and a Button, once the button is pressed I need the IFrame to be refreshed. 
Is this possible, if so how? I searched and could not find any answers.


##Answer

```js
var iframe = document.getElementById('youriframe');
iframe.src = iframe.src;
```

###comments 
  
Does not work in Chrome. At least not in Chrome 19... – Tarlog Jun 18 '12 at 10:08 
  
It works for me in this version of Chrome: Version 24.0.1312.56 Ubuntu 12.04 (24.0.1312.56-0ubuntu0.12.04.1)
– Paul A Jungwirth Mar 13 '13 at 17:13 
  
FYI - There is currently (as of January 2013) a Chromium project bug (code.google.com/p/chromium/issues/detail?id=172859) 
which causes iframe updates to add to document history. – Robert Altman Apr 10 '13 at 16:58 
  
Did not work for chrome 27! – user2019515 May 27 '13 at 6:40 
  
var tmp_src = iframe.src; iframe.src = ''; iframe.src = tmp_src; – lyfing Jan 27 '15 at 8:28 
