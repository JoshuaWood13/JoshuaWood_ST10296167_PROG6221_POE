// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using JoshuaWood_ST10296167_PROG6221_POE.Classes;

namespace UnitTests_POE
{
    [TestClass]
    public class RecipeClassUnitTests
    {
        //------------------------------------------------------------------------------------------------------------------------------------------//
        [TestMethod]
        public void calculateCalorieTotal_ShouldReturnCorrectSum_WhenGivenIngredientList()
        {
            //Arrange
            var test = new RecipeClass();
            IngredientsClass ingredient1 = new IngredientsClass();
            IngredientsClass ingredient2 = new IngredientsClass();
            IngredientsClass ingredient3 = new IngredientsClass();

            ingredient1.ingredientCalories = 100;
            ingredient2.ingredientCalories = 50;
            ingredient3.ingredientCalories = 30;

            test.ingredientList.Add(ingredient1);
            test.ingredientList.Add(ingredient2);
            test.ingredientList.Add(ingredient3);

            //Act
            double result = test.calculateCalorieTotal(test.ingredientList);

            //Assert
            Assert.AreEqual(180, result);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        [TestMethod]
        public void calculateCalorieTotal_ShouldReturnZero_WhenGivenEmptyIngredientList()
        {
            //Arrange
            var test = new RecipeClass();

            //Act
            double result = test.calculateCalorieTotal(test.ingredientList);

            //Assert
            Assert.AreEqual(0, result);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        [TestMethod]
        public void calculateCalorieTotal_ShouldReturnCorrectSum_WhenGivenSingleIngredient()
        {
            //Arrange
            var test = new RecipeClass();
            IngredientsClass ingredient = new IngredientsClass();

            ingredient.ingredientCalories = 500;

            test.ingredientList.Add(ingredient);

            //Act
            double result = test.calculateCalorieTotal(test.ingredientList);

            //Assert
            Assert.AreEqual(500, result);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        [TestMethod]
        public void calculateCalorieTotal_ShoulReturnCorrectSum_WhenGivenDuplicateIngredients()
        {
            //Arrange
            var test = new RecipeClass();
            IngredientsClass ingredient = new IngredientsClass();

            ingredient.ingredientCalories = 3;

            test.ingredientList.Add(ingredient);
            test.ingredientList.Add(ingredient);
            test.ingredientList.Add(ingredient);

            //Act
            double result = test.calculateCalorieTotal(test.ingredientList);

            //Assert
            Assert.AreEqual(9, result);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        [TestMethod]
        public void calculateCalorieTotal_ShouldReturnCorrectSum_WhenGivenMixedZeroAndPositiveCalories()
        {
            //Arrange
            var test = new RecipeClass();
            IngredientsClass ingredient1 = new IngredientsClass();
            IngredientsClass ingredient2 = new IngredientsClass();
            IngredientsClass ingredient3 = new IngredientsClass();

            ingredient1.ingredientCalories = 100;
            ingredient2.ingredientCalories = 0;
            ingredient3.ingredientCalories = 100;

            test.ingredientList.Add(ingredient1);
            test.ingredientList.Add(ingredient2);
            test.ingredientList.Add(ingredient3);

            //Act
            double result = test.calculateCalorieTotal(test.ingredientList);

            //Assert
            Assert.AreEqual(200, result);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
