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
  private int[] getNumberList(string line) 
  {
    string[] tokens = line.Split(null);

    List<int> numberList = new List<int>();
      
    for (int i=1; i < tokens.Length; i++) 
    {
      numberList.Add(Int32.Parse(tokens[i]));
    }

    return numberList.ToArray();
  }

  private int getLargestNumber(string line) 
  {
    string[] tokens = line.Split(null);

    string val = tokens[0];
    int expected = Int32.Parse(val);

    return expected;
  }

  private bool hasComment(string line)
  {
    return line.StartsWith("#");
  }

  // Run all the tests in testdata.txt (does not test
  // exception case). We'll get an error if any of the
  // file I/O goes wrong.
  [Test]
  public void FromFile() 
  {
    string line;
    // most IDEs output binaries in bin/[Debug,Release]
    StreamReader reader = 
        new StreamReader("../../testdata.txt");
    
    while ((line = reader.ReadLine()) != null) 
    {
      if (hasComment(line)) 
      {
        continue;
      }

      int[] numbersForLine = getNumberList(line);
      int actualLargest = Cmp.Largest(numbersForLine);
      int expectedLargest = getLargestNumber(line);

      Assert.That(actualLargest, Is.EqualTo(expectedLargest));
    }
  }
}
