// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

// References: 
// tutorialspoint. 2024. How to change the Foreground Color of Text in C# Console?. Available at: https://www.tutorialspoint.com/how-to-change-the-foreground-color-of-text-in-chash-console

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaWood_ST10296167_PROG6221_POE.Classes
{
    public class RecipeClass
    {
        //Declaring ingredient and step arrays that will store all ingredient and step information  
        private string[] stepArray;
        private IngredientsClass[] ingredientArray;

//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method provides a menu of recipe actions and prompts the user to choose what action they would like to perform.
        //It then returns a valid choice
        private static string recipeMenu()
        {
            //Stores user's choice
            string choice;  

            //Displays recipe generator menu
            borderColour("====== RECIPE GENERATOR MENU ======\n", ConsoleColor.Blue);
            Console.WriteLine("1) Enter recipe details");
            Console.WriteLine("2) Display recipe");
            Console.WriteLine("3) Scale recipe");
            Console.WriteLine("4) Reset recipe");
            Console.WriteLine("5) Clear recipe");
            Console.WriteLine("6) Quit");
            Console.WriteLine();

            //This do while loop prompts the user to choose an action from the menu and runs until the validChoice method returns true, indicating a valid choice 
            do
            {
                Console.Write("Enter choice: ");
                //Assigns user input to choice variable
                choice = Console.ReadLine();  

                //If a user does not enter a valid choice they are reminded of the valid range 
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
        //This method calls recipeMenu() to get a valid user choice and then calls the appropriate method to execute based on the user's choice
        public void buildRecipe()
        {
            //This while loop ensures the menu and user prompt will continue to apear after every action until a user decides to quit the application
            while (true)
            {
                //recipeMenu() retuns a valid user input which is assigned to choice variable
                string choice = recipeMenu();  

                //This switch statements uses the user's choice to decide what method to call
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
                        //Exits the application
                        Environment.Exit(0);  
                        break;
                }
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method accepts a user's choice and attempts to convert the choice to an int and only returns true if the choice is succesfully
        //converted to an int and that int falls within the range specified by the min and max parameters 
        private static bool validChoice(string choice, int min, int max)
        {
            //Stores converted integer 
            int num;  
            bool valid = int.TryParse(choice, out num);
            //Retuns true if conversion from string is succesfull and num is within the min and max range
            return valid && num >= min && num <= max;  
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//
        //This method accepts a user's input as a parameter and checks if the input is an integer. If not the user is prompted to enter an integer.
        //A valid integer is then returned
        private static int validNum(string input)
        {
            //Stores converted integer
            int result;  

            //This while loop keeps executing until the user's input is a valid integer
            while(!int.TryParse(input, out result))
            {
                Console.Write("Please enter a valid number: ");
                input = Console.ReadLine();
            }
            return result;
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//
        //This method accepts a user's input as a parameter and determines if the input is a double. If not the user is prompted to enter a double
        //A valid double is then returned
        private static double validQuantity(string input)
        {
            //Stores converted double
            double result;  

            //This while loop keeps executing until the user's input is a valid double
            while (!double.TryParse(input, out result))
            {
                Console.Write("Please enter a valid number: ");
                input = Console.ReadLine();
            }
            return result;
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method prompts the user for all recipe ingredient details and all recipe steps and saves these details in arrays
        private void inputRecipeDetails()
        {
            //Stores the number of ingredients in the recipe
            int ingredientNum;
            //Stores the number of steps for the recipe
            int steps;
            //Used to label ingredient and step numbers when iterating through for loops
            int count = 1;  

            Console.WriteLine();
            borderColour("---------RECIPE DETAILS---------", ConsoleColor.Green);
            Console.WriteLine();

            //Determines if a recipe already exists. If it does the user will be prompted to first clear the recipe before creating a new one
            if(ingredientArray != null)
            {
                Console.WriteLine("Please clear the current recipe before creating a new one!");
                Console.WriteLine();
                borderColour("--------------------------------", ConsoleColor.Green);
                Console.WriteLine();
            }
            else
            {
                //Prompts user for number of ingredients
                Console.Write("Enter number of ingredients in recipe: ");
                //Assigns a valid number entered by user
                ingredientNum = validNum(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("--------------------------------");

                //Initializing an ingredient array and setting its size to the user's input
                ingredientArray = new IngredientsClass[ingredientNum];
                //This for loop iterates over each element of the ingredient array
                for (int i = 0; i < ingredientNum; i++)
                {
                    string unitDecision;
                    //Create a new IngredientsClass object
                    ingredientArray[i] = new IngredientsClass();
                    //Prompts user to enter ingredient name
                    Console.Write($"Enter ingredient {count} name: ");
                    //Assings ingredient name to user input
                    ingredientArray[i].ingredientName = Console.ReadLine();
                    //Prompts the user to enter ingredient quantity
                    Console.Write($"Enter ingredient {count} quantity: ");
                    //Assigns ingredient quantity to a valid quantity input by the user
                    ingredientArray[i].ingredientQuantity = validQuantity(Console.ReadLine());
                    Console.WriteLine();
                    //Stores the choice for measurement unit in variable
                    unitDecision = ingredientArray[i].decideUnit();
                    //Uses unitDecision to assign the correct measurement unit to the ingredient
                    ingredientArray[i].assignUnit(ingredientArray[i], unitDecision);
                    //Saves all the ingredient information 
                    ingredientArray[i].saveOriginal();
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------");
                    count++;
                }
                Console.WriteLine();
                //Prompts the user to enter the required number of steps
                Console.Write("Enter number of recipe steps: ");
                //Assigns step number to a valid number entered by user
                steps = validNum(Console.ReadLine());
                Console.WriteLine();
                //Initializing step array and setting its size to the number chosen by user
                stepArray = new string[steps];
                //Resetting count so that steps can be properly numbered
                count = 1;
                //This for loop iterates over every element of the step array
                for (int j = 0; j < steps; j++)
                {
                    //Prompts the user to enter a step
                    Console.WriteLine($"Please type step {count} below:");
                    //Assings array element to the user's input
                    stepArray[j] = Console.ReadLine();
                    Console.WriteLine();
                    count++;
                }
            }
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//
        //This method displays all stored ingredient and step information for recipe
        private void displayRecipe()
        {
            Console.WriteLine();
            borderColour("---------FULL RECIPE---------", ConsoleColor.Magenta);

            //Checks if recipe exists before attempting to display
            if(ingredientArray != null)
            {
                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Ingredients:");
                //Count initialized in order to number ingredients
                int count = 1;
                //Displays the ingredient information for each ingredientsClass object in the ingredient array 
                foreach (IngredientsClass i in ingredientArray)
                {
                    Console.WriteLine($"{count}. {i.ingredientQuantity} {i.measurementUnitName}(s) of {i.ingredientName}");
                    count++;
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Steps:");
                //Count is reset in order to number the steps
                count = 1;
                //Displays each string element in the step array
                foreach (string x in stepArray)
                {
                    Console.WriteLine($"{count}. {x}");
                    count++;
                }
                Console.ResetColor();
                borderColour("--------------------------------", ConsoleColor.Magenta);
                Console.WriteLine();
            }
            //Displays to the user that no recipe has been created
            else
            {
                Console.WriteLine();
                Console.WriteLine("No Recipe Found");
                Console.WriteLine();
            }
            
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method prompts the user to select a scale factor from a menu, sets the scale factor based on user choice and then passes the scale
        //factor as an argument in order to scale each ingredient 
        private void scaleRecipe()
        {
            //Stores the user's choice
            string choice;
            //Stores the scale factor
            double scaleFactor;

            Console.WriteLine();
            borderColour("---------SCALE RECIPE---------", ConsoleColor.Cyan);

            //Checks if a recipe exists 
            if (ingredientArray != null)
            {
                //Displays scaling options
                Console.WriteLine("1) Half");
                Console.WriteLine("2) Double");
                Console.WriteLine("3) Triple");
                Console.WriteLine();
                //This do while loop continously prompts the user for a choice until a valid choice is input
                do
                {
                    Console.Write("Enter choice: ");
                    //Assings user input to choice variable
                    choice = Console.ReadLine();
                    Console.WriteLine();

                    //If input is not valid user is asked to enter a valid input
                    if (!validChoice(choice, 1, 3))
                    {
                        Console.WriteLine("Please enter a number between 1 and 3.");
                        Console.WriteLine();
                    }
                } while (!validChoice(choice, 1, 3));

                //Determins what scaling factor value to assign based on user input
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

                //Each IngredientsClass object in the ingredient array is scaled based on the chosen scale factor 
                foreach (IngredientsClass i in ingredientArray)
                {
                    i.scaleIngredients(i, scaleFactor);
                }
            }
            //Inform the user to create a recipe before scaling
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please create a recipe before scaling!");
                Console.WriteLine();
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method resets all IngredientClass object's quantities in the ingredient array to the original values first entered by the user
        private void resetValues()
        {
            Console.WriteLine();
            borderColour("---------RESET RECIPE---------", ConsoleColor.DarkYellow);
            Console.WriteLine();

            //Checks if a recipe exists 
            if(ingredientArray != null)
            {
                //Each IngredientsClass object's quantities in the ingredient array are assigned to the saved original values
                foreach (IngredientsClass i in ingredientArray)
                {
                    i.ingredientQuantity = i.originalQuantity;
                    i.measurementUnitName = i.originalunitName;
                    //Determines if the ingredient value is stored in ml or grams 
                    if (i.measurementUnitGrams == 0)
                    {
                        i.measurementUnitMl = i.originalUnitMl;
                    }
                    else
                    {
                        i.measurementUnitGrams = i.originalGrams;
                    }
                }
                //Displays to the user that the recipe quantiies has been reset 
                Console.WriteLine("Recipe quantities have been reset to original values!");
                Console.WriteLine();
            }
            //Informs the user to create a recipe before resetting quantities 
            else
            {
                Console.WriteLine("No recipe found. Please create a recipe before resseting ingredient quantities!");
                Console.WriteLine();
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method clears all ingredient array and step array values if a user confirms the action
        private void clearRecipe()
        {
            Console.WriteLine();
            borderColour("---------CLEAR RECIPE---------", ConsoleColor.Red);
            Console.WriteLine();
            //Clears values if user confirms 
            if (confrimPrompt())
            {
                ingredientArray = null;
                stepArray = null;
                Console.WriteLine();
                Console.WriteLine("Recipe has been cleared!");
                Console.WriteLine();
            }
            //Does not clear values if user declines
            else
            {
                Console.WriteLine();
                Console.WriteLine("Recipe will not be cleared!");
                Console.WriteLine();
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method prompts a user to confirm an action and returns a boolean value based on the decision 
        private static Boolean confrimPrompt()
        {
            //Stores user input
            string input;
            //Stores users decision 
            Boolean decision = false;
            //Flag to control the loop
            Boolean y = false;
            //Loops until user makes a valid choice
            while(y == false)
            {
                //Prompts the user to confirm their action
                Console.Write("Are you sure you would like to clear recipe? (Y/N): ");
                //Assigns user input to input variable
                input = Console.ReadLine();

                //If user confirms action, the method returns true
                if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
                {
                    y = true;
                    decision = true;
                    return decision;
                }
                //If the user declines the action, the method returns false
                else if (input.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    y = true;
                    decision= false;
                    return decision;
                }
                //If user enters an invalid input they are prompted to enter a valid input 
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
        //This method sets the colour of the text for a specific string 
        private static void borderColour(string border, ConsoleColor c)
        {
            //Text colour is assigned based on the passed parameter
            Console.ForegroundColor = c;  // (tutorialspoint, 2024)
            //Displays string parameter
            Console.WriteLine(border);
            //Resets the colour back to default
            Console.ResetColor();
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//