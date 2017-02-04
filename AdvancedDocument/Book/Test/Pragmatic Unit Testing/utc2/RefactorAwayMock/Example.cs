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
using System.Threading;

class RefactorAwayMockExample 
{

  public void WaitForData(int dataSize) 
  {
    int timeToWait;
    if (dataSize < 100)
    {
      timeToWait = 50;
    }
    else if (dataSize < 250)
    {
      timeToWait = 100;
    }
    else
    {
      timeToWait = 200;
    }

    Thread.Sleep(timeToWait);
  }

  public static void Main() 
  { 
  }
}

class Waiter {

  public int HowLongToWait(int dataSize) 
  {
    int timeToWait;
    if (dataSize < 100)
    {
      timeToWait = 50;
    }
    else if (dataSize < 250)
    {
      timeToWait = 100;
    }
    else
    {
      timeToWait = 200;
    }

    return timeToWait;
  }

  public void WaitForData(int dataSize) 
  {
    Thread.Sleep(HowLongToWait(dataSize));
  }



  [Test]
  void WaitTimes() 
  {
    Waiter w = new Waiter();
    Assert.That(w.HowLongToWait(0), Is.EqualTo(50));
    Assert.That(w.HowLongToWait(99), Is.EqualTo(50));
    Assert.That(w.HowLongToWait(100), Is.EqualTo(100));
    Assert.That(w.HowLongToWait(249), Is.EqualTo(100));
    Assert.That(w.HowLongToWait(250), Is.EqualTo(200));
    Assert.That(w.HowLongToWait(251), Is.EqualTo(200));
  }

}
