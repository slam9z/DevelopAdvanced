[User Authentication in ASP.NET Web API](http://stackoverflow.com/questions/11731683/user-authentication-in-asp-net-web-api)


## answer

I am working on a MVC5/Web API project and needed to be able to get authorization for the Web Api methods. When my index view is first loaded I make a call to the 'token' Web API method which I believe is created automatically.
The client side code (CoffeeScript) to get the token is:

```js
getAuthenticationToken = (username, password) ->
    dataToSend = "username=" + username + "&password=" + password
    dataToSend += "&grant_type=password"
    $.post("/token", dataToSend).success 
    
    saveAccessToken
```

If successful the following is called, which saves the authentication token locally:

```js
saveAccessToken = (response) ->
    window.authenticationToken = response.access_token
```

Then if I need to make an Ajax call to a Web API method that has the [Authorize] tag I simply add the following header to my Ajax call:

```js
{ "Authorization": "Bearer " + window.authenticationToken }
```