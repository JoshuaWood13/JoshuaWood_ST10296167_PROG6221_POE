using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Part3_POE.Classes
{
    public class RecipeClass
    {
        //Declaring ingredient and step Lists that will store all ingredient and step information  
        public List<string> stepList { get; set; }
        public List<IngredientsClass> ingredientList { get; set; }

        public string recipeName { get; set; }
        public double recipeCalorieTotal { get; set; }
        //Declaring delegate for the calorie alert
        public delegate void calorieAlert(double num);

        //Constructor
        public RecipeClass()
        {
            ingredientList = new List<IngredientsClass>();
            stepList = new List<string>();
        }

        public string displayRecipeDetails(RecipeClass recipe)
        {
            StringBuilder display = new StringBuilder();
            display.AppendLine($"Recipe: {recipe.recipeName}\n");
            display.AppendLine($"Calories: {recipe.recipeCalorieTotal}\n");
            display.AppendLine($"Ingredients: \n");
            int ingredientNum = 1;
            foreach (IngredientsClass ingredient in recipe.ingredientList)
            {
                display.AppendLine($"{ingredientNum}. {ingredient.ingredientQuantity} {ingredient.measurementUnitName}(s) of {ingredient.ingredientName}");
                ingredientNum++;
            }
            display.AppendLine("\nSteps:\n");
            int stepNum = 1;
            foreach(var step in recipe.stepList)
            {
                display.AppendLine($"{stepNum}. {step}");
                stepNum++;
            }
            return display.ToString();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method provides a menu of recipe actions and prompts the user to choose what action they would like to perform.
        //It then returns a valid choice
        private static string recipeMenu()
        {
            //Stores user's choice
            string choice;

            //Displays recipe generator menu
            borderColour("====== RECIPE GENERATOR MENU ======\n", ConsoleColor.Blue);
            Console.WriteLine("1) Create a recipe");
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
                if (!validChoice(choice, 1, 6))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a number between 1 and 6.");
                    Console.WriteLine();
                }
            } while (!validChoice(choice, 1, 6));

            return choice;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method calls recipeMenu() to get a valid user choice and then calls the appropriate method to execute based on the user's choice
        public void buildRecipe(RecipeManagerClass recipeManager)
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
                        //inputRecipeDetails(recipeManager);
                        break;

                    case "2":
                        displayRecipe(recipeManager);
                        break;

                    case "3":
                        scaleRecipe(recipeManager);
                        break;

                    case "4":
                        resetValues(recipeManager);
                        break;

                    case "5":
                        clearRecipe(recipeManager);
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
        public static bool validChoice(string choice, int min, int max)
        {
            //Stores converted integer 
            int num;
            bool valid = int.TryParse(choice, out num);
            //Retuns true if conversion from string is succesfull and num is within the min and max range
            return valid && num >= min && num <= max;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------//


        public static int validNum(string input)
        {
            //Stores converted integer
            int result;

            //This while loop keeps executing until the user's input is a valid integer
            while (!int.TryParse(input, out result) || result <= 0)
            {
                Console.WriteLine();
                Console.Write("Please enter a valid non-negative number above zero: ");
                input = Console.ReadLine();
            }
            return result;
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------//
        //This method accepts a user's input as a parameter and determines if the input is a double. If not the user is prompted to enter a double
        //A valid double is then returned
        public bool validDouble(string input)
        {
            double result;
            bool isValid;

            isValid = double.TryParse(input, out result) && result > 0;
            return isValid;
        }

        public bool validCalorieDouble(string input)
        {
            double result;
            bool isValid;

            isValid = double.TryParse(input, out result) && result >= 0;
            return isValid;     
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method takes a users string input and checks if it is null or contains digits. If so, the user is prompted again until they enter
        //a valid string
        public bool validString(string input)
        {
            bool isValid;

            if(!string.IsNullOrWhiteSpace(input) && !input.Any(char.IsDigit)) // (StackOverflow,2019)
            {
                isValid = true;
                return isValid;
            }
            else
            {
                isValid= false;
                return isValid;
            }
        }




        //This method accepts a user's input as a parameter and checks if the input is an integer. If not the user is prompted to enter an integer.
        //A valid integer is then returned
        //public static int validNum(string input)
        //{
        //    //Stores converted integer
        //    int result;

        //    //This while loop keeps executing until the user's input is a valid integer
        //    while (!int.TryParse(input, out result) || result <= 0)
        //    {
        //        Console.WriteLine();
        //        Console.Write("Please enter a valid non-negative number above zero: ");
        //        input = Console.ReadLine();
        //    }
        //    return result;
        //}
        ////-----------------------------------------------------------------------------------------------------------------------------------------//
        ////This method accepts a user's input as a parameter and determines if the input is a double. If not the user is prompted to enter a double
        ////A valid double is then returned
        //public static double validDouble(string input, int condition)
        //{
        //    //Stores converted double
        //    double result;

        //    //This while loop keeps executing until the user's input is a valid double
        //    while (!double.TryParse(input, out result) || result < condition)
        //    {
        //        if (condition == 1)
        //        {
        //            Console.WriteLine();
        //            Console.Write("Please enter a valid non-negative number above zero: ");
        //            input = Console.ReadLine();
        //        }
        //        else
        //        {
        //            Console.WriteLine();
        //            Console.Write("Please enter a valid non-negative number: ");
        //            input = Console.ReadLine();
        //        }
        //    }
        //    return result;
        //}
        ////------------------------------------------------------------------------------------------------------------------------------------------//
        ////This method takes a users string input and checks if it is null or contains digits. If so, the user is prompted again until they enter
        ////a valid string
        //public static string validString()
        //{
        //    while (true)
        //    {
        //        string input = Console.ReadLine();

        //        if (!string.IsNullOrWhiteSpace(input) & !input.Any(char.IsDigit))  // (StackOverflow,2019)
        //        {
        //            return input;
        //        }
        //        else
        //        {
        //            Console.WriteLine();
        //            Console.Write("Invalid input. Please enter a non-empty string without numbers: ");
        //        }
        //    }
        //}
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method prompts the user for all recipe ingredient details and all recipe steps and saves these details in Lists
        //private void inputRecipeDetails(RecipeManagerClass r)
        //{
        //    //Stores the number of ingredients in the recipe
        //    int ingredientNum;
        //    //Stores the number of steps for the recipe
        //    int steps;
        //    //Used to label ingredient and step numbers when iterating through for loops
        //    int count = 1;

        //    RecipeClass recipe = new RecipeClass();
        //    bool x = true;
        //    Console.WriteLine();
        //    borderColour("---------RECIPE DETAILS---------", ConsoleColor.Green);
        //    Console.WriteLine();

        //    Console.Write("Enter recipe name: ");
        //    recipe.recipeName = validString();
        //    Console.WriteLine();

        //    //Prompts user for number of ingredients
        //    Console.Write("Enter number of ingredients in recipe: ");
        //    //Assigns a valid number entered by user
        //    ingredientNum = validNum(Console.ReadLine());
        //    Console.WriteLine();
        //    Console.WriteLine("--------------------------------");

        //    double calorieCount = 0;
        //    for (int i = 0; i < ingredientNum; i++)
        //    {
        //        string decision;
        //        //Create a new IngredientsClass object
        //        IngredientsClass ingredient = new IngredientsClass();

        //        Console.WriteLine();
        //        //Prompts user to enter ingredient name
        //        Console.Write($"Enter ingredient {count} name: ");
        //        //Assings ingredient name to user input
        //        ingredient.ingredientName = validString();
        //        Console.WriteLine();
        //        //Prompts the user to enter ingredient quantity
        //        Console.Write($"Enter ingredient {count} quantity: ");
        //        //Assigns ingredient quantity to a valid quantity input by the user
        //        ingredient.ingredientQuantity = validDouble(Console.ReadLine(), 1);
        //        Console.WriteLine();

        //        //Stores the choice for measurement unit in variable
        //        decision = ingredient.decideUnit();

        //        //Uses unitDecision to assign the correct measurement unit to the ingredient
        //        ingredient.assignUnit(ingredient, decision);
        //        Console.WriteLine();
        //        //Showing an explanation to the user of what a food group is
        //        Console.WriteLine("****************************************************************************************");
        //        Console.WriteLine("A food group is a collection of foods that share similar nutrional properties.");
        //        Console.WriteLine("Each of the 7 food groups are essential in ensuring we have a healthy and balanced diet!");    // (Twinkl,2024)
        //        Console.WriteLine("****************************************************************************************");
        //        Console.WriteLine();

        //        //Takes a users decision and assings it as the food group 
        //        decision = ingredient.decideFoodGroup();
        //        ingredient.assignFoodGroup(ingredient, decision);

        //        //Showing an explanation to the user of what a calorie is
        //        Console.WriteLine();
        //        Console.WriteLine("***************************************************************************************************");
        //        Console.WriteLine("A calorie is a unit of energy that refer to the energy people get from food and drink they consume.");
        //        Console.WriteLine("Calories are essential for human health. The key is consuming the right amount!");  //(MedicalNewsToday,2017)
        //        Console.WriteLine("***************************************************************************************************");
        //        Console.WriteLine();

        //        Console.Write($"Enter ingredient {count} calorie count: ");
        //        ingredient.ingredientCalories = validDouble(Console.ReadLine(), 0);

        //        calorieCount += ingredient.ingredientCalories;
        //        calorieCheck(calorieCount, caloriesExceeded);
        //        //Saves all the ingredient information and adds the ingredient to the ingredient List
        //        ingredient.saveOriginal();
        //        recipe.ingredientList.Add(ingredient);

        //        Console.WriteLine();
        //        Console.WriteLine("--------------------------------");
        //        count++;
        //    }
        //    //Calls method to calculate and assing the total calories for the recipe, then displays this information to the user 
        //    recipe.recipeCalorieTotal = calculateCalorieTotal(recipe.ingredientList);
        //    displayCalorieTotal(recipe.recipeCalorieTotal);
        //    Console.WriteLine();
        //    //Prompts the user to enter the required number of steps
        //    Console.Write("Enter number of recipe steps: ");
        //    //Assigns step number to a valid number entered by user
        //    steps = validNum(Console.ReadLine());
        //    Console.WriteLine();
        //    //Resetting count so that steps can be properly numbered
        //    count = 1;

        //    for (int j = 0; j < steps; j++)
        //    {
        //        string step;
        //        //Prompts the user to enter a step
        //        Console.WriteLine($"Please type step {count} below:");
        //        //Assings step information based on the user's valid input
        //        step = validString();
        //        recipe.stepList.Add(step);
        //        Console.WriteLine();
        //        count++;
        //    }
        //    //Adds recipe to the recipe List
        //    r.addRecipe(recipe);
        //    //r.sortAlphabeticalOrder();
        //    Console.WriteLine("Recipe Created!");
        //    Console.WriteLine();
        //}
        //-----------------------------------------------------------------------------------------------------------------------------------------//
        //This method displays all stored ingredient and step information for recipe
        private void displayRecipe(RecipeManagerClass r)
        {
            Console.WriteLine();
            borderColour("---------DISPLAY RECIPE---------", ConsoleColor.DarkMagenta);
            RecipeClass selected = r.displayOrderedRecipes("display");

            //Checks if recipe exists before attempting to display
            if (selected != null)
            {
                Console.WriteLine();
                borderColour("---------FULL RECIPE---------", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Recipe name: {selected.recipeName}");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Total calories: {selected.recipeCalorieTotal}");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ingredients:");
                //Count initialized in order to number ingredients
                int count = 1;
                //Displays the ingredient information for each ingredientsClass object in the ingredient List 
                foreach (IngredientsClass i in selected.ingredientList)
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
                //Displays each string element in the step List
                foreach (string x in selected.stepList)
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
                Console.WriteLine("No Recipes Found");
                Console.WriteLine();
            }

        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method prompts the user to select a scale factor from a menu, sets the scale factor based on user choice and then passes the scale
        //factor as an argument in order to scale each ingredient 
        private void scaleRecipe(RecipeManagerClass r)
        {
            //Stores the user's choice
            string choice;
            //Stores the scale factor
            double scaleFactor;

            Console.WriteLine();
            borderColour("---------SCALE RECIPE---------", ConsoleColor.Cyan);
            RecipeClass selected = r.displayOrderedRecipes("scale");

            //Checks if a recipe exists 
            if (selected != null)
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

                //Each IngredientsClass object in the ingredient List is scaled based on the chosen scale factor 
                foreach (IngredientsClass i in selected.ingredientList)
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
        //This method resets all IngredientClass object's quantities in the ingredient List to the original values first entered by the user
        private void resetValues(RecipeManagerClass r)
        {
            Console.WriteLine();
            borderColour("---------RESET RECIPE---------", ConsoleColor.DarkYellow);
            RecipeClass selected = r.displayOrderedRecipes("reset");

            //Checks if a recipe exists 
            if (selected != null)
            {
                //Each IngredientsClass object's quantities in the ingredient List are assigned to the saved original values
                foreach (IngredientsClass i in selected.ingredientList)
                {
                    i.ingredientQuantity = i.originalQuantity;
                    i.measurementUnitName = i.originalunitName;
                    i.ingredientCalories = i.originalCalories;
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
                Console.WriteLine();
                Console.WriteLine("Recipe quantities have been reset to original values!");
                Console.WriteLine();
            }
            //Informs the user to create a recipe before resetting quantities 
            else
            {
                Console.WriteLine("No recipe found. Please create a recipe before resetting ingredient quantities!");
                Console.WriteLine();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method clears all ingredient List and step List values if a user confirms the action
        private void clearRecipe(RecipeManagerClass r)
        {
            Console.WriteLine();
            borderColour("---------CLEAR RECIPE---------", ConsoleColor.Red);
            //Console.WriteLine();
            RecipeClass selected = r.displayOrderedRecipes("clear");

            if (selected != null)
            {
                //Clears values if user confirms 
                if (confrimPrompt())
                {
                    r.removeRecipe(selected);
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
            else
            {
                Console.WriteLine();
                Console.WriteLine("No Recipe found. Please create a recipe before clearing");
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
            while (y == false)
            {
                Console.WriteLine();
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
                    decision = false;
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
        //This method accepts the calorie total as a parameter and then displays the appropraite explanation depending on the amount
        private static void displayCalorieTotal(double total)
        {
            Console.WriteLine();
            Console.WriteLine($"Total calories in recipe: {total}");
            Console.WriteLine();
            Console.WriteLine("Explanation:");
            Console.WriteLine();
            if (total <= 100)
            {
                Console.WriteLine("This recipe is very low in calories, making it a great option for a light snack or a side dish.");
                Console.WriteLine("It's ideal for those looking to reduce their caloric intake while still enjoying a flavourful meal.");
            }
            else if (total <= 300)
            {
                Console.WriteLine("This recipe falls into the low-calorie range, perfect for a light meal or a more substantial snack.");
                Console.WriteLine("It can be part of a balanced diet and is suitable for those managing their weight.");
            }
            else if (total <= 500)
            {
                Console.WriteLine("This recipe is moderately high in calories, which makes it suitable for a main meal that will keep you full and energized.");
                Console.WriteLine("It’s ideal for individuals who need a bit more energy, such as after physical activity.");
            }
            else if (total <= 700)
            {
                Console.WriteLine("This recipe is moderately high in calories, which makes it suitable for a main meal that will keep you full and energized.");
                Console.WriteLine("It’s ideal for individuals who need a bit more energy, such as after physical activity.");
            }
            else if (total <= 1000)
            {
                Console.WriteLine("This recipe is relatively high in calories and works well as a hearty main dish.");
                Console.WriteLine("It’s suitable for those with higher energy needs, such as active individuals or those looking to gain weight in a healthy manner.");
            }
            else
            {
                Console.WriteLine("This recipe is very high in calories, making it a substantial meal that is best suited for special occasions.");
                Console.WriteLine("Be mindful of portion sizes if you’re watching your calorie intake.");
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method calculates the total calories for a recipe by adding the calories of each ingredient from the recipe together
        public double calculateCalorieTotal(List<IngredientsClass> ingredients)
        {
            double total = 0;

            foreach (IngredientsClass ingredient in ingredients)
            {
                total += ingredient.ingredientCalories;
            }

            return total;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method calls the calorie alert delegate and passes the current calorie count to it
        private static void calorieCheck(double num, calorieAlert alert)
        {
            alert(num);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method matches the signature of the delegate and displays a calorie warning if the calories exceed 300
        private static void caloriesExceeded(double num)
        {
            if (num > 300)
            {
                Console.WriteLine();
                borderColour("******************************************************", ConsoleColor.Red);
                borderColour("Total calories of recipe exceed 300!", ConsoleColor.Red);
                borderColour("This recipe no longer falls into the low-calorie range", ConsoleColor.Red);
                borderColour("It is therefore more suitable for a main meal ", ConsoleColor.Red);
                borderColour("******************************************************", ConsoleColor.Red);
            }
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//
