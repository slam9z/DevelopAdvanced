/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

public class MyStack 
{
  public MyStack() 
  {
    elements = new string[100];
    nextIndex = 0;
  }
  
  public String Pop() 
  {
    return elements[--nextIndex];
  }

  // Delete n items from the elements en-masse
  public void Delete(int n) 
  {
    nextIndex -= n;
  }
  
  public void Push(string element) 
  {
    elements[nextIndex++] = element;
  }

  public String Top() 
  {
    return elements[nextIndex-1];
  }



  public void CheckInvariant() 
  {
    if (!(nextIndex >= 0 && 
          nextIndex  < elements.Length)) 
    {
      throw new InvariantException(
             "nextIndex out of range: "  +  nextIndex + 
             " for elements length " + elements.Length);
    }
  }

  


  private int nextIndex;
  private string[] elements;

}


