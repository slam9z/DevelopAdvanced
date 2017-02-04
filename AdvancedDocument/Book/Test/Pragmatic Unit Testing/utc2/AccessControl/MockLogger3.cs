/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using DotNetMock;
using System;

public class MockLogger3 : MockObject, ILogger 
{
  private ExpectationValue name = 
      new ExpectationValue("name");

  private ExpectationArrayList _msgs =
      new ExpectationArrayList("msgs");

  // Mock control interface

  public string ExpectedName 
  {
    set { name.Expected = value; }
  }

  public void AddExpectedMsg(string msg) 
  {
    _msgs.AddExpected(msg);
  }

  // Implement ILogger interface
  public void SetName(string name) 
  {
    name.Actual = name;
  }

  public void Log(string msg) 
  {
    _msgs.AddActual(msg);
  }
}
