[Introduction to session and application state in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state)

HTTP is a stateless protocol. A web server treats each HTTP request as an independent request and does not retain user values from previous requests. This article discusses different ways to preserve application and session state between requests.

## Session state

Session state is a feature in ASP.NET Core that you can use to save and store user data while the user browses your web app. Consisting of a dictionary or hash table on the server, session state persists data across requests from a browser. The session data is backed by a cache.

ASP.NET Core maintains session state by giving the client a `cookie` that contains the `session ID`, which is sent to the server with each request. 

> Do not store sensitive data in session. The client might not close the browser and clear the session cookie (and some browsers keep session cookies alive across windows). Also, a session might not be restricted to a single user; the next user might continue with the same session.


## Cookie-based TempData provider


## Setting and getting Session values

## Working with HttpContext.Items


## Application state data


