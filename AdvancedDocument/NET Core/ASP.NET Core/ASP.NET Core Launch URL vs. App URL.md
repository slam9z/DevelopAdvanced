[ASP.NET Core: Launch URL vs. App URL](http://stackoverflow.com/questions/42650194/asp-net-core-launch-url-vs-app-url)


## answer


As far as i can tell:

* Launch Url is the url you host your dotnet core application on and to which port the kestrel server is listening.

* Websettings, AppUrl is the url IIS is listening to. IIS (which is basically a reverse proxy here) will forward all the http request comming from the AppUrl to the Launch URL.

If you remove the Launch URL kestrel has no port to listen to, and will throw an error on startup, or fallback to port 5000.
