[Refactoring Module Dependencies](http://martinfowler.com/articles/refactoring-dependencies.html)


<div class="frontLeft">

13 October 2015


## Contents

*   [The Starting Point(s)](#TheStartingPoints)
*   [Presentation-Domain-Data Layering](#Presentation-domain-dataLayering)
*   [Performing the split](#PerformingTheSplit)
*   [Linker Substitution](#LinkerSubstitution)
*   [Data source as parameter with each call](#DataSourceAsParameterWithEachCall)

    *   [Paramterizing the data source file name](#ParamterizingTheDataSourceFileName)
    *   [Trade offs to parameterizing](#TradeOffsToParameterizing)

*   [Singular Services](#SingularServices)
*   [Introducing Service Locator](#IntroducingServiceLocator)

    *   [Refactoring the JavaScript to use the locator](#RefactoringTheJavascriptToUseTheLocator)
    *   [Java](#Java)
    *   [Consequences of using a service locator](#ConsequencesOfUsingAServiceLocator)

*   [Split Phase](#SplitPhase)
*   [Dependency Injection](#DependencyInjection)

    *   [Java example](#JavaExample)
    *   [JavaScript example](#JavascriptExample)
    *   [Consequences](#Consequences)

*   [Final Thoughts](#FinalThoughts)

### Sidebars

*   [JavaScript Style](#JavascriptStyle)
*   [ES6 Modules](#Es6Modules)



 As a program grows in size it's important to split it into modules, so that you don't need to understand all of it to make a small modification. Often these modules can be supplied by different teams and combined dynamically. In this refactoring essay I split a small program using Presentation-Domain-Data layering. I then refactor the dependencies between these modules to introduce the Service Locator and Dependency Injection patterns. These apply in different languages, yet look different, so I show these refactorings in both Java and a classless JavaScript style.


As programs go larger than a few hundred lines of code, you need to think about how to split them up into modules. At the very least it's useful to have smaller files to better manage your editing. But more seriously you want to divide up your program so that you don't have to keep it all in your head in order to make changes.

A well designed modular structure should allow you to only understand a small part of a larger program when you need to make a small change to it. Sometimes a small change will cross-cut over the modules, but most of the time you'll just need to understand a single module and its neighbors.

The hardest part of splitting a program into modules is just deciding on what the module boundaries should be. There's no easy guidelines to follow for this, indeed a major theme of my life's work is to try and understand what good module boundaries will look like. Perhaps the most important part of drawing good module boundaries is paying attention to the changes you make and refactoring your code so that code that changes together is in the same or nearby modules.

On top of this is the mechanics of making the separation of how the various parts relate to each other. In the simplest case you have client modules that call suppliers. But often the configuration of these clients and suppliers can get tangled because you don't always want the client program to know too much about how its suppliers fit together.

I'm going to explore this problem with an example, where I'll take a hunk of code and see how it can be split into pieces. In fact I'm going to do this twice, using two different languages: Java and JavaScript, which despite their similar names are really very different when it comes to the affordances they have for modularity. 


## Presentation-Domain-Data Layering

I said earlier that setting module boundaries was a subtle and nuanced art, but one guideline that many people follow is Presentation-Domain-Data Layering - separating presentation code (UI), business logic, and data access. There are good reasons for following this kind of split. Each of those three categories involve thinking about different concerns, and often use different frameworks to assist in the task. Furthermore there is also a desire for substitution - multiple presentations using the same core business logic, or the business logic using different data sources in different environments.

So for this example I'm going to follow this common split, and I'll also stress the substitution justification. After all this gondorff number is such a valuable metric that many people will want to make use of it - encouraging me to package it as a unit that can easily be reused by multiple applications. Furthermore not all applications will keep their sales data in a csv file, some will use a database or a remote microservice. We want an application developer to be able to take the gondorff code and plug it into her specific data source, which she may write herself or get from yet another developer.

But before we embark on the refactoring to enable all this, I do need to stress that presentation-domain-data layering does have its limitations. The general rule of modularity is that we want to confine the consequences of change to one module if we can. But separate presentation-domain-data modules often do have to change together. The simple act of adding a data field will usually cause all three to update. As a result I favor using this approach in smaller scopes, but larger applications need high level modules to be developed along different lines. In particular you shouldn't use the presentation-domain-data layers as a basis for team boundaries.