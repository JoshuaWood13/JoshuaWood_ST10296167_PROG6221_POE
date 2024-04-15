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

            borderColour("====== RECIPE GENERATOR MENU ======\n", ConsoleColor.Blue);
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

                if (!validChoice(choice,1 ,6 ))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a number between 1 and 6.");
                    Console.WriteLine();
                }
            } while (!validChoice(choice,1 ,6 ));

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
                        scaleRecipe();
                        break;

                    case "4":
                        resetValues();
                        break;

                    case "5":
                        clearRecipe();
                        break;

                    case "6":
                        Environment.Exit(0);
                        break;
                }
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private static bool validChoice(string choice, int min, int max)
        {
            int num;
            bool valid = int.TryParse(choice, out num);
            return valid && num >= min && num <= max;
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
        private static double validQuantity(string input)
        {
            double result;
            while (!double.TryParse(input, out result))
            {
                Console.Write("Please enter a valid number: ");
                input = Console.ReadLine();
            }
            return result;
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void inputRecipeDetails()
        {
            int ingredientNum;
            int steps;
            int count = 1;

            Console.WriteLine();
            borderColour("---------RECIPE DETAILS---------", ConsoleColor.Green);
            Console.WriteLine();

            if(ingredientArray != null)
            {
                Console.WriteLine("Please clear the current recipe before creating a new one!");
                Console.WriteLine();
                borderColour("--------------------------------", ConsoleColor.Green);
                Console.WriteLine();
            }
            else
            {
                Console.Write("Enter number of ingredients in recipe: ");
                ingredientNum = validNum(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("--------------------------------");
                ingredientArray = new IngredientsClass[ingredientNum];
                for (int i = 0; i < ingredientNum; i++)
                {
                    string unitDecision;
                    ingredientArray[i] = new IngredientsClass();
                    Console.Write($"Enter ingredient {count} name: ");
                    ingredientArray[i].ingredientName = Console.ReadLine();
                    Console.Write($"Enter ingredient {count} quantity: ");
                    ingredientArray[i].ingredientQuantity = validQuantity(Console.ReadLine());
                    Console.WriteLine();
                    unitDecision = ingredientArray[i].decideUnit();
                    ingredientArray[i].assignUnit(ingredientArray[i], unitDecision);
                    ingredientArray[i].saveOriginal();
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
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//
        private void displayRecipe()
        {
            Console.WriteLine();
            borderColour("---------FULL RECIPE---------", ConsoleColor.Magenta);

            if(ingredientArray != null)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Ingredients:");
                int count = 1;
                foreach (IngredientsClass i in ingredientArray)
                {
                    Console.WriteLine($"{count}. {i.ingredientQuantity} {i.measurementUnitName}(s) of {i.ingredientName}");
                    count++;
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Steps:");
                count = 1;
                foreach (string x in stepArray)
                {
                    Console.WriteLine($"{count}. {x}");
                    count++;
                }
                Console.ResetColor();
                borderColour("--------------------------------", ConsoleColor.Magenta);
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No Recipe Found");
                Console.WriteLine();
            }
            
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void scaleRecipe()
        {
            string choice;
            double scaleFactor;

            Console.WriteLine();
            borderColour("---------SCALE RECIPE---------", ConsoleColor.Cyan);

            if (ingredientArray != null)
            {
                Console.WriteLine("1) Half");
                Console.WriteLine("2) Double");
                Console.WriteLine("3) Triple");
                Console.WriteLine();
                do
                {
                    Console.Write("Enter choice: ");
                    choice = Console.ReadLine();
                    Console.WriteLine();

                    if (!validChoice(choice, 1, 3))
                    {
                        //Console.WriteLine();
                        Console.WriteLine("Please enter a number between 1 and 3.");
                        Console.WriteLine();
                    }
                } while (!validChoice(choice, 1, 3));

                if (int.Parse(choice) == 1)
                {
                    scaleFactor = 0.5;
                }
                else if (int.Parse(choice) == 2)
                {
                    scaleFactor = 2;
                }
                else
                {
                    scaleFactor = 3;
                }

                foreach (IngredientsClass i in ingredientArray)
                {
                    i.scaleIngredients(i, scaleFactor);
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please create a recipe before scaling!");
                Console.WriteLine();
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void resetValues()
        {
            Console.WriteLine();
            borderColour("---------RESET RECIPE---------", ConsoleColor.DarkYellow);
            Console.WriteLine();

            if(ingredientArray != null)
            {
                foreach (IngredientsClass i in ingredientArray)
                {
                    i.ingredientQuantity = i.originalQuantity;
                    i.measurementUnitName = i.originalunitName;
                    if (i.measurementUnitGrams == 0)
                    {
                        i.measurementUnitMl = i.originalUnitMl;
                    }
                    else
                    {
                        i.measurementUnitGrams = i.originalGrams;
                    }
                }
                Console.WriteLine("Recipe quantities have been reset to original values!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("No recipe found. Please create a recipe before resseting values!");
                Console.WriteLine();
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void clearRecipe()
        {
            Console.WriteLine();
            borderColour("---------CLEAR RECIPE---------", ConsoleColor.Red);
            Console.WriteLine();
            if (confrimPrompt())
            {
                ingredientArray = null;
                stepArray = null;
                Console.WriteLine();
                Console.WriteLine("Recipe has been cleared!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Recipe will not be cleared!");
                Console.WriteLine();
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private static Boolean confrimPrompt()
        {
            string input;
            Boolean decision = false;
            Boolean y = false;
            while(y == false)
            {
                Console.Write("Are you sure you would like to clear recipe? (Y/N): ");
                input = Console.ReadLine();

                if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    y = true;
                    decision = true;
                    return decision;
                }
                else if (input.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    y = true;
                    decision= false;
                    return decision;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter Y or N!");
                    Console.WriteLine();
                }
            }
            return decision;
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private static void borderColour(string border, ConsoleColor c)
        {
            Console.ForegroundColor = c;
            Console.WriteLine(border);
            Console.ResetColor();
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
