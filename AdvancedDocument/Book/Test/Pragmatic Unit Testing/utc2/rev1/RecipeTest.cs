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
using System.IO;

[TestFixture]
public class RecipeTest 
{
  const string CHEESEBURGER = "Cheeseburger";
  const string SIRLOIN = "1/4 lb ground sirloin";
  const string CHEESE = "3 slices Vermont cheddar cheese";
  const string BACON = "2 slices maple-cured bacon";
  const string RECIPE_FILE_NAME = "recipe.save";     

  [TearDown]
  public void TearDown()
  {
    if (File.Exists(RECIPE_FILE_NAME))
    {
      File.Delete(RECIPE_FILE_NAME);
    }
  }

  [Test]
  public void SaveAndRestore() 
  {
    Recipe originalRecipe = new Recipe();
    originalRecipe.Name = CHEESEBURGER;
    originalRecipe.AddIngredient(SIRLOIN);
    originalRecipe.AddIngredient(CHEESE);
    originalRecipe.AddIngredient(BACON);

    Stream recipeStream;
    RecipeFile filer; 
    using (recipeStream = 
        File.OpenWrite(RECIPE_FILE_NAME))
    {
      filer = new RecipeFile();    
      filer.Save(recipeStream, originalRecipe);
    }

    // Now get it back
    Recipe reconstitutedRecipe;
    using (recipeStream = 
        File.OpenRead(RECIPE_FILE_NAME))
    {
      filer = new RecipeFile();
      reconstitutedRecipe = filer.Load(recipeStream);
    }
  
    Assert.That(
      reconstitutedRecipe.Name, 
      Is.EqualTo(originalRecipe.Name)
    );

    int originalIngredientCount =
      originalRecipe.Ingredients.Count;

    Assert.That(
      reconstitutedRecipe.Ingredients.Count,
      Is.EqualTo(originalIngredientCount)
    );

    for (int i=0; i < originalIngredientCount; i++)
    {
      Assert.That(
        reconstitutedRecipe.Ingredients[i], 
        Is.EqualTo(originalRecipe.Ingredients[i])
      );
    }
  }
}
