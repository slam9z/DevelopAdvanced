[Is launchSettings.json used when running ASP.NET 5 apps from the command line on Mac?](http://stackoverflow.com/questions/34553048/is-launchsettings-json-used-when-running-asp-net-5-apps-from-the-command-line-on)


## answer

LaunchSettings.json is strictly a VS concept. In other cases, you will have to configure environment variables as commands below:

For standard command line run, use:

set ASPNET_ENV=Development

dnx web

For powershell, use:

$env:ASPNET_ENV='Development'

dnx web

Shorter version: dnx web ASPNET_ENV=Development


> 火大,除了用VS，我怎么制定网站的域名。

