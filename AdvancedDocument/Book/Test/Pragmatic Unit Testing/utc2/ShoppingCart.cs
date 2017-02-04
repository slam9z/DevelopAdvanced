/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;
using System.Collections;

public class Item {}
public class ArgumentOutOfRangeException : Exception {}
public class NoSuchItemException : Exception {}


public interface ShoppingCart {
  
  /// <summary>
  /// Add this many of this item to the 
  /// shopping cart.
  /// </summary>
  /// <exception cref="ArgumentOutOfRangeException">
  /// </exception>
  void AddItems(Item anItem, int quantity);
  
  /// <summary>
  /// Delete this many of this item from the 
  /// shopping cart
  /// </summary>
  /// <exception cref="ArgumentOutOfRangeException">
  /// </exception>
  /// <exception cref="NoSuchItemException">
  /// </exception>
  void DeleteItems(Item anItem, int quantity);

  /// <summary>
  /// Count of all items in the cart 
  /// (that is, all items x qty each)
  /// </summary>
  int ItemCount { get; }
  
  
  /// Return iterator of all items 
  IEnumerable GetEnumerator();
}

