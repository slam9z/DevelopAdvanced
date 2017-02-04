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

public class Connection : IDisposable
{
  public Connection(string name, int port, string user, string passwd) {}
  public void Connect() {}
  public void Disconnect() {}
  public void Dispose() {}
}


[TestFixture]
public class DBTest 
{
  private Connection dbConn;

  //[TestFixtureSetUp]
  public void PerFixtureSetup() 
  {
    dbConn = new Connection("mysql", 1521, "user", "pw");
    dbConn.Connect();
  }

  [SetUp]
  public void PerTestSetup()
  {
    // populate database with test data
  }

  [TearDown]
  public void PerTestTearDown()
  {
    // clean up database to avoid pollution
  }

  //[TestFixtureTearDown]
  public void PerFixtureTeardown() 
  {
    dbConn.Disconnect();
    dbConn.Dispose();
  }

  [Test]
  public void AccountAccess() 
  {
    // Uses dbConn
    //xxx xxx xxxxxx xxx xxxxxxxxx;//greek
    //xx xxx xxx xxxx x xx xxxx;//greek
  }

  [Test]
  public void EmployeeAccess() 
  {
    // Uses dbConn
    //xxx xxx xxxxxx xxx xxxxxxxxx;//greek
    //xxxx x x xx xxx xx xxxx;//greek
  }
}

