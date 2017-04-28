[Model Binding](https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding)


## Introduction to model binding

Model binding in ASP.NET Core MVC maps data from HTTP requests to action method parameters. The parameters may be simple types such as strings, integers, or floats, or they may be complex types. This is a great feature of MVC because mapping incoming data to a counterpart is an often repeated scenario, regardless of size or complexity of the data. MVC solves this problem by abstracting binding away so developers don't have to keep rewriting a slightly different version of that same code in every app. Writing your own text to type converter code is tedious, and error prone.


## How model binding works

Below is a list of the data sources in the order that model binding looks through them:


* Form values: These are form values that go in the HTTP request using the POST method. (including jQuery POST requests).

* Route values: The set of route values provided by Routing

* Query strings: The query string part of the URI.






