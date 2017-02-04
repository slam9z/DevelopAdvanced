/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Recipe 
{
  protected string name;
  protected List<string> ingredients;

  public Recipe() 
  {
    name = string.Empty;
    ingredients = new List<string>();
  }

  public Recipe(Recipe another) 
  {
    name = another.name;
    ingredients = new List<string>(another.ingredients);
  }

  public string Name 
  {
    get { return name; }
    set { name = value; }
  }

  public ReadOnlyCollection<string> Ingredients 
  {
    get 
    { 
      return 
        new ReadOnlyCollection<string>(ingredients); 
    }
  }

  public void AddIngredient(string ingredient) 
  {
    ingredients.Add(ingredient);
  }
}
