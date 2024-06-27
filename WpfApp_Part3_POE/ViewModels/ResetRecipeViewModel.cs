using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using WpfApp_Part3_POE.Classes;
using Microsoft.VisualBasic;
using System.IO;
using System.Windows;


namespace WpfApp_Part3_POE.ViewModels
{
    public class ResetRecipeViewModel : INotifyPropertyChanged
    {
        private RecipeManagerClass _recipeManager;
        private RecipeClass selectedRecipe;
        private ObservableCollection<RecipeClass> filteredRecipes;
        private string selectedFilter;
        private string filterName;
        private string recipeDetails;

        public ResetRecipeViewModel(RecipeManagerClass recipeManager)
        {
            _recipeManager = recipeManager;
            FilterOptions = new ObservableCollection<string> { "None", "Ingredient", "Food Group", "Max Calories" };
            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
            SelectedFilter = "None"; // Set initial selection to "None"
        }

        public ObservableCollection<string> FilterOptions { get; }

        public string SelectedFilter
        {
            get => selectedFilter;
            set
            {
                selectedFilter = value;
                OnPropertyChanged(nameof(SelectedFilter));
            }
        }

        public ObservableCollection<RecipeClass> FilteredRecipes
        {
            get => filteredRecipes;
            set
            {
                filteredRecipes = value;
                OnPropertyChanged(nameof(FilteredRecipes));
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

        public string RecipeDetails
        {
            get => recipeDetails;
            set
            {
                recipeDetails = value;
                OnPropertyChanged(nameof(RecipeDetails));
            }
        }

        public ICommand ConfirmFilterCommand => new RelayCommand(ConfirmFilter);
        public ICommand ResetRecipeCommand => new RelayCommand(ResetRecipe);

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

        private void ResetRecipe(object parameter)
        {
            if (SelectedRecipe != null)
            {
                double calories = 0;
                foreach (var ingredient in SelectedRecipe.ingredientList)
                {
                    ingredient.ingredientQuantity = ingredient.originalQuantity;
                    ingredient.measurementUnitName = ingredient.originalunitName;
                    ingredient.ingredientCalories = ingredient.originalCalories;

                    calories += ingredient.ingredientCalories;

                    if (ingredient.measurementUnitGrams == 0)
                    {
                        ingredient.measurementUnitMl = ingredient.originalUnitMl;
                    }
                    else
                    {
                        ingredient.measurementUnitGrams = ingredient.originalGrams;
                    }
                }
                SelectedRecipe.recipeCalorieTotal = calories;
                MessageBox.Show("Recipe quantities have been reset to original values!");
            }
            else
            {
                MessageBox.Show("Please select a recipe to reset.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshRecipeList()
        {
            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
        }
    }
}
