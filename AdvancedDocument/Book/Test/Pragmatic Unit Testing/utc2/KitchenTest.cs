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
using System.Collections.Generic;

[TestFixture]
public class KitchenTest 
{
  public class FoodItem 
  {
    public FoodItem(string name) {}
  }

  public class Dessert : FoodItem 
  { 
    public Dessert(string name) : base(name) {} 
  }
  public class Entree : FoodItem 
  { 
    public Entree(string name) : base(name) {} 
  }
  public class Salad : FoodItem 
  { 
    public Salad(string name) : base(name) {} 
  }

  public class Order
  {
    public void AddFoodItem(FoodItem foodItem) {}
    public IEnumerator<FoodItem> GetEnumerator() { return null; }      
  }


  [Test]
  public void KitchenOrder() 
  {
    Order order = new Order();
    FoodItem dessert = new Dessert("Chocolate Decadence");
    FoodItem entree = new Entree("Beef Oscar");
    FoodItem salad  = new Salad("Parmesan Peppercorn");

    // Add out of order
    order.AddFoodItem(dessert);
    order.AddFoodItem(entree);
    order.AddFoodItem(salad);

    // But should come out in serving order
    IEnumerator<FoodItem> itr = order.GetEnumerator();

    Assert.That(salad, Is.EqualTo(itr.Current));
    itr.MoveNext();
    Assert.That(entree, Is.EqualTo(itr.Current));
    itr.MoveNext();
    Assert.That(dessert, Is.EqualTo(itr.Current));
    itr.MoveNext();

    // No more left
    Assert.That(itr.MoveNext(), Is.False);
  }


}
