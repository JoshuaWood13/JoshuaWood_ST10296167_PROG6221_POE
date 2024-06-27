﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;
using WpfApp_Part3_POE.Classes;
using System.IO;

namespace WpfApp_Part3_POE.ViewModels
{
    public class ScaleRecipeViewModel : INotifyPropertyChanged
    {
        private RecipeManagerClass _recipeManager;
        private RecipeClass selectedRecipe;
        private ObservableCollection<RecipeClass> filteredRecipes;
        private string selectedFilter;
        private double? selectedScalingFactor;

        public ScaleRecipeViewModel(RecipeManagerClass recipeManager)
        {
            _recipeManager = recipeManager;
            FilterOptions = new ObservableCollection<string> { "None", "Ingredient", "Food Group", "Max Calories" };
            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
            ScalingFactors = new ObservableCollection<double> { 0.5, 2, 3 };
            SelectedFilter = "None"; // Set initial selection to "None"
        }

        public ObservableCollection<string> FilterOptions { get; }
        public ObservableCollection<RecipeClass> FilteredRecipes
        {
            get => filteredRecipes;
            set
            {
                filteredRecipes = value;
                OnPropertyChanged(nameof(FilteredRecipes));
            }
        }

        public ObservableCollection<double> ScalingFactors { get; }
        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
            }
        }

        public RecipeClass SelectedRecipe
        {
            get => selectedRecipe;
            set
            {
                selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }

        public double? SelectedScalingFactor
        {
            get => selectedScalingFactor;
            set
            {
                selectedScalingFactor = value;
                OnPropertyChanged(nameof(SelectedScalingFactor));
            }
        }

        public ICommand ConfirmFilterCommand => new RelayCommand(ConfirmFilter);
        public ICommand ScaleRecipeCommand => new RelayCommand(ScaleRecipe);

        private void ConfirmFilter(object parameter)
        {
            string filterValue = string.Empty;
            SelectedRecipe = null;

            switch (SelectedFilter)
            {
                case "None":
                    FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                    break;
                case "Ingredient":
                    filterValue = PromptUserForInput("Please enter the ingredient to filter recipes by:");
                    bool ingredientFound = false;
                    if (!string.IsNullOrEmpty(filterValue))
                    {
                        RecipeManagerClass filteredList = new RecipeManagerClass();
                        foreach (RecipeClass recipe in _recipeManager.recipeList)
                        {
                            foreach (IngredientsClass ingredient in recipe.ingredientList)
                            {
                                if (filterValue.Equals(ingredient.ingredientName, StringComparison.OrdinalIgnoreCase))
                                {
                                    ingredientFound = true;
                                    break;
                                }
                            }

                            if (ingredientFound)
                            {
                                filteredList.addRecipe(recipe);
                                ingredientFound = false;
                            }
                        }
                        if (filteredList.recipeList.Count == 0)
                        {
                            MessageBox.Show($"No recipes found for ingredient: {filterValue}");
                            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                        }
                        else
                        {
                            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(filteredList.recipeList));
                        }
                    }
                    else
                    {
                        FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                    }
                    break;
                case "Food Group":
                    filterValue = PromptUserForFoodGroup();
                    bool foodGroupFound = false;
                    if (!string.IsNullOrEmpty(filterValue))
                    {
                        RecipeManagerClass filteredList = new RecipeManagerClass();
                        foreach (RecipeClass recipe in _recipeManager.recipeList)
                        {
                            foreach (IngredientsClass ingredient in recipe.ingredientList)
                            {
                                if (filterValue.Equals(ingredient.ingredientFoodGroup, StringComparison.OrdinalIgnoreCase))
                                {
                                    foodGroupFound = true;
                                    break;
                                }
                            }

                            if (foodGroupFound)
                            {
                                filteredList.addRecipe(recipe);
                                foodGroupFound = false;
                            }
                        }
                        if (filteredList.recipeList.Count == 0)
                        {
                            MessageBox.Show($"No recipes found for food group: {filterValue}");
                            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                        }
                        else
                        {
                            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(filteredList.recipeList));
                        }
                    }
                    else
                    {
                        FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                    }
                    break;
                case "Max Calories":
                    filterValue = PromptUserForInput("Enter the maximum number of calories:");
                    if (double.TryParse(filterValue, out double maxCalories))
                    {
                        RecipeManagerClass filteredList = new RecipeManagerClass();
                        foreach (RecipeClass recipe in _recipeManager.recipeList)
                        {
                            if (recipe.recipeCalorieTotal <= maxCalories)
                            {
                                filteredList.addRecipe(recipe);
                            }
                        }
                        if (filteredList.recipeList.Count == 0)
                        {
                            MessageBox.Show($"No recipes found for max calories: {filterValue}");
                            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                        }
                        else
                        {
                            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(filteredList.recipeList));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid number for calories.");
                        FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                    }
                    break;
                default:
                    MessageBox.Show("Please select a filter option to filter recipes.");
                    FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                    break;
            }

        }

        private string PromptUserForFoodGroup()
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Select a food group by entering the corresponding number:\n" +
                "1) Starchy foods\n" +
                "2) Vegetables and fruits\n" +
                "3) Dry beans, peas, lentils and soya\n" +
                "4) Chicken, fish, meat and eggs\n" +
                "5) Milk and dairy\n" +
                "6) Fats and oil\n" +
                "7) Water",
                "Food Group Selection",
                "");

            return SelectedFoodGroup(input);
        }

        private string SelectedFoodGroup(string input)
        {
            switch (input)
            {
                case "1":
                    return "Starchy foods";
                case "2":
                    return "Vegetables and fruits";
                case "3":
                    return "Dry beans, peas, lentils and soya";
                case "4":
                    return "Chicken, fish, meat and eggs";
                case "5":
                    return "Milk and dairy";
                case "6":
                    return "Fats and oil";
                case "7":
                    return "Water";
                default:
                    MessageBox.Show("Invalid selection. Please enter a number between 1 and 7.");
                    return null;
            }
        }

        private string PromptUserForInput(string message)
        {
            return Interaction.InputBox(message, "Filter Input", "");
        }

        private void ScaleRecipe(object parameter)
        {
            if (SelectedRecipe == null)
            {
                MessageBox.Show("Please select a recipe to scale.");
                return;
            }

            if (!SelectedScalingFactor.HasValue)
            {
                MessageBox.Show("Please select a valid scaling factor.");
                return;
            }

            foreach(var ingredient in SelectedRecipe.ingredientList)
            {
                ingredient.scaleIngredients(ingredient, SelectedScalingFactor.Value);
            }

            SelectedRecipe.recipeCalorieTotal *= SelectedScalingFactor.Value;
            MessageBox.Show($"Recipe '{SelectedRecipe.recipeName}' has been scaled by {SelectedScalingFactor}x.");
        }

        public void RefreshRecipeList()
        {
            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
        }

        public void ClearSelections()
        {
            SelectedFilter = "None";
            SelectedRecipe = null;
            SelectedScalingFactor = null; // Clear scaling factor selection if applicable
            //OnPropertyChanged(nameof(SelectedFilter));
            //OnPropertyChanged(nameof(SelectedRecipe));
            //OnPropertyChanged(nameof(SelectedScalingFactor));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
