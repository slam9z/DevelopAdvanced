[DuckInterface](http://martinfowler.com/bliki/DuckInterface.html)





# [DuckInterface](DuckInterface.html)



21 December 2005



[API design](/tags/API design.html)&nbsp;· [ruby](/tags/ruby.html)

tags:



Perhaps I was being naive but I never expected quite the chatter
	that my post on [HumaneInterface](HumaneInterface.html) opened up. Sadly most of
	it ended up being arguments about the relative merits of Ruby's
	Array and Java's List rather than the underlying points I was trying
	to make, but despite that I think some nice conversational
	tributaries appeared.

(Although I feel I ought to point out that it wasn't my intention
	to say that I thought that Ruby's Array was better or indeed that
	Ruby is better - I don't think either is better than the other
	unless you give more context. As it turns out I like both Ruby's Array and
	Java's List, although they are designed quite differently. Like any
	software they are flawed, since nobody writes classes that are
	exactly the way I want them this minute, but I wouldn't like to pit
	any of my code against them. The point is they are both useful and I
	use them both a lot - which is why they came to mind as an example.)

One of these conversational threads brought out that there are
other reasons for the differences between Array and List than the
humane/minimal philosophies. One of these reasons has to do with the
way similar functionality plays different roles in the two
languages.

Ruby's Array has a number of methods that made a few people
puzzled when the looked at the list. Push and pop - as [Elliotte](http://www.cafeaulait.org/oldnews/news2005December8.html)

said _"Someone pushed a stack into the list"_. There's also shift and
unshift which are like a list. This doesn't look right - Elliotte
again _"if you're using a queue or a stack, shouldn't you use a Queue or Stack class rather a List class?"_

Reading this triggered a thought in one of the recesses of my
	memory. I dug out [Smalltalk Best Practice Patterns](https://www.amazon.com/gp/product/013476904X?ie=UTF8&amp;tag=martinfowlerc-20&amp;linkCode=as2&amp;camp=1789&amp;creative=9325&amp;creativeASIN=013476904X)![](https://www.assoc-amazon.com/e/ir?t=martinfowlerc-20&amp;l=as2&amp;o=1&amp;a=0321601912), a fantastic book
	that anyone who seriously wants to understand object-orientation
	should read (despite the funny syntax).

> "One of the first objects many people write when
> 	they come to Smalltalk is Stack. Stack is the basic data structure,
> 	fabled in song, story, and hundreds of papers about theoretical
> 	programming languages... there is no Stack class in any of the basic
> 	images. I've seen one written any number of times, but they never
> 	seem to last long."
> 
> -- Kent Beck

The smalltalker approach, at least then, was to use
	`OrderedCollection`, Smalltalk's equivalent of Ruby's Array. There
	wasn't even a push or pop - instead Kent showed using `addLast:` and `removeLast:`.

Kent didn't give an explanation for the lack or a stack (and
	queue) - _"Why is there no Stack in Smalltalk? Well, 'just because'. It
		is part of the culture to simulate stacks using OrderedCollection."_

I'm not sure how I feel about this (and I get the distinct
	impression of uncertainty in Kent's writing). If you are going to use
something like a queue it does make sense to say `Stack
new` rather than `OrderedCollection new`, let alone
use `push` and `pop` rather than `addLast` and `removeLast`.

It strikes me that part of this situation may be to do with the
difference between static and dynamic languages. Static languages like
to talk to objects through strict type interfaces, dynamic languages
have classes that can fit multiple roles -  [Duck Typing](http://en.wikipedia.org/wiki/Duck_typing). Java
also has a list that does double duty as a queue: [LinkedList](http://java.sun.com/j2se/1.5.0/docs/api/java/util/LinkedList.html),

but you'd typically use it through distinct interfaces not realizing
the common implementation. The Smalltalk feeling is that we can get
what we want with our OrderedCollection, so why build another class?
Ruby seems to be echoing that reaction and adding the meaningful
method names for people who use it in that context. (Although to be
fair I have no idea whether the rubyists actually are happy with Array
being a stack, or whether it's a piece of regretful legacy.)

Another factor is what the language encourages for implementing
these structures. As [Charles

Miller](http://fishbowl.pastiche.org/2005/12/09/humane_interfaces) said "Java's design _affords_ small interfaces, and
utility functions provided as static methods on helper classes. Ruby's
design affords larger classes with mixed-in utility methods."

Perhaps one of the conclusions from this is that we should be
	wary of judging the features of a class in one language using the
	values of another language. Is Array the equivalent of List or of
	List plus various interfaces and implementations in the collections
	package - or is it something even more complicated? Some people
	might recoil from the thought of doing 78 things to a list, but I
	suspect lispers would think of many more to add. Ruby's Array has
	it's warts, but I must admit I like working with it more than the
	Java collections, although how much this is due to the humane
	interface guidelines and how much due to Ruby syntactic support I'm
	not sure.

All in all I'm not sure who I agree with here, or whether it
really matters that much. Like with many of these arguments I think
the most interesting thing is to try to understand both points of
view.












## Find similar articles at these tags

[API design](/tags/API design.html) [ruby](/tags/ruby.html)




