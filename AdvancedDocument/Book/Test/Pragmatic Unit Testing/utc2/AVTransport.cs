/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

public interface AVTransport {

  /// Move the current position ahead by this many 
  /// seconds. Fast-forwarding past EOT
  /// leaves the position at EOT
  void FastForward(double seconds);

  
  /// Move the current position backwards by this 
  /// many seconds. Rewinding past zero leaves 
  /// the position at zero
  void Rewind(double seconds);
  
  /// Return current time position in seconds
  double CurrentTimePosition();
  
  /// Mark the current time position with label
  void MarkTimePosition(String name);
  
  /// Change the current position to the one 
  /// associated with the marked name
  void GotoMark(String name);

}
