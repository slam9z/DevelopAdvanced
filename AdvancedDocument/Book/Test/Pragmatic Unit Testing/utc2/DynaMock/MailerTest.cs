/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using DotNetMock.Dynamic;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;


[TestFixture]
public class CalculatorTest 
{
  [Test]
  public void AgeCalculation() 
  {
    IMock mock = 
      new DynamicMock(typeof(ICustomer));
    ICustomer customer = (ICustomer)mock.Object;
    mock.SetValue("Title",         "Mr.");
    mock.SetValue("FirstName",     "Fred");
    mock.SetValue("MiddleInitial", "E");
    mock.SetValue("LastName",      "Bloggs");
    mock.SetValue("NameSuffix",    "III");
    
    Mailer mailer = new Mailer(customer);

    Assert.That(
      "Mr. Bloggs", 
      Is.EqualTo(mailer.ShortGreeting())
    );

    Assert.That(
      "Mr. Fred E Bloggs, III", 
      Is.EqualTo(mailer.FullGreeting())
    );
  }
}
