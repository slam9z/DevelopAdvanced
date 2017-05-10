[Authorization Policies and Data Protection with IdentityServer4 in ASP.NET Core](https://damienbod.com/2016/02/14/authorization-policies-and-data-protection-with-identityserver4-in-asp-net-core/)

This article shows how authorization policies can be used together with IdentityServer4. The policies are configured on the resource server and the ASP.NET Core IdentityServer4 configures the user claims to match these. The resource server is also setup to encrypt a ‘Description’ field in the SQLite database, so it cannot be read by opening the SQLite database directly.

The demo uses and extends the existing code from this previous article:
OAuth2 Implicit Flow with Angular and ASP.NET Core 1.0 IdentityServer4

Code: VS2017 msbuild | VS2015 project.json

History:
2017.03.18: Updated to angular 2.4.10, oidc client validation

Full history:
<https://github.com/damienbod/AspNet5IdentityServerAngularImplicitFlow#history>

Other posts in this series:

    OAuth2 Implicit Flow with Angular and ASP.NET Core 1.1 IdentityServer4
    Authorization Policies and Data Protection with IdentityServer4 in ASP.NET Core
    Angular OpenID Connect Implicit Flow with IdentityServer4
    Angular2 OpenID Connect Implicit Flow with IdentityServer4
    Secure file download using IdentityServer4, Angular2 and ASP.NET Core
    Angular2 Secure File Download without using an access token in URL or cookies
    Full Server logout with IdentityServer4 and OpenID Connect Implicit Flow
    IdentityServer4, WebAPI and Angular2 in a single ASP.NET Core project
    Extending Identity in IdentityServer4 to manage users in ASP.NET Core

Extending the Resource Server with Policies

An authorization policy can be added to the MVC application in the Startup class ConfigureServices method. The policy can be added globally using the filters or individually using attributes on a class or method. To add a global authorization policy, the AuthorizationPolicyBuilder helper method can be used to create the policy. The claimType parameter in the RequireClaim method must match a claim supplied in the token.

Then the policy can be added using the AuthorizeFilter in the AddMVC extension.

```cs	
var guestPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .RequireClaim("scope", "dataEventRecords")
    .Build();
 
services.AddMvc(options =>
{
   options.Filters.Add(new AuthorizeFilter(guestPolicy));
});
```

To create policies which can be used individually, the AddAuthorization extension method can be used. Again the claimType parameter in the RequireClaim method must match a claim supplied in the token.


```cs	
services.AddAuthorization(options =>
{
    options.AddPolicy("dataEventRecordsAdmin", policyAdmin =>
    {
        policyAdmin.RequireClaim("role", "dataEventRecords.admin");
    });
    options.AddPolicy("dataEventRecordsUser", policyUser =>
    {
        policyUser.RequireClaim("role",  "dataEventRecords.user");
    });
 
});
```

To use and authenticate the token from IdentityServer4, the UseJwtBearerAuthentication extension method can be used in the Configure method in the Startup class of the MVC application.

```cs
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap = new Dictionary<string, string>();
 
app.UseJwtBearerAuthentication(options =>
{
    options.Authority = "https://localhost:44345";
    options.Audience = "https://localhost:44345/resources";
    options.AutomaticAuthenticate = true;
    options.AutomaticChallenge = true;
});
```


The authorization policies can then be applied in the controller using authorization filters with the policy name as a parameter. Maybe it’s not a good idea to define each method with a different policy as this might be hard to maintain over time, maybe per controller might be more appropriate, all depends on your requirements. It pays off to define your security strategy before implementing it.

```cs	
[Authorize]
[Route("api/[controller]")]
public class DataEventRecordsController : Controller
{
    [Authorize("dataEventRecordsUser")]
    [HttpGet]
    public IEnumerable<DataEventRecord> Get()
    {
        return _dataEventRecordRepository.GetAll();
    }
 
    [Authorize("dataEventRecordsAdmin")]
    [HttpGet("{id}")]
    public DataEventRecord Get(long id)
    {
        return _dataEventRecordRepository.Get(id);
    }
```

Adding the User Claims and Client Scopes in IdentityServer4

The resource server has been setup to check for claim types of ‘role’ with the value of ‘dataEventRecords.user’ or ‘dataEventRecords.admin’. The application is also setup to check for claims type ‘scope’ with the value of ‘dataEventRecords’. IdentityServer4 needs to be configured for this. The IdentityResource and the ApiResource is configured for the client in the IdentityServer AspNetCore project.

```cs	
public static IEnumerable<IdentityResource> GetIdentityResources()
{
    return new List<IdentityResource>
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
        new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } ),
        new IdentityResource("securedfilesscope",new []{ "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user"} )
    };
}
 
public static IEnumerable<ApiResource> GetApiResources()
{
    return new List<ApiResource>
    {
        new ApiResource("dataEventRecords")
        {
            ApiSecrets =
            {
                new Secret("dataEventRecordsSecret".Sha256())
            },
            Scopes =
            {
                new Scope
                {
                    Name = "dataeventrecordsscope",
                    DisplayName = "Scope for the dataEventRecords ApiResource"
                }
            },
            UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
        },
        new ApiResource("securedFiles")
        {
            ApiSecrets =
            {
                new Secret("securedFilesSecret".Sha256())
            },
            Scopes =
            {
                new Scope
                {
                    Name = "securedfilesscope",
                    DisplayName = "Scope for the securedFiles ApiResource"
                }
            },
            UserClaims = { "role", "admin", "user", "securedFiles", "securedFiles.admin", "securedFiles.user" }
        }
    };
}
```

Then users are also configured and the appropriate role and scope claims for each user. (And some others which aren’t required for the angular client.) Two users are configured, damienboduser and damienbodadmin. The damienboduser has not the ‘dataEventRecords.admin’ role claim. This means that the user cannot create or update a user, only see the list. (Configured in the MVC Controller of the resource server)

```cs
new InMemoryUser{Subject = "48421157", Username = "damienbodadmin", Password = "damienbod",
  Claims = new Claim[]
  {
    new Claim(Constants.ClaimTypes.Name, "damienbodadmin"),
    new Claim(Constants.ClaimTypes.GivenName, "damienbodadmin"),
    new Claim(Constants.ClaimTypes.Email, "damien_bod@hotmail.com"),
    new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
    new Claim(Constants.ClaimTypes.Role, "admin"),
    new Claim(Constants.ClaimTypes.Role, "dataEventRecords.admin"),
    new Claim(Constants.ClaimTypes.Role, "dataEventRecords.user"),
    new Claim(Constants.ClaimTypes.Role, "dataEventRecords")
  }
},
new InMemoryUser{Subject = "48421158", Username = "damienboduser", Password = "damienbod",
  Claims = new Claim[]
  {
    new Claim(Constants.ClaimTypes.Name, "damienboduser"),
    new Claim(Constants.ClaimTypes.GivenName, "damienboduser"),
    new Claim(Constants.ClaimTypes.Email, "damien_bod@hotmail.com"),
    new Claim(Constants.ClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
    new Claim(Constants.ClaimTypes.Role, "user"),
    new Claim(Constants.ClaimTypes.Role, "dataEventRecords.user"),
    new Claim(Constants.ClaimTypes.Role, "dataEventRecords")
  }
}
```

The security for the client application is configured to allow the different scopes which can be used. If the client requests a scope which isn’t configured here, the client application request will be rejected.

```cs
new Client
{
    ClientName = "angularclient",
    ClientId = "angularclient",
    Flow = Flows.Implicit,
    RedirectUris = new List<string>
    {
        "https://localhost:44347/identitytestclient.html",
        "https://localhost:44347/authorized"
 
    },
    PostLogoutRedirectUris = new List<string>
    {
        "https://localhost:44347/identitytestclient.html",
        "https://localhost:44347/authorized"
    },
    AllowedScopes = new List<string>
    {
        "openid",
        "email",
        "profile",
        "dataEventRecords",
        "aReallyCoolScope",
        "role"
    }
},
```

If the user damienboduser sends a HTTP GET request for the single item, the resource server returns a 403 with no body. If the AutomaticChallenge option in the UseJwtBearerAuthentication is false, this will be returned as a 401.

dataprotectionAspNet5IdentityServerAngularImplicitFlow_02

Using Data Protection to encrypt the SQLite data

It is really easy to encrypt your data using the Data Protection library from ASP.NET Core. To use it in an MVC application, just add it in the ConfigureServices method using the DataProtection extension methods. There are a few different ways to configure this and it is well documented here.

This example uses the file system with a self signed cert as data protection configuration. This will work for long lived encryption and is easy to restore. The ProtectKeysWithCertificate method does not work in dnxcore at present, but hopefully this will be implemented in RC2. Thanks to Barry Dorrans ‏@blowdart for all his help.

Note: For this to work the cert needs to be added to the cert store.

```cs
public void ConfigureServices(IServiceCollection services)
{
 
var connection = Configuration["Production:SqliteConnectionString"];
var folderForKeyStore = Configuration["Production:KeyStoreFolderWhichIsBacked"];
 
var cert = new X509Certificate2(Path.Combine(_env.ContentRootPath, "damienbodserver.pfx"), "");
 
// Important The folderForKeyStore needs to be backed up.
services.AddDataProtection()
    .SetApplicationName("AspNet5IdentityServerAngularImplicitFlow")
    .PersistKeysToFileSystem(new DirectoryInfo(folderForKeyStore))
    .ProtectKeysWithCertificate(cert);
```

Now the IDataProtectionProvider interface can be injected in your class using constructor injection. You can then create a protector using the CreateProtector method. Care should be taken on how you define the string in this method. See the documentation for the recommendations.

```cs	
private readonly DataEventRecordContext _context;
private readonly ILogger _logger;
private IDataProtector _protector;
 
public DataEventRecordRepository(IDataProtectionProvider provider, 
                 DataEventRecordContext context, 
                 ILoggerFactory loggerFactory)
{
    _context = context;
    _logger = loggerFactory.CreateLogger("IDataEventRecordResporitory");
    _protector = provider.CreateProtector("DataEventRecordRepository.v1");
}

```

Now the protector IDataProtectionProvider can be used to Protect and also Unprotect the data. In this example all descriptions which are saved to the SQLite database are encrypted using the default data protection settings.


```cs
	
private void protectDescription(DataEventRecord dataEventRecord)
{
    var protectedData = _protector.Protect(dataEventRecord.Description);
    dataEventRecord.Description = protectedData;
}
 
private void unprotectDescription(DataEventRecord dataEventRecord)
{
    var unprotectedData = _protector.Unprotect(dataEventRecord.Description);
    dataEventRecord.Description = unprotectedData;
}
```

When the SQLite database is opened, you can see that all description fields are encrypted.

dataprotectionAspNet5IdentityServerAngularImplicitFlow_01png