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
using System.IO;
using System.Collections.Generic;

[TestFixture]
public class LargestDataFileTest 
{
  private bool isComment(string line)
  {
      return line.StartsWith("#");
  }
  
  private int getExpectedLargest(string line)
  {
      string[] tokens = line.Split(' ');
      return Int32.Parse(tokens[0]);
  }
  
  private int[] getArguments(string line)
  {
    string[] tokens = line.Split(' ');
    const int argumentsIndex = 1;

    int[] args = 
        new int[tokens.Length - argumentsIndex];

    for (int i = argumentsIndex; i < tokens.Length; i++)
    {
      args[i - argumentsIndex] = Int32.Parse(tokens[i]);
    }
    
    return args;
  }
  
  /* Run all the tests in testdata.txt (does not test
   * exception case). We'll get an error if any of the
   * file I/O goes wrong.
   */
  [Test]
  public void FromFile() 
  {
    String line;
    StreamReader rdr = 
      new StreamReader("../../testdata.txt");
    
    while ((line = rdr.ReadLine()) != null) 
    {
      if (isComment(line))
      { 
        continue;
      }
      
      int expected = getExpectedLargest(line);

      int[] args = getArguments(line);

      Assert.That(Cmp.Largest(args), Is.EqualTo(expected));
    }
  }
}
