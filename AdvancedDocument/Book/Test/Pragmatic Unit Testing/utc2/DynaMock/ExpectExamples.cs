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
using DotNetMock.Dynamic;

interface ITaxCalculator 
{
  decimal CalculateTax(decimal amount, String state);
}

[TestFixture]
public class ExpectExamplesTest 
{
  [Test]
  [ExpectedException(typeof(ArgumentException))]
  public RejectInvalidStateAbbreviation() 
  {
    IMock mock = new DynamicMock(typeof(ITaxCalculator));

    mock.ExpectAndReturn("CalculateTax",7.25,100,"TX");
    mock.ExpectAndReturn("CalculateTax",7.00,100,"NC");
    mock.ExpectAndThrow("CalculateTax", 
      new ArgumentException(), 100, "XX");
 
    ITaxCalculator calc = mock.Object as ITaxCalculator;
 
    Assert.That(
      calc.CalculateTax(100, "TX"),
      Is.EqualTo(7.25)
    );
    Assert.That(
      calc.CalculateTax(100, "NC"),
      Is.EqualTo(7.00));
  
    calc.CalculateTax(100, "XX");
  }
}
