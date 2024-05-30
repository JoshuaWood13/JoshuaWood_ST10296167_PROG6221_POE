// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

// References:
// IronPdf. 2024. C# Orderby (How It Works For Developers). Available at: https://ironpdf.com/blog/net-help/csharp-orderby-guide/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaWood_ST10296167_PROG6221_POE.Classes
{
    public class RecipeManagerClass
    {
        private List<RecipeClass> recipeList {  get; set; }

        //Constructor
        public RecipeManagerClass()
        {
            recipeList = new List<RecipeClass>();
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        public void addRecipe(RecipeClass recipe)
        {
            recipeList.Add(recipe);
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        public void removeRecipe(RecipeClass recipe)
        {
            recipeList.Remove(recipe);
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method sorts the recipe List alphabetically by recipe name and then displays the list of recipes and gets user input to select
        //a recipe from the list. This recipe is then returned
        public RecipeClass displayOrderedRecipes(string prompt)
        {
            if (recipeList == null || recipeList.Count == 0)
            {
                return null;
            }

            recipeList = recipeList.OrderBy(r => r.recipeName).ToList(); // (IronPdf,2024).
            int count = 1;
            Console.WriteLine();
            Console.WriteLine("################################");
            foreach (RecipeClass recipe in recipeList)
            {
                Console.WriteLine($"{count}. {recipe.recipeName}");
                count++;
            }
            Console.WriteLine("################################");
            Console.WriteLine();

            while (true)
            {
                Console.Write($"Select a recipe to {prompt}: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    if (choice >= 1 && choice <= recipeList.Count)
                    {
                        return recipeList[choice - 1];
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid recipe number. Please select a valid number.");
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid number.");
                    Console.WriteLine();
                }
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//