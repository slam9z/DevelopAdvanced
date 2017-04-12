[Enterprise Solution Patterns Using Microsoft .NET](https://msdn.microsoft.com/en-us/library/ff647095.aspx)

Authors: David Trowbridge, Microsoft Prescriptive Architecture Guidance; Dave Mancini and Dave Quick, 
Microsoft Core Infrastructure Solutions; Gregor Hohpe and James Newkirk, ThoughtWorks, Inc.; 
David Lavigne, SBI and Company

Microsoft Corporation

June 2003

Summary: This document provides a brief overview of Enterprise Solution Patterns Using Microsoft .NET, which embraces
 existing work in the patterns community, contributes new patterns, and shows how to implement these patterns in .NET.
 Included in the guide are an introduction to patterns and a catalog of 32 architecture, design, and implementation patterns.


## Introduction 

"Enterprise developers and administrators should study these and other patterns not just because they offer advice
 that can be applied immediately, but because they provide a vocabulary to talk about intellectual property independent
 of that property." - From the Foreword by Ward Cunningham


Enterprise Solution Patterns Using Microsoft .NET introduces patterns and then presents them in a repository,
 or catalog, organized to help you locate the right combination of patterns that solve your problem. The Pattern 
Frame, shown in Figure 1, is used throughout this documentation to delineate the problem space and show the relationships
 between patterns.

[Figure 1: The Pattern Frame]

The rows of the Pattern Frame represent progressive levels of abstraction: architecture, design, and implementation. 
The columns represent viewpoints, or lenses into the solution, which include database, application, deployment, and 
infrastructure perspectives. The chapters of the guide group the patterns into patterns clusters by subject area.

## Who Should Read This Guide 

This guide is written for readers in one or more of the following categories:

* Architects, designers, and developers who are new to patterns
* Architects and designers who are already experienced in using patterns to build enterprise solutions
* System architects and system engineers who architect or design systems infrastructure

## Contents 

### Foreword

Ward Cunningham discusses the merits of patterns in general and how this catalog extends the work of 
the patterns community.

### Chapter 1: Patterns for Building Enterprise Solutions

This chapter introduces the notion of a pattern, explains how a pattern documents simple, proven[已证实的] mechanisms,
 and shows how collections of patterns provide a common language for developers and architects. To illustrate 
these concepts, this chapter applies abbreviated versions of actual patterns to real-life development situations. 

### Chapter 2: Organizing Patterns

In recent years, patterns have emerged at different levels of abstraction and across a variety of domains. Chapter 2
 explores pattern levels in detail and outlines an organizing frame that helps you find relevant patterns quickly. 
The chapter then demonstrates how patterns provide a vocabulary to efficiently describe complex solutions without 
sacrificing detail. 

### Chapter 3: Web Presentation Patterns

The Web Presentation patterns cluster describes design and implementation patterns related to constructing dynamic Web
 applications. Depending on the size and the complexity of the application, different design tradeoffs have to be made.
 The Web Presentation cluster offers a number of pattern alternatives that illustrate the varied types of applications 
and their resulting tradeoffs.


### Chapter 4: Deployment Patterns

The patterns in the Deployment cluster help reduce the tension between application development and system infrastructure 
teams by offering guidance on how to optimally structure your applications and technical infrastructure to efficiently 
fulfill your solution requirements. The patterns discuss such topics as organizing your application into logical layers,
 refining layers to provide and consume services, organizing hardware into physical tiers, and allocating processes to 
processors with a deployment plan.


## #Chapter 5: Distributed Systems Patterns

This patterns cluster introduces concepts relevant to both the Distributed Systems and Service patterns clusters, 
including the distinction between interface-based and service-based collaboration and the concept of near versus far
 links. Distributed Systems patterns, as defined here, focus on instance-based collaboration and near links. 


### Chapter 6: Services Patterns

The Services patterns cluster briefly revisits collaboration concepts introduced in the previous chapter before presenting
 patterns that focus on collaboration between applications and external services. In contrast with Distributed Systems,
 Services patterns are primarily concerned with systems connected by far links using service-based collaboration. 


### Chapter 7: Performance and Reliability Patterns

Enterprise solutions must reliably meet the needs of an unpredictable number of users, and often must operate 24 hours a day
, seven days a week. Although there are many ways to increase performance and improve reliability, this patterns cluste
r focuses how to combine multiple systems that serve any number of applications or users for greater scalability and improved
 availability. 

### [Appendix A: Pattlets](https://msdn.microsoft.com/en-us/library/ff649898.aspx)

Pattlets are actual patterns to which this guide refers, but which it does not discuss in detail. The appendix lists all of the
 pattlets defined in this pattern catalog. The concept of pattlets is discussed in Chapter 2, "Organizing Patterns."

### [Bibliography](https://msdn.microsoft.com/en-us/library/ff649146.aspx)
