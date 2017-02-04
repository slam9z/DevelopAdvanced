/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

//
// Compass bearing
//
public class Bearing 
{
  protected int bearing; // 0..359

  // 
  // Initialize a bearing to a value from 0..359
  // 
  public Bearing(int degrees) 
  {
    if (degrees < 0 || degrees > 359) 
    {
      throw new ArgumentException("out of range", 
                                  "degrees");
    }
    bearing = degrees;
  }

  //
  // Return the angle between our bearing and another.
  // May be negative.
  // 
  public int AngleBetween(Bearing anOther) 
  {
    return bearing - anOther.bearing;
  }
}
