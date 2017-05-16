[Token Based Authentication using ASP.NET Web API 2, Owin, and Identity](http://bitoftech.net/2014/06/01/token-based-authentication-asp-net-web-api-2-owin-asp-net-identity/)

Token Based Authentication
As I stated before we’ll use token based approach to implement authentication between the front-end application and the back-end API, as we all know the common and old way to implement authentication is the cookie-based approach were the cookie is sent with each request from the client to the server, and on the server it is used to identify the authenticated user.
With the evolution of front-end frameworks and the huge change on how we build web applications nowadays the preferred approach to authenticate users is to use signed token as this token sent to the server with each request, some of the benefits for using this approach are:
Scalability of Servers: The token sent to the server is self contained which holds all the user information needed for authentication, so adding more servers to your web farm is an easy task, there is no dependent on shared session stores.
Loosely Coupling: Your front-end application is not coupled with specific authentication mechanism, the token is generated from the server and your API is built in a way to understand this token and do the authentication.
Mobile Friendly: Cookies and browsers like each other, but storing cookies on native platforms (Android, iOS, Windows Phone) is not a trivial task, having standard way to authenticate users will simplify our life if we decided to consume the back-end API from native applications.
What we’ll build in this tutorial?
The front-end SPA will be built using HTML5, AngularJS, and Twitter Bootstrap. The back-end server will be built using ASP.NET Web API 2 on top of Owin middleware not directly on top of ASP.NET; the reason for doing so that we’ll configure the server to issue OAuth bearer token authentication using Owin middleware too, so setting up everything on the same pipeline is better approach. In addition to this we’ll use ASP.NET Identity system which is built on top of Owin middleware and we’ll use it to register new users and validate their credentials before generating the tokens.
As I mentioned before our back-end API should accept request coming from any origin, not only our front-end, so we’ll be enabling CORS (Cross Origin Resource Sharing) in Web API as well for the OAuth bearer token provider.
Use cases which will be covered in this application:
Allow users to signup (register) by providing username and password then store credentials in secure medium.
Prevent anonymous users from viewing secured data or secured pages (views).
Once the user is logged in successfully, the system should not ask for credentials or re-authentication for the next 24 hours 30 minutes because we are using refresh tokens.
So in this post we’ll cover step by step how to build the back-end API, and on the next post we’ll cover how we’ll build and integrate the SPA with the API.
Enough theories let’s get our hands dirty and start implementing the API!
Building the Back-End API
Step 1: Creating the Web API Project
In this tutorial I’m using Visual Studio 2013 and .Net framework 4.5, you can follow along using Visual Studio 2012 but you need to install Web Tools 2013.1 for VS 2012 by visiting this link.
Now create an empty solution and name it “AngularJSAuthentication” then add new ASP.NET Web application named “AngularJSAuthentication.API”, the selected template for project will be as the image below. Notice that the authentication is set to “No Authentication” taking into consideration that we’ll add this manually.

Step 2: Installing the needed NuGet Packages:
Now we need to install the NuGet packages which are needed to setup our Owin server and configure ASP.NET Web API to be hosted within an Owin server, so open NuGet Package Manger Console and type the below:





1
2
Install-Package Microsoft.AspNet.WebApi.Owin -Version 5.1.2
Install-Package Microsoft.Owin.Host.SystemWeb -Version 2.1.0
The  package “Microsoft.Owin.Host.SystemWeb” is used to enable our Owin server to run our API on IIS using ASP.NET request pipeline as eventually we’ll host this API on Microsoft Azure Websites which uses IIS.
Step 3: Add Owin “Startup” Class
Right click on your project then add new class named “Startup”. We’ll visit this class many times and modify it, for now it will contain the code below:





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
22
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
 
[assembly: OwinStartup(typeof(AngularJSAuthentication.API.Startup))]
namespace AngularJSAuthentication.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
        }
 
    }
}
What we’ve implemented above is simple, this class will be fired once our server starts, notice the “assembly” attribute which states which class to fire on start-up. The “Configuration” method accepts parameter of type “IAppBuilder” this parameter will be supplied by the host at run-time. This “app” parameter is an interface which will be used to compose the application for our Owin server.
The “HttpConfiguration” object is used to configure API routes, so we’ll pass this object to method “Register” in “WebApiConfig” class.
Lastly, we’ll pass the “config” object to the extension method “UseWebApi” which will be responsible to wire up ASP.NET Web API to our Owin server pipeline.
Usually the class “WebApiConfig” exists with the templates we’ve selected, if it doesn’t exist then add it under the folder “App_Start”. Below is the code inside it:





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
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
 
            // Web API routes
            config.MapHttpAttributeRoutes();
 
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
 
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }

