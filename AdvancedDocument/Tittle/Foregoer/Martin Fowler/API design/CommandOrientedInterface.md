[CommandOrientedInterface](http://martinfowler.com/bliki/CommandOrientedInterface.html)




# [CommandOrientedInterface](CommandOrientedInterface.html)



[![](/mf.jpg "Photo of Martin Fowler")](/)

[Martin Fowler](/)

23 November 2003



[API design](/tags/API design.html)

tags:



 The most common style of interface to a module is to use
procedures, or object methods. So if you want a module to calculate a
bunch of charges for a contract, you might have a BillingService class
with a method for doing the calculation, calling it like this

<pre>aBillingService.calculateCharges(aContract)</pre>

A command oriented interface would have a command class for each
operation, and be called with something like this

<pre>CalculateChargeCommand.new(aContract).run()</pre>

Essentially you have one command class for each method that you
would have in the method-oriented interface.

A common variation is to have a separate command executor object
that actually does the running of the command.

<pre>aCommandExecutor.run(CalculateChargeCommand.new(aContract))</pre>

If you've used frameworks like Struts, you'll recognize that the
action classes follow this style of operation.

So why do people like and dislike this approach? First off it's
fair to say that a command oriented interface is rather more involved
than a method oriented one. You have instantiate the command and pass
them through for execution. This is more involved than just calling a
method, which is why even fans of this approach only use them for
significant interfaces (service layers, server-side logic, interfaces
of major subsystems).

Command oriented interfaces have a number of benefits. One of the
primary ones is the ability to easily add common behavior to commands
by decorating the command executor. This is very handy for handling
transactions, logging, and the like. Commands can be queued for later
execution and (if the commands and their data are serializable) be
passed across a network. Command results can be cached by holding
results against a key synthesized from the command name and
arguments.

Where I've seen complaints about commands, the biggest issue is
due to a lot of duplicated logic in commands. This tends to happen
most when the commands are [Transaction Scripts](http://martinfowler.com/eaaCatalog/transactionScript.html) that contain a lot of logic. This isn't necessarily an
issue with using commands as opposed to methods; this problem is a
general issue with Transaction Scripts. It may that command-oriented
structure tends to exaggerate this, if only because many people feel
that a class needs a page or two of code to be worthwhile, and thus
end up putting more code in the command than should be there.

You'll notice I've used the word _interface_ for this page.
This reflects that the choice about using commands is primarily about
the interface to clients rather than about the actual implementation
of the command logic. It's perfectly reasonable to have command
classes whose run method is just a single line calling out to another
method. Doing this gives you all the advantages of commands, but
allows you to keep the logic out of the command classes themselves.
Such command classes have very few lines of code.

A common question with commands is what to return. Generic run
methods need a general return type, such as Object or CommandResult,
but you'll want a more specific type to get the results from running a
command. One route is to define a result class for each command class
and use a naming convention, so that CalculateChargeCommand would have
a return type of CalculateChargeResult. Another route is to make the
command store the results in the same object. In this case you would
first run the the command, and then interrogate the command object for
results.


<span class="label">Share:</span>[![](/t_mini-a.png)](https://twitter.com/intent/tweet?url=http://martinfowler.com/bliki/CommandOrientedInterface.html&amp;text=Bliki: CommandOrientedInterface ➙  "Share on Twitter")[![](/fb-icon-20.png)](https://facebook.com/sharer.php?u=http://martinfowler.com/bliki/CommandOrientedInterface.html "Share on Facebook")[![](/gplus-16.png)](https://plus.google.com/share?url=http://martinfowler.com/bliki/CommandOrientedInterface.html "Share on Google Plus")

if you found this article useful, please share it. I appreciate the feedback and encouragement







## Find similar articles at the tag

[API design](/tags/API design.html)




