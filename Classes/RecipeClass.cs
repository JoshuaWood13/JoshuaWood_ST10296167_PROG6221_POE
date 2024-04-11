using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaWood_ST10296167_PROG6221_POE.Classes
{
    public class RecipeClass
    {
        private string[] stepArray;
        private IngredientsClass[] ingredientArray;
//------------------------------------------------------------------------------------------------------------------------------------------//
        private static string recipeMenu()
        {
            string choice;

            Console.WriteLine("====== RECIPE GENERATOR MENU ======\n");
            Console.WriteLine("1) Enter recipe details");
            Console.WriteLine("2) Display recipe");
            Console.WriteLine("3) Scale recipe");
            Console.WriteLine("4) Reset recipe");
            Console.WriteLine("5) Clear recipe");
            Console.WriteLine("6) Quit");
            Console.WriteLine();
            do
            {
                Console.Write("Enter choice: ");
                choice = Console.ReadLine();

                if (!validChoice(choice))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a number between 1 and 6.");
                    Console.WriteLine();
                }
            } while (!validChoice(choice));

            return choice;
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        
        public void buildRecipe()
        {
            while (true)
            {
                String choice = recipeMenu();
                switch (choice)
                {
                    case "1":
                        inputRecipeDetails();
                        break;

                    case "2":
                        displayRecipe();
                        break;

                    case "3":

                        break;

                    case "4":

                        break;

                    case "5":

                        break;

                    case "6":
                        Environment.Exit(0);
                        break;
                }
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//

        private static bool validChoice(string choice)
        {
            int num;
            bool valid = int.TryParse(choice, out num);
            return valid && num >= 1 && num <= 6;
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//
        private static int validNum(string input)
        {
            int result;
            while(!int.TryParse(input, out result))
            {
                Console.Write("Please enter a valid number: ");
                input = Console.ReadLine();
            }
            return result;
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//

        private void inputRecipeDetails()
        {
            int ingredientNum;
            int steps;
            int count = 1;

            Console.WriteLine();
            Console.WriteLine("---------RECIPE DETAILS---------");
            Console.WriteLine();
            Console.Write("Enter number of ingredients in recipe: ");
            ingredientNum = validNum(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("--------------------------------");
            ingredientArray = new IngredientsClass[ingredientNum];
            for(int i = 0; i < ingredientNum; i++)
            {
                string unitDecision;
                ingredientArray[i] = new IngredientsClass();
                Console.Write($"Enter ingredient {count} name: ");
                ingredientArray[i].ingredientName = Console.ReadLine();
                Console.Write($"Enter ingredient {count} quantity: ");
                ingredientArray[i].ingredientQuantity = validNum(Console.ReadLine());
                Console.WriteLine();
                unitDecision = ingredientArray[i].decideUnit();
                ingredientArray[i].assignUnit(ingredientArray[i],unitDecision);
                Console.WriteLine();
                Console.WriteLine("--------------------------------");
                count++;
            }
            Console.WriteLine();
            Console.Write("Enter number of recipe steps: ");
            steps = validNum(Console.ReadLine());
            Console.WriteLine();
            stepArray = new string[steps];
            count = 1;
            for (int j = 0; j < steps; j++)
            {
                Console.WriteLine($"Please type step {count} below:");
                stepArray[j] = Console.ReadLine();
                Console.WriteLine();
                count++;
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//
        private void displayRecipe()
        {
            Console.WriteLine();
            Console.WriteLine("---------FULL RECIPE---------");
            Console.WriteLine("Ingredients:");
            int count = 1;
            foreach(IngredientsClass i in ingredientArray)
            {
                Console.WriteLine($"{count}. {i.measurementUnitValue} {i.measurementUnitName}(s) of {i.ingredientName}");
                count++;
            }
            Console.WriteLine();
            Console.WriteLine("Steps:");
            foreach (string x in stepArray)
            {
                Console.WriteLine($"{count}. {x}");
                count++;
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine();
        }
    }
}
