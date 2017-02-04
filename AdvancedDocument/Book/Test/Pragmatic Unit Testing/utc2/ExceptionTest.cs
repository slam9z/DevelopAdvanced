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

public class WhitePages
{
  public static void ImportList(string listName) 
  { 
    if (listName == null)
    {
      throw new ArgumentNullException("listName");
    }
  }
}


[TestFixture]
public class ImportListTest
{


  [Test] 
  //[ExpectedException(typeof(ArgumentNullException))]
  public void NullList() 
  {
    WhitePages.ImportList(null);
    // Shouldn't get to here
  }



  [Test] 
  [Ignore("Out of time.  Will Continue Monday. --AH")]
  public void Something() 
  {
    //xxx xxx xxxxxx xxxxx xxxx;//greek
  }


}

