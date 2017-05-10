[Integration testing](https://docs.microsoft.com/en-us/aspnet/core/testing/integration-testing)

Integration testing ensures that an application's components function correctly when assembled together. ASP.NET Core supports integration testing using unit test frameworks and a built-in test web host that can be used to handle requests without network overhead.



## Introduction to integration testing

Integration tests verify that different parts of an application work correctly together. Unlike Unit testing, integration tests frequently involve application infrastructure concerns, such as a database, file system, network resources, or web requests and responses. Unit tests use fakes or mock objects in place of these concerns, but the purpose of integration tests is to confirm that the system works as expected with these systems.

Integration tests, because they exercise larger segments of code and because they rely on infrastructure elements, tend to be orders of magnitude slower than unit tests. Thus, it's a good idea to limit how many integration tests you write, especially if you can test the same behavior with a unit test.
Note

> If some behavior can be tested using either a unit test or an integration test, prefer the unit test, since it will be almost always be faster. You might have dozens or hundreds of unit tests with many different inputs but just a handful of integration tests covering the most important scenarios.

Testing the logic within your own methods is usually the domain of unit tests. Testing how your application works within its framework, for example with ASP.NET Core, or with a database is where integration tests come into play. It doesn't take too many integration tests to confirm that you're able to write a row to the database and read it back. You don't need to test every possible permutation of your data access code - you only need to test enough to give you confidence that your application is working properly.

## Integration testing ASP.NET Core

To get set up to run integration tests, you'll need to create a test project, add a reference to your ASP.NET Core web project, and install a test runner. This process is described in the Unit testing documentation, along with more detailed instructions on running tests and recommendations for naming your tests and test classes.


### The Test Host

ASP.NET Core includes a test host that can be added to integration test projects and used to host ASP.NET Core applications, serving test requests without the need for a real web host. The provided sample includes an integration test project which has been configured to use xUnit and the Test Host. It uses the Microsoft.AspNetCore.TestHost NuGet package.

Once the Microsoft.AspNetCore.TestHost package is included in the project, you'll be able to create and configure a TestServer in your tests. The following test shows how to verify that a request made to the root of a site returns "Hello World!" and should run successfully against the default ASP.NET Core Empty Web template created by Visual Studio.