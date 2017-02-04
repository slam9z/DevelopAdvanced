/**
 * Excerpted from the book, "Pragmatic Unit Testing in C# with NUnit, 2nd Ed."
 * ISBN 0-9776166-7-3
 * Copyrights apply to this code. It may not be used to create training material, 
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose. 
 * Visit http://www.pragmaticprogrammer.com/titles/utc2 for more book information.
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class Recipes : Form 
{
  private Button exitButton = new Button();
  private StatusBar statusBar = new StatusBar();
  private GroupBox searchGroupBox = new GroupBox();
  private ListBox searchList = new ListBox();
  private GroupBox ingredientsGroupBox = new GroupBox();
  private ListBox ingredientsList = new ListBox();
  private Button removeButton = new Button();
  private TextBox ingredientsText = new TextBox();
  private Button saveButton = new Button();
  private Button addButton = new Button();
  private Button searchButton = new Button();
  private TextBox titleText = new TextBox();

  public Recipes() 
  {
    InitializeComponent();
  }

  private void InitializeComponent() 
  {
    exitButton.Location = 
      new System.Drawing.Point(120, 232);
    exitButton.Size = new System.Drawing.Size(48, 24);
    exitButton.Text = "E&xit";
    exitButton.Click += 
      new System.EventHandler(exitButton_Click);

    statusBar.Location = new System.Drawing.Point(0, 261);
    statusBar.Size = new System.Drawing.Size(400, 16);

    searchGroupBox.Controls.Add(titleText);
    searchGroupBox.Controls.Add(searchList);
    searchGroupBox.Controls.Add(searchButton);
    searchGroupBox.Location = new System.Drawing.Point(8, 8);
    searchGroupBox.Size = new System.Drawing.Size(176, 216);
    searchGroupBox.TabStop = false;
    searchGroupBox.Text = "Recipes";

    searchList.Location = new System.Drawing.Point(16, 56);
    searchList.Size = new System.Drawing.Size(144, 147);
    searchList.SelectedIndexChanged += 
      new System.EventHandler(
        searchList_SelectedIndexChanged);

    searchButton.Location = new System.Drawing.Point(112, 24);
    searchButton.Size = new System.Drawing.Size(48, 20);
    searchButton.Text = "S&earch";
    searchButton.Click += 
      new System.EventHandler(searchButton_Click);

    titleText.Location = new System.Drawing.Point(16, 24);
    titleText.Size = new System.Drawing.Size(88, 20);

    ingredientsGroupBox.Controls.Add(ingredientsList);
    ingredientsGroupBox.Controls.Add(ingredientsText);
    ingredientsGroupBox.Controls.Add(addButton);
    ingredientsGroupBox.Controls.Add(removeButton);
    ingredientsGroupBox.Location = new System.Drawing.Point(200, 8);
    ingredientsGroupBox.Size = new System.Drawing.Size(192, 248);
    ingredientsGroupBox.TabStop = false;
    ingredientsGroupBox.Text = "Ingredients";

    addButton.Location = new System.Drawing.Point(136, 176);
    addButton.Size = new System.Drawing.Size(48, 23);
    addButton.Text = "&Add";
    addButton.Click += 
      new System.EventHandler(addButton_Click);

    ingredientsText.Location = new System.Drawing.Point(16, 176);
    ingredientsText.Size = new System.Drawing.Size(112, 20);

    removeButton.Enabled = false;
    removeButton.Location = new System.Drawing.Point(16, 208);
    removeButton.Size = new System.Drawing.Size(168, 32);
    removeButton.Text = "&Remove";
    removeButton.Click += 
      new System.EventHandler(removeButton_Click);

    ingredientsList.Location = new System.Drawing.Point(16, 24);
    ingredientsList.Size = new System.Drawing.Size(160, 134);
    ingredientsList.SelectedIndexChanged += 
      new System.EventHandler(
        ingredientsList_SelectedIndexChanged);

    saveButton.Enabled = false;
    saveButton.Location = new System.Drawing.Point(40, 232);
    saveButton.Size = new System.Drawing.Size(48, 24);
    saveButton.Text = "&Save";
    saveButton.Click += 
      new System.EventHandler(saveButton_Click);

    AutoScaleBaseSize = new System.Drawing.Size(5, 13);
    ClientSize = new System.Drawing.Size(400, 277);
    Controls.Add(saveButton);
    Controls.Add(searchGroupBox);
    Controls.Add(ingredientsGroupBox);
    Controls.Add(statusBar);
    Controls.Add(exitButton);
    ingredientsGroupBox.ResumeLayout(false);
    searchGroupBox.ResumeLayout(false);
    ResumeLayout(false);
  }

  [STAThread]
  static void Main() 
  {
    Directory.SetCurrentDirectory(@"../../recipes/");
    Application.Run(new Recipes());
  }

  private void exitButton_Click(object sender, 
                                System.EventArgs e) 
  {
    Application.Exit();
  }

  private void searchButton_Click(object sender, 
                                  System.EventArgs e) 
  {
    String toMatch = "*" + titleText.Text + "*";
		
    try 
    {
      string [] matchingFiles = Directory.GetFiles(@".", toMatch);
      searchList.DataSource = matchingFiles;
    }
    catch (Exception error) 
    {
      statusBar.Text = error.Message;
    }
  }

  private void 
  searchList_SelectedIndexChanged(object sender, 
                                  System.EventArgs e) 
  {
    string file = (string)searchList.SelectedItem;
    string line;
    char [] delim = new char[] { '=' };

    statusBar.Text = file;

    using (StreamReader reader = 
             new StreamReader(file)) 
    {
      while ((line = reader.ReadLine()) != null) 
      {
        string [] tokens = line.Split(delim, 2);
        switch (tokens[0]) 
        {
          case "NAME":
            titleText.Text = tokens[1];
            break;
          case "INGREDIENTS":
            try 
            {
              int count = Int32.Parse(tokens[1]);
              ingredientsList.Items.Clear();
              for (int i = 0; i < count; i++)
              {
                ingredientsList.Items.Add(reader.ReadLine());
              }
            }
            catch (Exception error) 
            {
              statusBar.Text = "Bad ingredient count: " + 
                 error.Message;
              return;
            }
            break;
          default:
            statusBar.Text = "Invalid recipe line: " + line;
            return;
        }
      }
    }
    saveButton.Enabled = false;
  }

  private void removeButton_Click(object sender, 
                                  System.EventArgs e) 
  {
    int index = ingredientsList.SelectedIndex;
    if (index >= 0) 
    {
      statusBar.Text = "Removed " + 
        ingredientsList.SelectedItem;
      ingredientsList.Items.RemoveAt(index);
      saveButton.Enabled = true;
    }	
  }

  private void addButton_Click(object sender, 
                               System.EventArgs e) 
  {
    string newIngredient = ingredientsText.Text;
    if (newIngredient.Length > 0) 
    {
      ingredientsList.Items.Add(newIngredient);
      saveButton.Enabled = true;
    }
  }

  private void 
  ingredientsList_SelectedIndexChanged(object sender, 
                                       System.EventArgs e) 
  {
    int index = ingredientsList.SelectedIndex;
    if (index < 0)
    {
      removeButton.Enabled = false;
    }
    else 
    {
      removeButton.Text = "&Remove " + 
        ingredientsList.SelectedItem;
      removeButton.Enabled = true;
    }
  }

  private void saveButton_Click(object sender, 
                                System.EventArgs e) 
  {
    string fileName = titleText.Text + ".txt";
    ICollection items = ingredientsList.Items;
    using (StreamWriter file = 
             new StreamWriter(fileName, false)) 
    {
      file.WriteLine("NAME={0}", titleText.Text);
      file.WriteLine("INGREDIENTS={0}", items.Count);
      foreach (string line in items) 
      {
        file.WriteLine(line);
      }
    }
    statusBar.Text = "Saved " + fileName;
  }
}
