/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

public interface StackExercise {
  
  /// <summary>
  /// Return and remove the most recent item from 
  /// the top of the  stack.  
  /// </summary>
  /// <exception cref="StackEmptyException">
  /// Throws exception if the stack is empty.
  /// </exception>
  String Pop();
  
  
  /// <summary>
  /// Add an item to the top of the stack.
  /// </summary>
  /// <param name="item">A String to push 
  /// on the stack</param>
  void Push(String item);
  
  /// <summary>
  /// Return but do not remove the most recent 
  /// item from the top of the stack.  
  /// </summary>
  /// <exception cref="StackEmptyException">
  /// Throws exception if the stack is empty.
  /// </exception>
  String Top();
  
  /// <summary>
  /// Returns true if the stack is empty.
  /// </summary>
  bool IsEmpty();
}
