/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

 //using DotNetMock.Framework.Data;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using System;

[TestFixture]
public class AnotherAccessControllerTest 
{
  [Test]
  public void ValidUser() 
  {
    MockLogger3 logger = new MockLogger3();
    logger.ExpectedName = "AccessControl";
    logger.AddExpectedMsg(
      "Checking access for dave to secrets");
    logger.AddExpectedMsg("Access granted");

    // set up the mock database
    MockDbConnection conn = new MockDbConnection();
    MockCommand cmd = new MockCommand();
    MockDataReader rdr = new MockDataReader();

    conn.SetExpectedCommand(cmd);
    cmd.SetExpectedCommandText(
      AccessController1.CHECK_SQL);
    cmd.SetExpectedExecuteCalls(1);
    cmd.SetExpectedParameter(
      new MockDataParameter("@user",     "dave"));
    cmd.SetExpectedParameter(
      new MockDataParameter("@password", "shhh"));
    cmd.SetExpectedParameter(
      new MockDataParameter("@resource", "secrets"));

    cmd.SetExpectedReader(rdr);
    object [,] rows = new object[1,1];
    rows[0, 0] = 1;
    rdr.SetRows(rows);

    AccessController1 access = 
      new AccessController1("secrets", logger, conn);

    Assert.That(
      access.CanAccess("dave", "shhh"),
      Is.True
    );
    logger.Verify();
    conn.Verify();
    cmd.Verify();
  }
}
