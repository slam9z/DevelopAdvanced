/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using NUnit.Framework;


[TestFixture]
public class MyStackTest 
{
  [Test]
  public void Empty() 
  {
    MyStack stack = new MyStack();

    stack.CheckInvariant();
    stack.Push("sample");
    stack.CheckInvariant();

    // Popping last element ok
    Assert.That(
      stack.Pop(), 
      Is.EqualTo("sample")
    ); 
    stack.CheckInvariant();

    // Delete from empty stack
    stack.Delete(1); 
    stack.CheckInvariant();
  }
}