Step 4: Delete Global.asax Class
No need to use this class and fire up the Application_Start event after we’ve configured our “Startup” class so feel free to delete it.
Step 5: Add the ASP.NET Identity System
After we’ve configured the Web API, it is time to add the needed NuGet packages to add support for registering and validating user credentials, so open package manager console and add the below NuGet packages:





1
2
Install-Package Microsoft.AspNet.Identity.Owin -Version 2.0.1
Install-Package Microsoft.AspNet.Identity.EntityFramework -Version 2.0.1
The first package will add support for ASP.NET Identity Owin, and the second package will add support for using ASP.NET Identity with Entity Framework so we can save users to SQL Server database.
Now we need to add Database context class which will be responsible to communicate with our database, so add new class and name it “AuthContext” then paste the code snippet below:





1
2
3
4
5
6
7
8
public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {
 
        }
    }
As you can see this class inherits from “IdentityDbContext” class, you can think about this class as special version of the traditional “DbContext” Class, it will provide all of the Entity Framework code-first mapping and DbSet properties needed to manage the identity tables in SQL Server. You can read more about this class on Scott Allen Blog.
Now we want to add “UserModel” which contains the properties needed to be sent once we register a user, this model is POCO class with some data annotations attributes used for the sake of validating the registration payload request. So under “Models” folder add new class named “UserModel” and paste the code below:





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
public class UserModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
 
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
 
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
Now we need to add new connection string named “AuthContext” in our Web.Config class, so open you web.config and add the below section:






1
2
3
<connectionStrings>
    <add name="AuthContext" connectionString="Data Source=.\sqlexpress;Initial Catalog=AngularJSAuth;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
  </connectionStrings>

Step 6: Add Repository class to support ASP.NET Identity System
Now we want to implement two methods needed in our application which they are: “RegisterUser” and “FindUser”, so add new class named “AuthRepository” and paste the code snippet below:





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
22
23
24
25
26
27
28
29
30
31
32
33
34
35
36
37
38
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;
 
        private UserManager<IdentityUser> _userManager;
 
        public AuthRepository()
        {
            _ctx = new AuthContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }
 
        public async Task<IdentityResult> RegisterUser(UserModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };
 
            var result = await _userManager.CreateAsync(user, userModel.Password);
 
            return result;
        }
 
        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);
 
            return user;
        }
 
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();
 
        }
    }
What we’ve implemented above is the following: we are depending on the “UserManager” that provides the domain logic for working with user information. The “UserManager” knows when to hash a password, how and when to validate a user, and how to manage claims. You can read more about ASP.NET Identity System.
Step 7: Add our “Account” Controller
Now it is the time to add our first Web API controller which will be used to register new users, so under file “Controllers” add Empty Web API 2 Controller named “AccountController” and paste the code below:





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
22
23
24
25
26
27
28
29
30
31
32
33
34
35
36
37
38
39
40
41
42
43
44
45
46
47
48
49
50
51
52
53
54
55
56
57
58
59
60
61
62
63
64
65
66
67
68
69
70
71
[RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private AuthRepository _repo = null;
 
        public AccountController()
        {
            _repo = new AuthRepository();
        }
 
        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
 
            IdentityResult result = await _repo.RegisterUser(userModel);
 
            IHttpActionResult errorResult = GetErrorResult(result);
 
            if (errorResult != null)
            {
                return errorResult;
            }
 
            return Ok();
        }
 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }
 
            base.Dispose(disposing);
        }
 
        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
 
            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
 
                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }
 
                return BadRequest(ModelState);
            }
 
            return null;
        }
    }
