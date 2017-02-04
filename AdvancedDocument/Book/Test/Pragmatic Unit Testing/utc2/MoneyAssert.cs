/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;
using NUnit.Framework;


public class MoneyAssert 
{
  // Assert that the amount of money is an even
  // number of dollars (no cents)
  public static void AssertNoCents(Money amount,
                                   String message) 
  {
    Assert.That( 
        Decimal.Truncate(amount.AsDecimal()),
        Is.EqualTo(amount.AsDecimal()), 
        message);
  }

  // Assert that the amount of money is an even
  // number of dollars (no cents)
  public static void AssertNoCents(Money amount) 
  {
    AssertNoCents(amount, String.Empty);
  }
}
