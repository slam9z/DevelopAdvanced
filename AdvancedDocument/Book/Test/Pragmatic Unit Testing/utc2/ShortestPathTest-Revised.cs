/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

[TestFixture]
[Category("Long")]
public class ShortestPathTest-Revised 
{
  TSP tsp;

  [Test]
  public void Use50Cities() 
  {
    tsp = new TSP(); // load with default cities
    Assert.That(tsp.ShortestPath(50), Is.AtMost(2300));
  }

  [Test]
  public void Use100Cities() 
  {
    tsp = new TSP(); // load with default cities
    Assert.That(tsp.ShortestPath(100), Is.AtMost(4675));
  }

  [Test]
  public void Use150Cities() 
  {
    tsp = new TSP(); // load with default cities
    Assert.That(tsp.ShortestPath(150), Is.AtMost(5357));
  }
}
