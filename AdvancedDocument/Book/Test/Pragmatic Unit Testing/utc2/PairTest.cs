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

public struct Point 
{
  private int x;
  private int y;

  public Point(int x, int y) 
  {
    this.x = x;
    this.y = y;
  }

  public int X { get { return x; } }
  public int Y { get { return y; } }
}

public class PairTest 
{

  public const int MAX_DIST = 100;
  static public void AssertPairInRange(Point one, 
                                       Point two,
                                       String message) 
  {
    Assert.That(
      Math.Abs(one.X - two.X), 
      Is.AtMost(MAX_DIST),
      message
    );
    Assert.That(
      Math.Abs(one.Y - two.Y), 
      Is.AtMost(MAX_DIST),
      message
    );
  }

}
