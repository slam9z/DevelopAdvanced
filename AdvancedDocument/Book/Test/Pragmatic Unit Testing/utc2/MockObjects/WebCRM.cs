/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

namespace WebCRM
{
  public interface CustomerRepository
  {
    string[] FindById(long id);
  }

  public class MySqlCustomerRepository : CustomerRepository
  {
    public string[] FindById(long id)
    {
      string[] row = new string[4];
      //xxxx xx xxxxx//greek
      return row;
    }
  }

  public class ProductAdoptionService
  {
    CustomerRepository repository;

    public ProductAdoptionService(CustomerRepository repository)
    {
      this.repository = repository;
    }

    public float GetPercentage()
    {
      if (repository.FindById(0) == null)
      {
        return 0.0f;
      }

      return 100.0f;
    }
  }
}
