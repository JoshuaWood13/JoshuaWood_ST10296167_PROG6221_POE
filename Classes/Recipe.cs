using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaWood_ST10296167_PROG6221_POE.Classes
{
    public class Recipe
    {

//------------------------------------------------------------------------------------------------------------------------------------------//
        private static string recipeMenu()
        {
            Console.WriteLine("====== RECIPE GENERATOR ======");
            Console.WriteLine("Please select an option:");
            Console.WriteLine("1) Enter recipe details");
            Console.WriteLine("2) Display recipe");
            Console.WriteLine("3) Scale recipe");
            Console.WriteLine("4) Reset recipe");
            Console.WriteLine("5) Clear recipe");
            Console.WriteLine("6) Quit");
            Console.WriteLine();
            Console.WriteLine("Enter choice: ");
            string choice = Console.ReadLine();
            while (choice == null )
            {
                Console.WriteLine("Please enter a number between 1-5: ");
                choice = Console.ReadLine();
            }
            return choice;
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void inputRecipeDetails()
        {
            int numOfIngredients;

            Console.WriteLine();
        }
    }
}
