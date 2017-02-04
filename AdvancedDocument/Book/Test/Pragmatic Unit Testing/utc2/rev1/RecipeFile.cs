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
using System.IO;

public class RecipeFormatException : Exception 
{
  public RecipeFormatException(string msg) : base(msg) {}
};


public class RecipeFile 
{
  public Recipe Load(Stream savedRecipe) 
  {

    using (StreamReader reader = new StreamReader(savedRecipe)) 
    {
      string line;
      List<string> lines = new List<string>();
      while ((line = reader.ReadLine()) != null) 
      {
        lines.Add(line);
      }

      return createRecipe(lines);
    }
  }

  public void Save(Stream savedRecipe, Recipe recipe)  
  {
    using (StreamWriter file = new StreamWriter(savedRecipe))
    {
      file.WriteLine("NAME={0}", recipe.Name);
      file.WriteLine(
        "INGREDIENTS={0}", 
        recipe.Ingredients.Count
      );

      foreach (string line in recipe.Ingredients) 
      {
        file.WriteLine(line);
      }
    }
  }

  private Recipe createRecipe(ICollection<string> lines)
  {
    char[] delim = new char[] {'='};
    Recipe recipe = new Recipe();
    foreach (string line in lines) 
    {
      string[] tokens = line.Split(delim, 2);

      switch (tokens[0]) 
      {
        case "TITLE": 
        {
          recipe.Name = tokens[1];
          break;
        }
        case "INGREDIENTS":
        {
          try 
          {
            int count = Int32.Parse(tokens[1]);
            for (int i = 0; i < count; i++)
            {
              recipe.AddIngredient(line);
            }
          } 
          catch (IOException error) 
          {
            throw new RecipeFormatException(
              "Bad ingredient count: " + error.Message);
          }
          break;
        }
      }
    }

    return recipe;
  }
}

