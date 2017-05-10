[Where to Store Your JWTs](https://stormpath.com/blog/where-to-store-your-jwts-cookies-vs-html5-web-storage)


So now that you have a good understanding of what a JWT is, the next step is to figure out how to store these tokens. If you are building a web application, you have a couple of options:

    HTML5 Web Storage (localStorage or sessionStorage)
    Cookies

To compare these two, let’s say we have a fictitious AngularJS or single page app (SPA) called galaxies.com with a login route (/token) to authenticate users to return a JWT. To access the other APIs endpoints that serve your SPA, the client needs to pass a valid JWT.

The request that the single page app makes would resemble:

HTTP/1.1

POST /token
Host: galaxies.com
Content-Type: application/x-www-form-urlencoded

username=tom@galaxies.com&password=andromedaisheadingstraightforusomg&grant_type=password
1
2
3
4
5
6
7
8
	
HTTP/1.1
 
POST /token
Host: galaxies.com
Content-Type: application/x-www-form-urlencoded
 
username=tom@galaxies.com&password=andromedaisheadingstraightforusomg&grant_type=password

Your server’s response will vary based on whether you are using cookies or Web Storage. For comparison, let’s take a look at how you would do both.
JWT localStorage or sessionStorage (Web Storage)

Exchanging a username and password for a JWT to store it in browser storage (sessionStorage or localStorage) is rather simple. The response body would contain the JWT as an access token:

HTTP/1.1 200 OK

  {
  "access_token": "eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB",
       "expires_in":3600
   }
1
2
3
4
5
6
7
	
HTTP/1.1 200 OK
 
  {
  "access_token": "eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB",
       "expires_in":3600
   }

On the client side, you would store the token in HTML5 Web Storage (assuming that we have a success callback):

function tokenSuccess(err, response) {
    if(err){
        throw err;
    }
    $window.sessionStorage.accessToken = response.body.access_token;
}
1
2
3
4
5
6
7
	
function tokenSuccess(err, response) {
    if(err){
        throw err;
    }
    $window.sessionStorage.accessToken = response.body.access_token;
}

To pass the access token back to your protected APIs, you would use the HTTP Authorization Header and the Bearer scheme. The request that your SPA would make would resemble:

HTTP/1.1

GET /stars/pollux
Host: galaxies.com
Authorization: Bearer eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB
1
2
3
4
5
6
	
HTTP/1.1
 
GET /stars/pollux
Host: galaxies.com
Authorization: Bearer eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB

JWT Cookie Storage

Exchanging a username and password for a JWT to store it in a cookie is simple as well. The response would use the Set-Cookie HTTP header:

HTTP/1.1 200 OK

Set-Cookie: access_token=eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB; Secure; HttpOnly;
1
2
3
4
	
HTTP/1.1 200 OK
 
Set-Cookie: access_token=eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB; Secure; HttpOnly;

To pass the access token back to your protected APIs on the same domain, the browser would automatically include the cookie value. The request to your protected API would resemble:

GET /stars/pollux
Host: galaxies.com

Cookie: access_token=eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB;
1
2
3
4
5
	
GET /stars/pollux
Host: galaxies.com
 
Cookie: access_token=eyJhbGciOiJIUzI1NiIsI.eyJpc3MiOiJodHRwczotcGxlL.mFrs3Zo8eaSNcxiNfvRh9dqKP4F1cB;

So, What’s the difference?

If you compare these approaches, both receive a JWT down to the browser. Both are stateless because all the information your API needs is in the JWT. Both are simple to pass back up to your protected APIs. The difference is in the medium.
JWT sessionStorage and localStorage Security

Web Storage (localStorage/sessionStorage) is accessible through JavaScript on the same domain. This means that any JavaScript running on your site will have access to web storage, and because of this can be vulnerable to cross-site scripting (XSS) attacks. XSS, in a nutshell, is a type of vulnerability where an attacker can inject JavaScript that will run on your page. Basic XSS attacks attempt to inject JavaScript through form inputs, where the attacker puts <script>alert('You are Hacked');</script> into a form to see if it is run by the browser and can be viewed by other users.

To prevent XSS, the common response is to escape and encode all untrusted data. But this is far from the full story. In 2015, modern web apps use JavaScript hosted on CDNs or outside infrastructure. Modern web apps include 3rd party JavaScript libraries for A/B testing, funnel/market analysis, and ads. We use package managers like Bower to import other peoples’ code into our apps.

What if only one of the scripts you use is compromised? Malicious JavaScript can be embedded on the page, and Web Storage is compromised. These types of XSS attacks can get everyone’s Web Storage that visits your site, without their knowledge. This is probably why a bunch of organizations advise not to store anything of value or trust any information in web storage. This includes session identifiers and tokens.

As a storage mechanism, Web Storage does not enforce any secure standards during transfer. Whoever reads Web Storage and uses it must do their due diligence to ensure they always send the JWT over HTTPS and never HTTP.
JWT Cookie Storage Security

Cookies, when used with the HttpOnly cookie flag, are not accessible through JavaScript, and are immune to XSS. You can also set the Secure cookie flag to guarantee the cookie is only sent over HTTPS. This is one of the main reasons that cookies have been leveraged in the past to store tokens or session data. Modern developers are hesitant to use cookies because they traditionally required state to be stored on the server, thus breaking RESTful best practices. Cookies as a storage mechanism do not require state to be stored on the server if you are storing a JWT in the cookie. This is because the JWT encapsulates everything the server needs to serve the request.

However, cookies are vulnerable to a different type of attack: cross-site request forgery (CSRF). A CSRF attack is a type of attack that occurs when a malicious web site, email, or blog causes a user’s web browser to perform an unwanted action on a trusted site on which the user is currently authenticated. This is an exploit of how the browser handles cookies. A cookie can only be sent to the domains in which it is allowed. By default, this is the domain that originally set the cookie. The cookie will be sent for a request regardless of whether you are on galaxies.com or hahagonnahackyou.com.

CSRF works by attempting to lure you to hahagonnahackyou.com. That site will have either an img tag or JavaScript to emulate a form post to galaxies.com and attempt to hijack your session, if it is still valid, and modify your account.

For example:

<body>

  <!– CSRF with an img tag –>

  <img href="http://galaxies.com/stars/pollux?transferTo=tom@stealingstars.com" />

  <!– or with a hidden form post –>

  <script type="text/javascript">
  $(document).ready(function() {
    window.document.forms[0].submit();
  });
  </script>

  <div style="display:none;">
    <form action="http://galaxies.com/stars/pollux" method="POST">
      <input name="transferTo" value="tom@stealingstars.com" />
    <form>
  </div>
</body>
1
2
3
4
5
6
7
8
9
10
11
12
13
14
15
16
17
18
19
20
21
	
<body>
 
  <!– CSRF with an img tag –>
 
  <img href="http://galaxies.com/stars/pollux?transferTo=tom@stealingstars.com" />
 
  <!– or with a hidden form post –>
 
  <script type="text/javascript">
  $(document).ready(function() {
    window.document.forms[0].submit();
  });
  </script>
 
  <div style="display:none;">
    <form action="http://galaxies.com/stars/pollux" method="POST">
      <input name="transferTo" value="tom@stealingstars.com" />
    <form>
  </div>
</body>

Both would send the cookie for galaxies.com and could potentially cause an unauthorized state change. CSRF can be prevented by using synchronized token patterns. This sounds complicated, but all modern web frameworks have support for this.

For example, AngularJS has a solution to validate that the cookie is accessible by only your domain. Straight from AngularJS docs:

    When performing XHR requests, the $http service reads a token from a cookie (by default, XSRF-TOKEN) and sets it as an HTTP header (X-XSRF-TOKEN). Since only JavaScript that runs on your domain can read the cookie, your server can be assured that the XHR came from JavaScript running on your domain.

You can make this CSRF protection stateless by including a xsrfToken JWT claim:

{
  "iss": "http://galaxies.com",
  "exp": 1300819380,
  "scopes": ["explorer", "solar-harvester", "seller"],
  "sub": "tom@andromeda.com",
  "xsrfToken": "d9b9714c-7ac0-42e0-8696-2dae95dbc33e"
}
1
2
3
4
5
6
7
8
	
{
  "iss": "http://galaxies.com",
  "exp": 1300819380,
  "scopes": ["explorer", "solar-harvester", "seller"],
  "sub": "tom@andromeda.com",
  "xsrfToken": "d9b9714c-7ac0-42e0-8696-2dae95dbc33e"
}

If you are using the Stormpath SDK for AngularJS, you get stateless CSRF protection with no development effort.

Leveraging your web app framework’s CSRF protection makes cookies rock solid for storing a JWT. CSRF can also be partially prevented by checking the HTTP Referer and Origin header from your API. CSRF attacks will have Referer and Origin headers that are unrelated to your application.

Even though they are more secure to store your JWT, cookies can cause some developer headaches, depending on if your applications require cross-domain access to work. Just be aware that cookies have additional properties (Domain/Path) that can be modified to allow you to specify where the cookie is allowed to be sent. Using AJAX, your server side can also notify browsers whether credentials (including Cookies) should be sent with requests with CORS.
Conclusion

JWTs are an awesome authentication mechanism. They give you a structured way to declare users and what they can access. They can be encrypted and signed for to prevent tampering on the client side, but the devil is in the details and where you store them. Stormpath recommends that you store your JWT in cookies for web applications, because of the additional security they provide, and the simplicity of protecting against CSRF with modern web frameworks. HTML5 Web Storage is vulnerable to XSS, has a larger attack surface area, and can impact all application users on a successful attack.

Questions or comments? We would love to hear them! Let me know if you have any questions in the discussion below or at tom@stormpath.com / @omgitstom.