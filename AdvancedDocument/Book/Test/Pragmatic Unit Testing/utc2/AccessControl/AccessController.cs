/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;

public class AccessController 
{
  private ILogger logger;
  private string  resource;

  public AccessController(string resource, 
                          ILogger logger) 
  {
    this.logger = logger;
    this.resource = resource;
    logger.SetName("AccessControl");
  }

  public bool CanAccess(string user, string password) 
  {
    logger.Log("Checking access for " + user + 
               " to " + resource);

    if (string.IsNullOrEmpty(password)) 
    {
      logger.Log("Missing password. Access denied");
      return false;
    }

    // more checks...

    logger.Log("Access granted");
    return true;
  }
}
