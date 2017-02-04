/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;
using System.Text;

public class Mailer 
{
  private ICustomer customer;

  public Mailer(ICustomer customer) 
  {
    this.customer = customer;
  }

  public String ShortGreeting() 
  {
    return customer.Title + " " + customer.LastName;
  }

  public String FullGreeting() 
  {
    StringBuilder result = new StringBuilder();
    Append(result, customer.Title);
    Append(result, customer.FirstName);
    Append(result, customer.MiddleInitial);
    Append(result, customer.LastName);
    if (customer.NameSuffix.Length > 0)
    { 
      result.Append(", " + customer.NameSuffix);
    }
    return result.ToString();
  }

  private void Append(StringBuilder result, 
                      String field) 
  {
    if (field != null && field.Length > 0)
    {
      if (result.Length > 0)
      {
        result.Append(" ");
      }
    }
    result.Append(field);
  }
}
