[Using OAuth2 in HTML5 Web App](http://stackoverflow.com/questions/18280827/using-oauth2-in-html5-web-app)

## answer

It looks like you're using the Resource Owner Password Credentials OAuth 2.0 flow e.g. submitting username/pass to get back both an access token and refresh token.

    The access token CAN be exposed in javascript, the risks of the access token being exposed somehow are mitigated by its short lifetime.
    The refresh token SHOULD NOT be exposed to client-side javascript. It's used to get more access tokens (as you're doing above) but if an attacker was able to get the refresh token they'd be able to get more access tokens at will until such time as the OAuth server revoked the authorization of the client for which the refresh token was issued.

With that background in mind, let me address your questions:

    Either a cookie or localstorage will give you local persistence across page refreshes. Storing the access token in local storage gives you a little more protection against CSRF attacks as it will not be automatically sent to the server like a cookie will. Your client-side javascript will need to pull it out of localstorage and transmit it on each request. I'm working on an OAuth 2 app and because it's a single page approach I do neither; instead I just keep it in memory.
    I agree... if you're storing in a cookie it's just for the persistence not for expiration, the server is going to respond with an error when the token expires. The only reason I can think you might create a cookie with an expiration is so that you can detect whether it has expired WITHOUT first making a request and waiting for an error response. Of course you could do the same thing with local storage by saving that known expiration time.
    This is the crux of the whole question I believe... "How do I get a refresh token, without A, storing it with the original access_token to use later, and B) also storing a client_id". Unfortunately you really can't... As noted in that introductory comment, having the refresh token client side negates the security provided by the access token's limited lifespan. What I'm doing in my app (where I'm not using any persistent server-side session state) is the following:
        The user submits username and password to the server
        The server then forwards the username and password to the OAuth endpoint, in your example above http://domain.com/api/oauth/token, and receives both the access token and refresh token.
        The server encrypts the refresh token and sets it in a cookie (should be HTTP Only)
        The server responds with the access token ONLY in clear text (in a JSON response) AND the encrypted HTTP only cookie
        client-side javascript can now read and use the access token (store in local storage or whatever
        When the access token expires, the client submits a request to the server (not the OAuth server but the server hosting the app) for a new token
        The server, receives the encrypted HTTP only cookie it created, decrypts it to get the refresh token, requests a new access token and finally returns the new access token in the response.

Admittedly, this does violate the "JS-Only" constraint you were looking for. However, a) again you really should NOT have a refresh token in javascript and b) it requires pretty minimal server-side logic at login/logout and no persistent server-side storage.

Note on CSRF: As noted in the comments, this solution doesn't address Cross-site Request Forgery; see the OWASP CSRF Prevention Cheat Sheet for further ideas on addressing these forms of attacks.

Another alternative is simply to not request the refresh token at all (not sure if that's an option with the OAuth 2 implementation you're dealing with; the refresh token is optional per the spec) and continually re-authenticate when it expires.

Hope that helps!
