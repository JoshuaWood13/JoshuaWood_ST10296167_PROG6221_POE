using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp_POE_Part3.Classes;

namespace WpfApp_POE_Part3
{
    /// <summary>
    /// Interaction logic for CreateRecipeUserControl.xaml
    /// </summary>
    public partial class CreateRecipeUserControl : UserControl
    {
        private RecipeClass recipe;
        private int ingredientIndex;
        public int numOfIngredients;
        private int stepIndex;
        public int numOfSteps;
        private RecipeManagerClass recipeList;

        public CreateRecipeUserControl(RecipeManagerClass recipeList)
        {
            InitializeComponent();
            recipe = new RecipeClass();
            this.recipeList = recipeList;
        }

//------------------------------------------------------------------------------------------------------------------------------------------//
        public bool validNum(string input)
        {
            //Stores converted integer
            int result;

            if(int.TryParse(input, out result) && result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        public bool validString(string input)
        {
            if (!string.IsNullOrWhiteSpace(input) & !input.Any(char.IsDigit))  // (StackOverflow,2019)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        public bool validDouble(string input, int condition)
        {
            double result;

            //This while loop keeps executing until the user's input is a valid double
            if (double.TryParse(input, out result) && result > condition)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void ConfirmIngredientCount_Click(object sender, RoutedEventArgs e)
        {
            string ingredientNum = IngredientCountTextBox.Text;
            string recipeName = RecipeNameTextBox.Text;

            if (validNum(ingredientNum) && validString(recipeName))
            {
                InputBorder.Visibility = Visibility.Visible;
                InputPanel.Visibility = Visibility.Visible;
                AddIngredientButton.Visibility = Visibility.Visible;

                // Disable the input fields
                RecipeNameTextBox.IsEnabled = false;
                IngredientCountTextBox.IsEnabled = false;
                ConfirmCountButton.IsEnabled = false;

                InputPanel.Children.Clear();

                ingredientIndex = 1;
                numOfIngredients = int.Parse(ingredientNum);

                addIngredientInput();

                //for(int i = 0; i < numOfIngredients; i++)
                //{
                //    addIngredientInput();
                //    ingredientIndex++;
                //}
            }
            else
            {
                MessageBox.Show("Please enter a valid recipe name and number of ingredients.");
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private TextBox FindTextBox(string name)
        {
            return FindElement<TextBox>(name);
        }

        private ComboBox FindComboBox(string name)
        {
            return FindElement<ComboBox>(name);
        }

        private T FindElement<T>(string name) where T : FrameworkElement //idk bout dis one
        {
            return InputPanel.FindName(name) as T;
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void AddRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            recipe.recipeName = RecipeNameTextBox.Text;
            recipeList.addRecipe(recipe);
            MessageBox.Show("Recipe Succesfully Added!");
            // Switch to Home tab
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.SwitchToHomeTab();
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {

            string name = FindTextBox($"IngredientNameTextBox_{ingredientIndex}").Text;
            string quantity = FindTextBox($"IngredientQuantityTextBox_{ingredientIndex}").Text;
            string unit = FindComboBox($"IngredientUnitComboBox_{ingredientIndex}").Text;
            string foodGroup = FindComboBox($"FoodGroupComboBox_{ingredientIndex}").Text;
            string calories = FindTextBox($"CaloriesTextBox_{ingredientIndex}").Text;

            if (validString(name) && validDouble(quantity,0) && validDouble(calories,0))
            {
                int unitIndex = FindComboBox($"IngredientUnitComboBox_{ingredientIndex}").SelectedIndex + 1;
                int foodGroupIndex = FindComboBox($"FoodGroupComboBox_{ingredientIndex}").SelectedIndex + 1;

                IngredientsClass ingredient = new IngredientsClass();
                ingredient.ingredientName = name;
                ingredient.ingredientQuantity = double.Parse(quantity);
                ingredient.assignUnit(ingredient,unitIndex.ToString());
                ingredient.assignFoodGroup(ingredient,foodGroupIndex.ToString());
                ingredient.ingredientCalories = double.Parse(calories);
                ingredient.saveOriginal();

                recipe.ingredientList.Add(ingredient);

                // Increase ingredient index
                ingredientIndex++;

                // Clear old values and add new ingredient input
                InputPanel.Children.Clear();
                if (ingredientIndex <= numOfIngredients)
                {
                    addIngredientInput();
                }
                else
                {
                    AddIngredientButton.Visibility = Visibility.Collapsed;
                    addStepCountInput();
                }
            }
            else
            {
                MessageBox.Show("fill in everything blud");
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void AddStepButton_Click(object sender, EventArgs e)
        {
            string step = FindTextBox($"StepDescriptionTextBox_{stepIndex}").Text;

            if (!string.IsNullOrWhiteSpace(step))
            {
                recipe.stepList.Add(step);

                stepIndex++;

                InputPanel.Children.Clear();
                if(stepIndex <= numOfSteps)
                {
                    addStepInput();
                }
                else
                {
                    InputBorder.Visibility = Visibility.Collapsed;
                    AddStepButton.Visibility = Visibility.Collapsed;
                    AddRecipeButton.Visibility = Visibility.Visible;
                }
            }
            else
            {
                MessageBox.Show("Please enter a step");
            }
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void ConfirmStepCountButton_Click(Object sender, EventArgs e)
        {
            string stepNum = FindTextBox("StepCountTextBox").Text;

            if (validNum(stepNum))
            {
                AddStepButton.Visibility = Visibility.Visible;

                stepIndex = 1;
                numOfSteps = int.Parse(stepNum);

                InputPanel.Children.Clear();

                addStepInput();
            }
            else
            {
                MessageBox.Show("Please enter a valid number of steps.");
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        private void addIngredientInput()
        {
            StackPanel ingredientPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 10, 0, 0), HorizontalAlignment = HorizontalAlignment.Center };

            // Ingredient Name
            Label nameLabel = new Label { Content = $"Ingredient {ingredientIndex} Name:", Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            TextBox nameTextBox = new TextBox { Name = $"IngredientNameTextBox_{ingredientIndex}", Width = 100, Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            InputPanel.RegisterName(nameTextBox.Name, nameTextBox);

            // Ingredient Quantity
            Label quantityLabel = new Label { Content = $"Quantity:", Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            TextBox quantityTextBox = new TextBox { Name = $"IngredientQuantityTextBox_{ingredientIndex}", Width = 50, Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            InputPanel.RegisterName(quantityTextBox.Name, quantityTextBox);

            //Ingredient Unit
            Label unitLabel = new Label { Content = $"Unit:", Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            ComboBox unitComboBox = new ComboBox
            {
                Name = $"IngredientUnitComboBox_{ingredientIndex}",
                Width = 130,
                ItemsSource = new List<string> { "Teaspoons (tsp)", "Tablespoons (tbsp)", "Cups (C)", "Grams (g)", "Kilograms (kg)" },
                SelectedIndex = 0,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            InputPanel.RegisterName(unitComboBox.Name, unitComboBox);

            //Ingredient Food Group
            Label foodGroupLabel = new Label { Content = $"Food Group:", Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            ComboBox foodGroupComboBox = new ComboBox
            {
                Name = $"FoodGroupComboBox_{ingredientIndex}",
                Width = 200,
                ItemsSource = new List<string> { "Starchy foods", "Vegetables and fruits", "Dry beans, peas, lentils and soya", "Chicken, fish, meat and eggs", "Milk and dairy", "Fats and oil", "Water" },
                SelectedIndex = 0,
                HorizontalAlignment = HorizontalAlignment.Center
            };
            InputPanel.RegisterName(foodGroupComboBox.Name, foodGroupComboBox);

            //Ingredient Calories
            Label caloriesLabel = new Label { Content = "Calories", Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            TextBox caloriesTextBox = new TextBox { Name = $"CaloriesTextBox_{ingredientIndex}", Width = 100, Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            InputPanel.RegisterName(caloriesTextBox.Name, caloriesTextBox);

            // Add to Panel
            ingredientPanel.Children.Add(nameLabel);
            ingredientPanel.Children.Add(nameTextBox);
            ingredientPanel.Children.Add(quantityLabel);
            ingredientPanel.Children.Add(quantityTextBox);
            ingredientPanel.Children.Add(unitLabel);
            ingredientPanel.Children.Add(unitComboBox);
            ingredientPanel.Children.Add(foodGroupLabel);
            ingredientPanel.Children.Add(foodGroupComboBox);
            ingredientPanel.Children.Add(caloriesLabel);
            ingredientPanel.Children.Add(caloriesTextBox);

            InputPanel.Children.Add(ingredientPanel);
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
        private void addStepCountInput()
        {
            StackPanel stepCountPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 10, 0, 0), HorizontalAlignment = HorizontalAlignment.Center };

            Label stepCountLabel = new Label { Content = "Number of Steps:", Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            TextBox stepCountTextBox = new TextBox { Name = "StepCountTextBox", Width = 50, Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            InputPanel.RegisterName(stepCountTextBox.Name, stepCountTextBox);

            Button confirmStepCountButton = new Button { Content = "Confirm Steps", Width = 100, Margin = new Thickness(10), HorizontalAlignment = HorizontalAlignment.Center };
            confirmStepCountButton.Click += ConfirmStepCountButton_Click;

            stepCountPanel.Children.Add(stepCountLabel);
            stepCountPanel.Children.Add(stepCountTextBox);
            stepCountPanel.Children.Add(confirmStepCountButton);

            InputPanel.Children.Add(stepCountPanel);

        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        private void addStepInput()
        {
            StackPanel stepPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(0, 10, 0, 0), HorizontalAlignment = HorizontalAlignment.Center };

            Label stepLabel = new Label { Content = $"Step {stepIndex} Description:", Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            TextBox stepTextBox = new TextBox { Name = $"StepDescriptionTextBox_{stepIndex}", Width = 300, Margin = new Thickness(0, 0, 10, 0), HorizontalAlignment = HorizontalAlignment.Center };
            InputPanel.RegisterName(stepTextBox.Name, stepTextBox);

            stepPanel.Children.Add(stepLabel);
            stepPanel.Children.Add(stepTextBox);

            InputPanel.Children.Add(stepPanel);
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
