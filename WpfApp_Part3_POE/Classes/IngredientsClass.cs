// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

// References: 
// Exploratorium. 2024. Measurement Equivalents. Available at: https://www.exploratorium.edu/food/measurements
// SweetLife. 2022. What are the different food groups? A simple explanation. Available at: https://sweetlife.org.za/what-are-the-different-food-groups-a-simple-explanation/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Part3_POE.Classes
{
    public class IngredientsClass
    {
        //Declaring the variables that will be used to get and set all ingredient details
        public string ingredientName { get; set; } //Stores ingredient name
        public double ingredientQuantity { get; set; } //Stores ingredient quantity
        public double ingredientCalories { get; set; } //Stores ingredient calories
        public string ingredientFoodGroup { get; set; } //Stores ingredient food group
        public string measurementUnitName { get; set; } //Stores measurement unit name assigned to ingredient
        public double measurementUnitMl { get; set; } //Stores the quantity of ingredient in millilitres 
        public double measurementUnitGrams { get; set; } //Stores the quantity of ingredient in grams
        public double originalQuantity { get; set; } //Stores the original quantity assigned to ingredient
        public double originalCalories { get; set; } //Stores the original calorie count assigned to ingredient
        public double originalUnitMl { get; set; } //Stores the original millilitre amount of ingredient
        public double originalGrams { get; set; } //Stores the original gram amount of ingredient
        public string originalunitName { get; set; } //Stores the original measurement unit assigned to ingredient

        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method takes a user's valid choice as the input parameter, to assign the correct measurement unit name and value to the ingredientClass object i parameter
        public void assignUnit(IngredientsClass i, string selection)
        {
            switch (selection)
            {
                //Assigns a ml or gram equivalent amount for ingredient quantity that will be used to accurately scale values later
                case "Teaspoons (tsp)":
                    i.measurementUnitName = "tsp";
                    i.measurementUnitMl = i.ingredientQuantity * 5;  // 1 tsp = 5ml (Exploratorium, 2024)
                    break;

                case "Tablespoons (tbsp)":
                    i.measurementUnitName = "Tbsp";
                    i.measurementUnitMl = i.ingredientQuantity * 15;  // 1 Tbsp = 15ml (Exploratorium, 2024)
                    break;

                case "Cups (C)":
                    i.measurementUnitName = "C";
                    i.measurementUnitMl = i.ingredientQuantity * 240;  // 1 Cup = 240ml (Exploratorium, 2024)
                    break;

                case "Grams (g)":
                    i.measurementUnitName = "g";
                    i.measurementUnitGrams = i.ingredientQuantity;
                    break;

                case "Kilograms (kg)":
                    i.measurementUnitName = "kg";
                    i.measurementUnitGrams = i.ingredientQuantity * 1000;  // 1 kg = 1000 grams 
                    break;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method takes a user's valid choice as the input parameter, to assign the correct measurement unit name and value to the ingredientClass object i parameter
        public void assignFoodGroup(IngredientsClass i, string input)  // (SweetLife,2022)
        {
            switch (input)
            {
                //Assigns a ml or gram equivalent amount for ingredient quantity that will be used to accurately scale values later
                case "1":
                    i.ingredientFoodGroup = "Starchy foods";
                    break;

                case "2":
                    i.ingredientFoodGroup = "Vegetables and fruits";
                    break;

                case "3":
                    i.ingredientFoodGroup = "Dry beans, peas, lentils and soya";
                    break;

                case "4":
                    i.ingredientFoodGroup = "Chicken, fish, meat and eggs";
                    break;

                case "5":
                    i.ingredientFoodGroup = "Milk and dairy";
                    break;

                case "6":
                    i.ingredientFoodGroup = "Fats and oil";
                    break;

                case "7":
                    i.ingredientFoodGroup = "Water";
                    break;
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------------------------//
        //This method scales the ingredient i based off the scaling factor from the scaling parameter decided by user. The ml and gram values
        //are then used to determine the most appropriate measurement unit to use for the scaled ingredient quantity.
        public void scaleIngredients(IngredientsClass i, double scaling)
        {
            //Scales ingredient quantity by the scale factor (used for gram values)
            i.ingredientQuantity *= scaling;

            i.ingredientCalories *= scaling;

            //This if statement checks if there is no ingredient value for grams which would mean the current ingredient value is in ml
            if (i.measurementUnitGrams == 0)
            {
                //Scales the ml value for the ingredient by the scaling factor 
                i.measurementUnitMl *= scaling;

                //If the scaled ml value is less than 15ml (1 Tbsp) then the most appropriate measurement unit to use would be tsp
                if (i.measurementUnitMl < 15)
                {
                    //Assigns the tsp measurement unit name for current scaled value
                    i.measurementUnitName = "tsp";
                    //Calculates the scaled tsp quantity by dividing the scaled ml value by the ml value of a tsp (5)
                    i.ingredientQuantity = i.measurementUnitMl / 5;
                }
                //If the scaled ml value is less than 240ml (1 Cup) then the most appropriate measurement unit to use would be Tbsp
                else if (i.measurementUnitMl < 240)
                {
                    //Assigns the Tbsp measurement unit name for current scaled value
                    i.measurementUnitName = "Tbsp";
                    //Calculates the scaled tsp quantity by dividing the scaled ml value by the ml value of a Tbsp (15)
                    i.ingredientQuantity = i.measurementUnitMl / 15;
                }
                else
                {
                    //Assigns the C measurement unit name for current scaled value
                    i.measurementUnitName = "C";
                    //Calculates the scaled tsp quantity by dividing the scaled ml value by the ml value of a C (240)
                    i.ingredientQuantity = i.measurementUnitMl / 240;
                }
            }
            //If there is an ingredient value for grams then there is none for ml. So therefore scale and convert for grams
            else
            {
                //Scales the g value for the ingredient by the scaling factor
                i.measurementUnitGrams *= scaling;

                //If the g amount is above 1000 the the best measurement unit to use would be kg
                if (i.measurementUnitGrams > 1000)
                {
                    //Assigns the kg measurement unit name for current scaled value
                    i.measurementUnitName = "kg";
                    //Calculates the scaled kg quantity by dividing the scaled g value by the g value of a kg (1000)
                    i.ingredientQuantity = i.measurementUnitGrams / 1000;
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        //This method saves all the original ingredient values entered for one ingredient object in seperate variables  
        public void saveOriginal()
        {
            originalQuantity = ingredientQuantity;
            originalunitName = measurementUnitName;
            originalCalories = ingredientCalories;

            //This if else statement determines if the ingredient value is in ml or g and then stores the correct ml/g value in a seperate variable
            if (measurementUnitGrams == 0)
            {
                originalUnitMl = measurementUnitMl;
            }
            else
            {
                originalGrams = measurementUnitGrams;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//

    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//

