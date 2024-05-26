// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

// References: 

using JoshuaWood_ST10296167_PROG6221_POE.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaWood_ST10296167_PROG6221_POE
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WorkerClass worker = new WorkerClass();
            worker.run();
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//
//Console.Write("Enter recipe name: ");
//recipe.recipeName = Console.ReadLine();    //add error handling
//Console.WriteLine();

////Prompts user for number of ingredients
//Console.Write("Enter number of ingredients in recipe: ");
////Assigns a valid number entered by user
//ingredientNum = validNum(Console.ReadLine());
//Console.WriteLine();
//Console.WriteLine("--------------------------------");

////Initializing an ingredient array and setting its size to the user's input
////ingredientArray = new IngredientsClass[ingredientNum];
////This for loop iterates over each element of the ingredient array
//for (int i = 0; i < ingredientNum; i++)
//{
//    string decision;
//    //Create a new IngredientsClass object
//    //ingredientArray[i] = new IngredientsClass();

//    IngredientsClass ingredient = new IngredientsClass();

//    //Prompts user to enter ingredient name
//    Console.Write($"Enter ingredient {count} name: ");
//    //Assings ingredient name to user input
//    //ingredientArray[i].ingredientName = Console.ReadLine();

//    ingredient.ingredientName = Console.ReadLine();

//    //Prompts the user to enter ingredient quantity
//    Console.Write($"Enter ingredient {count} quantity: ");
//    //Assigns ingredient quantity to a valid quantity input by the user
//    //ingredientArray[i].ingredientQuantity = validQuantity(Console.ReadLine());

//    ingredient.ingredientQuantity = validDouble(Console.ReadLine());

//    //Console.WriteLine();

//    Console.Write($"Enter ingredient {count} calorie count: ");
//    ingredient.ingredientCalories = validDouble(Console.ReadLine());
//    recipe.recipeCalorieTotal += ingredient.ingredientCalories;
//    Console.WriteLine();

//    //Stores the choice for measurement unit in variable
//    //unitDecision = ingredientArray[i].decideUnit();

//    decision = ingredient.decideUnit();

//    //Uses unitDecision to assign the correct measurement unit to the ingredient
//    //ingredientArray[i].assignUnit(ingredientArray[i], unitDecision);

//    ingredient.assignUnit(ingredient, decision);
//    Console.WriteLine();

//    decision = ingredient.decideFoodGroup();
//    ingredient.assignFoodGroup(ingredient, decision);

//    //Saves all the ingredient information 
//    //ingredientArray[i].saveOriginal();

//    ingredient.saveOriginal();
//    recipe.ingredientList.Add(ingredient);

//    Console.WriteLine();
//    Console.WriteLine("--------------------------------");
//    count++;
//}
//displayCalorieTotal(recipe.ingredientList, recipe, caloriesExceeded);
//Console.WriteLine();
////Prompts the user to enter the required number of steps
//Console.Write("Enter number of recipe steps: ");
////Assigns step number to a valid number entered by user
//steps = validNum(Console.ReadLine());
//Console.WriteLine();
////Initializing step array and setting its size to the number chosen by user
////stepArray = new string[steps];
////Resetting count so that steps can be properly numbered
//count = 1;
////This for loop iterates over every element of the step array
//for (int j = 0; j < steps; j++)
//{
//    string step;
//    //Prompts the user to enter a step
//    Console.WriteLine($"Please type step {count} below:");
//    //Assings array element to the user's input
//    //stepArray[j] = Console.ReadLine();

//    step = Console.ReadLine();
//    recipe.stepList.Add(step);

//    Console.WriteLine();
//    count++;
//}

//r.addRecipe(recipe);
//r.sortAlphabeticalOrder();