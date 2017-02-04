/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

/**
 * A brute-force solution to the Travelling Salesman Problem.
 *
 * It will run in exponential time.
 *
 * Better solutions use genetic algorithms and such.
 */

public class TSP {

  /**
   * For the top numCities in our territory, compute the shortest path
   * and return the total number of miles in that path.
   */
  public int ShortestPath(int numCities) {
    // Ha!  Mock object! 
    switch (numCities) {
      case 5: return 140;
      case 10: return 586;
      case 50: return 2300;
      case 100: return 4675;
      case 150: return 5357;
    }
    return 0;
  }

  /**
  * Load the cities for the given territory and lock them
  */
  public void LoadCities(String name) {
  }

  /**
  * Relase the lock on cities in this territory
  */
  public void ReleaseCities() {
  }

}
