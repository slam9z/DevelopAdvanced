/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using NUnit.Framework;

using WebCRM;

namespace WebCRM.Test.ProductAdoptionTest
{
  public class StubCustomerRepository : CustomerRepository
  {
    public string[] FindById(long id)
    {
      return null; 
    }
  }

  [TestFixture]
  public class NoDataFixture
  {
    [Test]
    public void OverallRateIsZero()
    {
      CustomerRepository customerRepository = 
        new StubCustomerRepository();

      ProductAdoptionService service = 
        new ProductAdoptionService(customerRepository);

      Assert.That(service.GetPercentage(), Is.EqualTo(0.0));
    }
  }
}

