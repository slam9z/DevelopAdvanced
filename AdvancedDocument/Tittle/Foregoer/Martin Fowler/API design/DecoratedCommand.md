[DecoratedCommand](http://martinfowler.com/bliki/DecoratedCommand.html)

# [DecoratedCommand](DecoratedCommand.html)





[API design](/tags/API design.html)

tags:



This is a very common pattern and also very simple, it's really
	just the decorator pattern applied to commands. I've seen it used a
	lot with [CommandOrientedInterface](CommandOrientedInterface.html)s. You also hear this
	referred to as interceptors and as a form of Aspect Oriented Programming.

You start with some command, usually of some form of basic
	functionality that may need some additional function added to it
	later. So this might be a domain oriented command such as
	PayInvoice. These commands will have some kind of execute
	method.

```cs
class PayInvoiceCommand : Command ...
void Execute() {
  // do interesting domain logic
}
```

Let's say we want to do this inside a transaction. We can
	decorate the command with a suitable transactional decorator.

```cs

class TransactionalDecorator : CommandDecorator ...
  void Execute() {
    Transaction t = TransactionManager.beginTransaction();
    try {
      Component.Execute();
      t.commit();
    } catch (Exception) {
      t.rollback();
    }
  }
    
```

We can also do a security check this way

```cs

class SecurityDecorator : CommandDecorator ...
  void Execute() {
    if (passesSecurityCheck())
      Component.Execute();
  }
```

With these classes in place we can then easily combine them to
	get the right kinds of behavior. 

```cs

  // Transaction Invoice Payment
  Command c = new TransactionalDecorator(new PayInvoiceCommand(invoice));
  c.Execute();
  //Transactional and secure payment
  Command c = new SecurityDecorator(
                  new TransactionalDecorator(
                      new PayInvoiceCommand(invoice)));
  c.Execute();
```

Indeed this ability to add behavior dynamically is one of the big
	benefits of a [CommandOrientedInterface](CommandOrientedInterface.html).

A lot of things are doing this kind of thing under the aspect
	oriented banner these days. At some point I'm going to dig into this
	more, to see if there's more than this pattern in play.

This is aspectish, but there's more to aspect oriented
	programming than this. In aspect terms, the decorators provide
	advice to the domain command's Execute method. However in order to
	do this, you have to organize everything around the commands, since
	only the Execute method can be advised. More flexible AOP tools,
	such as aspectJ allow you to advise _any_ method, and indeed some
	other things such as field access.


<span class="label">Share:</span>[![](/t_mini-a.png)](https://twitter.com/intent/tweet?url=http://martinfowler.com/bliki/DecoratedCommand.html&amp;text=Bliki: DecoratedCommand ➙  "Share on Twitter")[![](/fb-icon-20.png)](https://facebook.com/sharer.php?u=http://martinfowler.com/bliki/DecoratedCommand.html "Share on Facebook")[![](/gplus-16.png)](https://plus.google.com/share?url=http://martinfowler.com/bliki/DecoratedCommand.html "Share on Google Plus")

if you found this article useful, please share it. I appreciate the feedback and encouragement


