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


[TestFixture]
public class LargestTest
{




  [Test]
  public void LargestOf3()
  {
    int[] numbers;

    numbers = new int[] {9, 8, 7};
    Assert.That(Cmp.Largest(numbers), Is.EqualTo(9));

    numbers = new int[] {8, 9, 7};
    Assert.That(Cmp.Largest(numbers), Is.EqualTo(9));


    numbers = new int[] {7, 8, 9};
    Assert.That(Cmp.Largest(numbers), Is.EqualTo(9));


  }






  [Test]
//  [ExpectedException(typeof(ArgumentException))]
  public void Empty() 
  {
    Cmp.Largest(new int[] {});
  }




  [Test]
  public void LargestOf3Alt() 
  {
    int[] numbers = new int[3];
    numbers[0] = 8;
    numbers[1] = 9;
    numbers[2] = 7;
    Assert.That(Cmp.Largest(numbers), Is.EqualTo(9));
  }





}


