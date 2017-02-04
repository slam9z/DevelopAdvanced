/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */



using System;

public class Cmp 
{

  ///
  /// <summary>
  /// Return the largest element in a list.
  /// </summary>
  /// <param name="list"> A list of integers </param>
  /// <returns>
  /// The largest number in the given list
  /// </returns>
  /// 


  public static int Largest(int[] list) 
  {
    int index, max=Int32.MaxValue;

    if (list.Length == 0) 
    {
      throw new ArgumentException("Empty list");
    }

    /*


    // ... 


    */

    for (index = 0; index < list.Length-1; index++) 
    {
      if (list[index] > max) 
      {
        max = list[index];
      }
    }
    return max;
  }

}


