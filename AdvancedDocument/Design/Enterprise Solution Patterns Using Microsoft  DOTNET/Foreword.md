This is a collection of patterns that will help you use Microsoft .NET, which contains many objects that
 follow patterns we've found useful. These objects are brought to life by the common language runtime which 
makes for strongly-patterned objects. An easy way to think about this is that the runtime takes care of so 
many aspects of an object that just the pattern parts are left. Patterns were important before the common 
language runtime, but now they are even more important.


You will find here a collection of patterns that you will see in most every transaction-processing Web 
application. These sorts of applications are really important to enterprise developers who are important to 
this book's authors. This is an important focus in the here and now. Of all the pattern books that could have
 been written about .NET, this is the most likely to be important to you today. Thank you, authors.


I could go on about Web applications but I wanted to point out an even more interesting thing about this collection. 
Whenever we pull patterns together our choices say something important about how we work. Our philosophy of work 
runs through our selections. For example, in the Design Patterns book, [Gamma, et. al, Addison-Wesley], the philosophy
 was to make programs flexible. This is important, of course, and some of those patterns are included here.
 But there are two other philosophies present in this volume worth mentioning. 

One philosophy is that in a continuously evolving environment like the enterprise, every complexity has a cost.
 You'll find a variety of patterns here that at first seem contradictory. That's because the authors know that
 successful enterprise applications start simple and grow over time. Something simple works for a while then it
 needs to be replaced. You'll find patterns here for both the simple and its replacement. This isn't the same as
 doing it wrong and then making it right. Both patterns are right, just not at the same time on a given project.
 

Another philosophy that runs through these patterns is that different people in the enterprise use different
 patterns for different purposes. Some patterns are more about the user experience than anything else. We can
 say that these patterns, and the people that apply them, are working in service of the user. The more these
 folks understand their users, the better they will be able to apply these patterns and the better their programs
 will be for their effort. Contrast this to classic concerns of the enterprise: efficiency, security, reliability,
 and so on. This collection includes patterns about these problems, too. When you apply them you will be working
 in service of the enterprise. It is also likely that you personally won't apply all the patterns in this book.
 That doesn't mean that you can't read them and understand more about how at least some of your colleagues think.

Many of the patterns are backed up by specific objects already available in .NET. For these, you will find 
implementations that tell you how to use these objects rather than telling you how to make these objects from
 scratch. Traditionally, implementation examples have been included as just one section of a pattern. These 
are just examples meant to be understood and emulated. The implementation "patterns" included in this volume 
are much more. They describe the practical experience the authors have had with using specific capabilities 
of .NET and, as such, amount to their best advice on how to proceed. 

When you find a pattern that you need and follow it to the implementation in .NET, you are using this volume 
as an index into the .NET libraries. The authors have organized all the patterns on a grid that categorizes 
the patterns according to levels of abstraction and viewpoints. Use this grid to find patterns that should be
 familiar. From there, you can find .NET capabilities that apply to the work you already do. You can also look
 around at patterns in neighboring parts of the grid. If these are familiar, move a little further. Soon you'll
 find the unfamiliar and can start benefiting from the experience of others. This works even if you know more 
about .NET than you do about patterns. Find the patterns that talk about sections of .NET that you use, find 
them on the grid, and then look around. 

This work is very much about helping you use the technology built into .NET. There is a temptation to enumerate 
the features of .NET in a work like this. The authors have worked hard to avoid this. When they did slip into a
 little bit of proud boasting, the reviewers, myself included, insisted that the patterns be rewritten to be 
the simplest advice you can use. 

I'll close by mentioning two more ways this work is important. The pattern community has invested a decade finding,
 writing, and reviewing patterns in what would have to be called an academic tradition of impartiality. This work 
is different. It is clearly in the sponsor's interest to have .NET well understood and this volume has that goal. 
However, that the sponsor would invest effort writing patterns is their acknowledgment that the decade of work has
 merit. The pattern community should be proud and should respond by reading, reviewing, debating, and enlarging this work. 

Finally, enterprise developers and administrators should study these and other patterns not just because they offer
 advice that can be applied immediately, but because they provide a vocabulary to talk about intellectual property 
independent of that property. Consider this work a first step in a new conversation with a company that wants to 
succeed by serving you. Your participation in a public dialog represents a sweet-spot for interacting with a vendor
 that lies somewhere between focus groups and the traditional code release cycle. It is a new way for a big corporation to listen.

Ward Cunningham of Cunningham & Cunningham, Inc.

January, 2003