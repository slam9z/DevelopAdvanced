/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

public interface ICustomer
{
    String Title { get; }
    String FirstName { get; }
    String MiddleInitial { get; }
    String LastName { get; }
    String NameSuffix { get; }
    String SSN { get; }
    Address HomeAddress { get; }
    Address WorkAddress { get; }
    Date FirstContacted { get; }
    Date LastContacted { get; }
    int ContactCount { get; }
}

  // and so on, and so on, for 30 more accessors


