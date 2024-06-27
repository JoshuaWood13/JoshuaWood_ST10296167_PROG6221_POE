// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

// References: 
// Twinkl. 2024. What are the Food Groups?. Avaialble at: https://www.twinkl.co.za/teaching-wiki/food-groups#:~:text=Well%2C%20each%20of%20the%205,a%20healthy%20and%20balanced%20diet.
// StackOverflow. 2019. Checking whether string is empty, contains int or is an integer. Available at: https://stackoverflow.com/questions/59438107/checking-whether-string-is-empty-contains-int-or-is-an-integer
// MedicalNewsToday. 2017. Calories: Requirements, health needs, and function. Available at: https://www.medicalnewstoday.com/articles/263028

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
        public delegate string calorieAlert(double total);

        //Constructor
        public RecipeClass()
        {
            ingredientList = new List<IngredientsClass>();
            stepList = new List<string>();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method uses a stringbuilder to construct the layout of displaying recipe details and returns this string
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

        //-----------------------------------------------------------------------------------------------------------------------------------------//
        //This method accepts a user's input as a parameter and determines if the input is a valid double.
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
        //This method takes a users string input and checks if it is null or contains digits. 
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
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public static string DisplayCalorieTotal(double total, calorieAlert alert)  // (MedicalNewsToday, 2017).
        {
            StringBuilder message = new StringBuilder();
            if(total > 300)
            {
                message.AppendLine();
                message.AppendLine(alert(total));
            }
            message.AppendLine();
            message.AppendLine($"Total calories in recipe: {total}");
            message.AppendLine();
            message.AppendLine("Explanation:");
            message.AppendLine();
            if (total <= 100)
            {
                message.AppendLine("This recipe is very low in calories, making it a great option for a light snack or a side dish. " +
                    "It's ideal for those looking to reduce their caloric intake while still enjoying a flavorful meal.");
            }
            else if (total <= 300)
            {
                message.AppendLine("This recipe falls into the low-calorie range, perfect for a light meal or a more substantial snack. " +
                    "It can be part of a balanced diet and is suitable for those managing their weight.");
            }
            else if (total <= 500)
            {
                message.AppendLine("This recipe is moderately high in calories, which makes it suitable for a main meal that will keep you full and energized. " +
                    "It’s ideal for individuals who need a bit more energy, such as after physical activity.");
            }
            else if (total <= 700)
            {
                message.AppendLine("This recipe is moderately high in calories, which makes it suitable for a main meal that will keep you full and energized. " +
                    "It’s ideal for individuals who need a bit more energy, such as after physical activity.");
            }
            else if (total <= 1000)
            {
                message.AppendLine("This recipe is relatively high in calories and works well as a hearty main dish. It’s suitable for those with higher energy needs," +
                    " such as active individuals or those looking to gain weight in a healthy manner.");
            }
            else
            {
                message.AppendLine("This recipe is very high in calories, making it a substantial meal that is best suited for special occasions. Be mindful of portion sizes if you’re watching your calorie intake.");
            }
            return message.ToString();
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
        //This method matches the signature of the delegate and displays a calorie warning if the calories exceed 300
        public static string caloriesExceeded(double total)
        {
            return "Alert: total calories of recipe exceed 300!";
        }
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//
