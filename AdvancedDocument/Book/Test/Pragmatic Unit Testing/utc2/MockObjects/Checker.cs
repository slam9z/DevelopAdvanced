/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;
using System.IO;


public class Checker 
{
  SoundPlayer player;

  public Checker(SoundPlayer player) 
  {
    this.player = player;
  }

  public void Reminder(int hour) 
  {
    if (hour >= 17) 
    {
      Stream oggStream = getSoundStream();
      player.PlaySoundFile(oggStream);
    }
  }

  private Stream getSoundStream()
  {
    // return File.OpenRead("quit_whistle.ogg");
    return null;
  }

}

