[HTTP OPTIONS](https://forums.iis.net/p/1149525/1870237.aspx)

##answer

with IIS 5/6 you can remove WebDAV if it's not necessary, and use Urlscan to remove http Options verb

http://www.microsoft.com/technet/security/tools/urlscan.mspx

For IIS 7 you must use Request Filtering

http://learn.iis.net/page.aspx/143/how-to-use-request-filtering/

http options verb 