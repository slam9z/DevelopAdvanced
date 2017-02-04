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

[TestFixture]
public class CheckerTest
{
  MockSoundPlayer player;
  Checker checker;

  [SetUp]
  public void SetUp() 
  {
    player = new MockSoundPlayer();
    checker = new Checker(player);
  }

  [Test]
  public void NoAlarmAt4PM()
  {
    checker.Reminder(16);
    Assert.That(player.CheckAndResetSound(), Is.False);
  }

  [Test]
  public void AlarmSoundsAt5PM()
  {
    checker.Reminder(17);
    Assert.That(player.CheckAndResetSound(), Is.True);
  }

  [Test]
  public void AlarmSoundsAfter5PM()
  {
    checker.Reminder(19);
    Assert.IsTrue(player.CheckAndResetSound(), "19:00");
  }
}
