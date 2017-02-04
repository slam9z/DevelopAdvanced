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

public class Timer 
{
  public Timer() {}
  public void Start() {}
  public void End() {}
  public double ElapsedTime { get { return 0.0;} }
}

public class UrlFilter 
{
  public UrlFilter(string[] list) {}
  public bool Check(string a) { return true; }
}


[TestFixture]
public class FilterTest 
{

  readonly String[] SMALL_LIST = new String[] {"a","b"};
  readonly String[] HUGE_LIST  = new String[] {"a","b","c"};

  readonly String naughtyUrl = 
    "http://www.xxxxxxxxx.com";//greek
  Timer timer;
  UrlFilter filter;

  [SetUp]
  public void SetUp()
  {
    timer = new Timer();
  }

  [Test]
  public void SmallList()
  {
    filter = new UrlFilter(SMALL_LIST);
    timer.Start();
    filter.Check(naughtyUrl);
    timer.End();
    Assert.That(timer.ElapsedTime, Is.LessThan(1.0));
  }

  [Test]
  [Category("Long")]
  public void HugeList()
  {
    filter = new UrlFilter(HUGE_LIST);
    timer.Start();
    filter.Check(naughtyUrl);
    timer.End();
    Assert.That(timer.ElapsedTime, Is.LessThan(10.0));
  }
}

