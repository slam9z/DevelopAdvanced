[NUnit expected exceptions](http://stackoverflow.com/questions/3407765/nunit-expected-exceptions)


## answer

I'm not sure what you've tried that is giving you trouble, but you can simply pass in a lambda as the first argument to Assert.Throws. Here's one from one of my tests that passes:
Assert.Throws<ArgumentException>(() => pointStore.Store(new[] { firstPoint }));



Okay, that example may have been a little verbose. Suppose I had a test

```cs
[Test]
[ExpectedException("System.NullReferenceException")]
public void TestFoo()
{
    MyObject o = null;
    o.Foo();
}
```

which would pass normally because o.Foo() would raise a null reference exception.

You then would drop the ExpectedException attribute and wrap your call to o.Foo() in an Assert.Throws.


```cs
[Test]
public void TestFoo()
{
    MyObject o = null;
    Assert.Throws<NullReferenceException>(() => o.Foo());
}
```

Assert.Throws "attempts to invoke a code snippet, represented as a delegate, in order to verify that it throws a particular exception." The () => DoSomething() syntax represents a lambda, essentially an anonymous method. So in this case, we are telling Assert.Throws to execute the snippet o.Foo().

So no, you don't just add a single line like you do an attribute; you need to explicitly wrap the section of your test that will throw the exception, in a call to Assert.Throws. You don't necessarily have to use a lambda, but that's often the most convenient.
 