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
        public double measurementUnitValue { get; set; }

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
                    i.measurementUnitValue = i.ingredientQuantity * 5;
                    break;

                case "2":
                    i.measurementUnitName = "tbsp";
                    i.measurementUnitValue = i.ingredientQuantity * 15;
                    break;

                case "3":
                    i.measurementUnitName = "C";
                    i.measurementUnitValue = i.ingredientQuantity * 250;
                    break;

                case "4":
                    i.measurementUnitName = "g";
                    i.measurementUnitValue = i.ingredientQuantity * 1;
                    break;

                case "5":
                    i.measurementUnitName = "kg";
                    i.measurementUnitValue = i.ingredientQuantity * 1000;
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
    }
}
