 
## Problem 


download.microsoft.com


Shield icon  
There is a problem with this website’s security certificate.


This organization's certificate has been revoked.


Security certificate problems may indicate an attempt to fool you or intercept any data you send to the server.  
   
We recommend that you close this webpage and do not continue to this website.  

   
Recommended iconClick here to close this webpage. 
 
   
Not recommended icon 
 
    

More information  More information  


## Solution

[how to prevent "There is a problem with this website's security certificate" error](https://social.technet.microsoft.com/Forums/zh-CN/46c8bf9f-97ea-498c-b153-eeca9cafa716/how-to-prevent-there-is-a-problem-with-this-websites-security-certificate-error?forum=winserversecurity)

Hello,
To get rid of the error, you can either (1) get a trusted certificate from a trusted CA (godaddy, VeriSign) or
 (2) trust  the certificate issued by your server.
 
To do the latter:

1. In Explorer Options, add the URL to your trusted sites. Exit Explorer.
2. Open Explorer again and navigate to the site and click continue to this Web site.
3. Click on the certificate error then select view certificates.
4. Click install certificate and place it in your trusted certificates authority.
5. Exit Explorer then open the page again. Error should be gone.

Note: There cannot be a mismatch. i.e. you cannot trust an issued cert for sharepoint.domain.com if the site you 
are visiting is www.domain.com. If that's the case, you will still get the error.







 
