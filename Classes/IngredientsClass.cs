using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaWood_ST10296167_PROG6221_POE.Classes
{
    public class IngredientsClass
    {
        public string ingredientName {  get; set; }
        public double ingredientQuantity { get; set; }
        public string measurementUnitName { get; set; }
        public double measurementUnitMl { get; set; }
        public double measurementUnitGrams { get; set; }
        public double originalQuantity { get; set; }
        public double originalUnitMl { get; set; }
        public double originalGrams { get; set; }
        public string originalunitName { get; set; }

        //------------------------------------------------------------------------------------------------------------------------------------------//
        public string decideUnit()
        {
            string choice;

            Console.WriteLine("Please select the unit of measurement:");
            Console.WriteLine("1) Teaspoons (tsp) ");
            Console.WriteLine("2) Tablespoons (tbsp)");
            Console.WriteLine("3) Cups (C)");
            Console.WriteLine("4) Grams (g)");
            Console.WriteLine("5) Kilograms (kg)");
            Console.WriteLine();

            do
            {
                Console.Write("Enter choice: ");
                choice = Console.ReadLine();

                if (!validChoice(choice))
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a number between 1 and 5!");
                    Console.WriteLine();
                }
            }while(!validChoice(choice));

            return choice;
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        public void assignUnit(IngredientsClass i, string input)
        {
            switch(input)
            {
                case "1":
                    i.measurementUnitName = "tsp";
                    i.measurementUnitMl = i.ingredientQuantity * 5;
                    break;

                case "2":
                    i.measurementUnitName = "Tbsp";
                    i.measurementUnitMl = i.ingredientQuantity * 15;
                    break;

                case "3":
                    i.measurementUnitName = "C";
                    i.measurementUnitMl = i.ingredientQuantity * 240;
                    break;

                case "4":
                    i.measurementUnitName = "g";
                    i.measurementUnitGrams = i.ingredientQuantity;
                    break;

                case "5":
                    i.measurementUnitName = "kg";
                    i.measurementUnitGrams = i.ingredientQuantity * 1000;
                    break;
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private static bool validChoice(string choice)
        {
            int num;
            bool valid = int.TryParse(choice, out num);
            return valid && num >= 1 && num <= 5;
        }
//-----------------------------------------------------------------------------------------------------------------------------------------//
        public void scaleIngredients(IngredientsClass i, double scaling)
        {
            i.ingredientQuantity *= scaling;

            if (i.measurementUnitGrams == 0)
            {
                i.measurementUnitMl *= scaling;

                if(i.measurementUnitMl < 15)
                {
                    i.measurementUnitName = "tsp";
                    i.ingredientQuantity /= 5;
                }
                else if(i.measurementUnitMl < 240)
                {
                    i.measurementUnitName = "Tbsp";
                    i.ingredientQuantity = i.measurementUnitMl / 15;
                }
                else
                {
                    i.measurementUnitName = "C";
                    i.ingredientQuantity = i.measurementUnitMl / 240;
                }
            }
            else
            {
                i.measurementUnitGrams *= scaling;

                if (i.measurementUnitGrams > 1000)
                {
                    i.measurementUnitName = "kg";
                    i.ingredientQuantity /= 1000;
                }
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        public void saveOriginal()
        {
            originalQuantity = ingredientQuantity;
            originalunitName = measurementUnitName;
            if(measurementUnitGrams == 0)
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
