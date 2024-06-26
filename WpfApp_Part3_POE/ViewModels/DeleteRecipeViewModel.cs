using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualBasic;
using WpfApp_Part3_POE.Classes;

namespace WpfApp_Part3_POE.ViewModels
{
    public class DeleteRecipeViewModel : INotifyPropertyChanged
    {
        private RecipeManagerClass _recipeManager;
        private RecipeClass selectedRecipe;
        private ObservableCollection<RecipeClass> filteredRecipes;
        private string selectedFilter;

        public DeleteRecipeViewModel(RecipeManagerClass recipeManager)
        {
            _recipeManager = recipeManager;
            FilterOptions = new ObservableCollection<string> { "None", "Ingredient", "Food Group", "Max Calories" };
            FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
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

        public ICommand ConfirmFilterCommand => new RelayCommand(ConfirmFilter);
        public ICommand DeleteRecipeCommand => new RelayCommand(DeleteRecipe);

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
                    filterValue = PromptUserForInput("Please enter the food group to filter recipes by:");
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

        private void DeleteRecipe(object parameter)
        {
            if (SelectedRecipe != null)
            {
                // Prompt the user for confirmation before deleting
                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the recipe '{SelectedRecipe.recipeName}'?", "Delete Recipe", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    _recipeManager.recipeList.Remove(SelectedRecipe);
                    MessageBox.Show("Recipe deleted successfully!");
                    // Refresh the filtered recipes list
                    FilteredRecipes = new ObservableCollection<RecipeClass>(_recipeManager.getOrderedRecipes(_recipeManager.recipeList));
                }
            }
            else
            {
                MessageBox.Show("Please select a recipe to delete.");
            }
        }

        private string PromptUserForInput(string message)
        {
            return Interaction.InputBox(message, "Filter Input", "");
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
