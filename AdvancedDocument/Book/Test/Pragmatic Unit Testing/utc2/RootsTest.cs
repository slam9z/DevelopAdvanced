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


public class MyMath 
{
  public static double SquareRoot(double x) 
  {
    return Math.Sqrt(x);
  }
}

[TestFixture]
public class RootsTest 
{

  [Test]
  public void SquareRootUsingInverse() 
  {
    double x = MyMath.SquareRoot(4.0);
    Assert.That(4.0, Is.EqualTo(x*x).Within(0.0001));
  }


  [Test]
  public void SquareRootUsingStd() 
  {
    double number = 3880900.0;
    double root1 = MyMath.SquareRoot(number);
    double root2 = Math.Sqrt(number);
    Assert.That(root2, Is.EqualTo(root1).Within(0.0001));
  }


  public int Calculate(int a, int b) 
  {
    return a / (a+b);
  }

  [Test]
  //[ExpectedException(typeof(DivideByZeroException))]
  public void DivZero() 
  {
     Calculate(-5, 5);
  }
}
