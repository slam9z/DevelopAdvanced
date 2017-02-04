/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */


using NUnit.Framework;

using System;

[TestFixture]
public class AccessControllerTest 
{
  [Test]
  public void MissingPassword1() 
  {
    MockLogger1 logger = new MockLogger1();
    AccessController access = 
      new AccessController("secrets", logger);

    Assert.That(
      access.CanAccess("dave", null),
      Is.False
    );

    Assert.That(
      access.CanAccess("dave", ""),
      Is.False
    );
  }


  [Test]
  public void MissingPassword2() 
  {
    MockLogger2 logger = new MockLogger2();
    logger.ExpectedName = "AccessControl";
    AccessController access = 
      new AccessController("secrets", logger);

    Assert.That(
      access.CanAccess("dave", null),
      Is.False
    );
    logger.Verify();
  }


  [Test]
  public void MissingPassword3() 
  {
    MockLogger3 logger = new MockLogger3();
    logger.ExpectedName = "AccessControl";
    logger.AddExpectedMsg(
      "Checking access for dave to secrets");
    logger.AddExpectedMsg(
      "Missing password. Access denied");
    AccessController access = 
      new AccessController("secrets", logger);

    Assert.That(
      access.CanAccess("dave", null),
      Is.False
    );
    logger.Verify();
  }


}

