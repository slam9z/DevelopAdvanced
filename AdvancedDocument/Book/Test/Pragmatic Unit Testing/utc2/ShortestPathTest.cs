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
public class ShortestPathTest 
{
  TSP tsp;

  [SetUp]
  public void SetUp()
  {
    tsp = new TSP();
  }
 
  [Test]
  [Category("Short")]
  public void Use5Cities() 
  {
    Assert.That(tsp.ShortestPath(5), Is.AtMost(140));
  }
  
  // This one takes a while...
  [Test]
  [Category("Long")]
  [Category("Fred")]
  public void Use50Cities()
  {
    Assert.That(tsp.ShortestPath(50), Is.AtMost(2300));
  }
}