By looking at the “Register” method you will notice that we’ve configured the endpoint for this method to be “/api/account/register” so any user wants to register into our system must issue HTTP POST request to this URI and the pay load for this request will contain the JSON object as below:





1
2
3
4
5
{
  "userName": "Taiseer",
  "password": "SuperPass",
  "confirmPassword": "SuperPass"
}
Now you can run your application and issue HTTP POST request to your local URI: “http://localhost:port/api/account/register” or you can try the published API using this end point: http://ngauthenticationapi.azurewebsites.net/api/account/register if all went fine you will receive HTTP status code 200 and the database specified in connection string will be created automatically and the user will be inserted into table “dbo.AspNetUsers”.
Note: It is very important to send this POST request over HTTPS so the sensitive information get encrypted between the client and the server.
The “GetErrorResult” method is just a helper method which is used to validate the “UserModel” and return the correct HTTP status code if the input data is invalid.
Step 8: Add Secured Orders Controller
Now we want to add another controller to serve our Orders, we’ll assume that this controller will return orders only for Authenticated users, to keep things simple we’ll return static data. So add new controller named “OrdersController” under “Controllers” folder and paste the code below:






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
22
23
24
25
26
27
28
29
30
31
32
33
34
35
36
37
[RoutePrefix("api/Orders")]
    public class OrdersController : ApiController
    {
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(Order.CreateOrders());
        }
 
    }
 
    #region Helpers
 
    public class Order
    {
        public int OrderID { get; set; }
        public string CustomerName { get; set; }
        public string ShipperCity { get; set; }
        public Boolean IsShipped { get; set; }
 
        public static List<Order> CreateOrders()
        {
            List<Order> OrderList = new List<Order> 
            {
                new Order {OrderID = 10248, CustomerName = "Taiseer Joudeh", ShipperCity = "Amman", IsShipped = true },
                new Order {OrderID = 10249, CustomerName = "Ahmad Hasan", ShipperCity = "Dubai", IsShipped = false},
                new Order {OrderID = 10250,CustomerName = "Tamer Yaser", ShipperCity = "Jeddah", IsShipped = false },
                new Order {OrderID = 10251,CustomerName = "Lina Majed", ShipperCity = "Abu Dhabi", IsShipped = false},
                new Order {OrderID = 10252,CustomerName = "Yasmeen Rami", ShipperCity = "Kuwait", IsShipped = true}
            };
 
            return OrderList;
        }
    }
 
    #endregion
Notice how we added the “Authorize” attribute on the method “Get” so if you tried to issue HTTP GET request to the end point “http://localhost:port/api/orders” you will receive HTTP status code 401 unauthorized because the request you send till this moment doesn’t contain valid authorization header. You can check this using this end point: http://ngauthenticationapi.azurewebsites.net/api/orders
Step 9: Add support for OAuth Bearer Tokens Generation
Till this moment we didn’t configure our API to use OAuth authentication workflow, to do so open package manager console and install the following NuGet package:





1
Install-Package Microsoft.Owin.Security.OAuth -Version 2.1.0
After you install this package open file “Startup” again and call the new method named “ConfigureOAuth” as the first line inside the method “Configuration”, the implemntation for this method as below:





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
22
23
24
public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
	    //Rest of code is here;
        }
 
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };
 
            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
 
        }
    }
