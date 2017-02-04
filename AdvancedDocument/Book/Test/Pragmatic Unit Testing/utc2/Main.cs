/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

// Dummy definition
class List { public void Add(Object a) {} }

class MainTest {


  public void Addit(Object anObject) {
    List myList = new List();
    myList.Add(anObject);
    myList.Add(anObject);
    // more code...
  }



  static void Main() {

  }

}
