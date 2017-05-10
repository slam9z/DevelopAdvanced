[Testing controllers](https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/testing)

Controllers in ASP.NET MVC apps should be small and focused on user-interface concerns. Large controllers that deal with non-UI concerns are more difficult to test and maintain.

## Why Test Controllers

Controllers are a central part of any ASP.NET Core MVC application. As such, you should have confidence they behave as intended for your app. Automated tests can provide you with this confidence and can detect errors before they reach production. It's important to avoid placing unnecessary responsibilities within your controllers and ensure your tests focus only on controller responsibilities.

Controller logic should be minimal and not be focused on business logic or infrastructure concerns (for example, data access). Test controller logic, not the framework. Test how the controller behaves based on valid or invalid inputs. Test controller responses based on the result of the business operation it performs.

Typical controller responsibilities:


* Verify ModelState.IsValid

* Return an error response if ModelState is invalid

* Retrieve a business entity from persistence

* Perform an action on the business entity

* Save the business entity to persistence

* Return an appropriate IActionResult