Here we’ve created new instance from class “OAuthAuthorizationServerOptions” and set its option as the below:
The path for generating tokens will be as :”http://localhost:port/token”. We’ll see how we will issue HTTP POST request to generate token in the next steps.
We’ve specified the expiry for token to be 24 hours, so if the user tried to use the same token for authentication after 24 hours from the issue time, his request will be rejected and HTTP status code 401 is returned.
We’ve specified the implementation on how to validate the credentials for users asking for tokens in custom class named “SimpleAuthorizationServerProvider”.
Now we passed this options to the extension method “UseOAuthAuthorizationServer” so we’ll add the authentication middleware to the pipeline.
Step 10: Implement the “SimpleAuthorizationServerProvider” class
Add new folder named “Providers” then add new class named “SimpleAuthorizationServerProvider”, paste the code snippet below:





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
22
23
24
25
26
27
28
29
30
31
public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
 
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
 
            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);
 
                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }
 
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
 
            context.Validated(identity);
 
        }
    }
As you notice this class inherits from class “OAuthAuthorizationServerProvider”, we’ve overridden two methods “ValidateClientAuthentication” and “GrantResourceOwnerCredentials”. The first method is responsible for validating the “Client”, in our case we have only one client so we’ll always return that its validated successfully.
The second method “GrantResourceOwnerCredentials” is responsible to validate the username and password sent to the authorization server’s token endpoint, so we’ll use the “AuthRepository” class we created earlier and call the method “FindUser” to check if the username and password are valid.
If the credentials are valid we’ll create “ClaimsIdentity” class and pass the authentication type to it, in our case “bearer token”, then we’ll add two claims (“sub”,”role”) and those will be included in the signed token. You can add different claims here but the token size will increase for sure.
Now generating the token happens behind the scenes when we call “context.Validated(identity)”.
To allow CORS on the token middleware provider we need to add the header “Access-Control-Allow-Origin” to Owin context, if you forget this, generating the token will fail when you try to call it from your browser. Not that this allows CORS for token middleware provider not for ASP.NET Web API which we’ll add on the next step.
Step 11: Allow CORS for ASP.NET Web API
First of all we need to install the following NuGet package manger, so open package manager console and type:





1
Install-Package Microsoft.Owin.Cors -Version 2.1.0
Now open class “Startup” again and add the highlighted line of code (line 8) to the method “Configuration” as the below:





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
public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
 
            ConfigureOAuth(app);
 
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
 
        }

Step 12: Testing the Back-end API
Assuming that you registered the username “Taiseer” with password “SuperPass” in the step below, we’ll use the same username to generate token, so to test this out open your favorite REST client application in order to issue HTTP requests to generate token for user “Taiseer”. For me I’ll be using PostMan.
Now we’ll issue a POST request to the endpoint http://ngauthenticationapi.azurewebsites.net/token the request will be as the image below:

Notice that the content-type and payload type is “x-www-form-urlencoded” so the payload body will be on form (grant_type=password&username=”Taiseer”&password=”SuperPass”). If all is correct you’ll notice that we’ve received signed token on the response.
As well the “grant_type” Indicates the type of grant being presented in exchange for an access token, in our case it is password.
Now we want to use this token to request the secure data using the end point http://ngauthenticationapi.azurewebsites.net/api/orders so we’ll issue GET request to the end point and will pass the bearer token in the Authorization header, so for any secure end point we’ve to pass this bearer token along with each request to authenticate the user.
Note: that we are not transferring the username/password as the case of Basic authentication.
The GET request will be as the image below:

If all is correct we’ll receive HTTP status 200 along with the secured data in the response body, if you try to change any character with signed token you directly receive HTTP status code 401 unauthorized.
Now our back-end API is ready to be consumed from any front end application or native mobile app.
Update (2014-08-11) Thanks for Attila Hajdrik for forking my repo and updating it to use MongoDb instead of Entity Framework, you can check it here.
You can check the demo application, play with the back-end API for learning purposes (http://ngauthenticationapi.azurewebsites.net), and check the source code on Github.
Follow me on Twitter @tjoudeh
References
10 Things You Should Know about Tokens by Matias Woloski
SPA Authentication Example by David Antaramian