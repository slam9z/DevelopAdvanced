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
public class HistoryTest 
{
  [Test]
  public void CountDeMonet() 
  {
    Money money = new Money(42.00);
    money.Add(2);
    MoneyAssert.AssertNoCents(money);
  }
}
