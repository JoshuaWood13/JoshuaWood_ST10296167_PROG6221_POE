using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp_Part3_POE.Classes;

namespace WpfApp_Part3_POE.ViewModels
{
    public class CreateRecipeViewModel : INotifyPropertyChanged
    {
        private string recipeName;
        private int? numberOfIngredients;
        private int? numberOfSteps;
        private int currentIngredientIndex;
        private int currentStepIndex;
        private bool isRecipeNameEnabled;
        private bool isNumberOfIngredientsEnabled;
        private bool isConfirmButtonEnabled;
        private RecipeManagerClass recipeManager;
        private RecipeClass currentRecipe;
        private Visibility inputPanelVisibility;
        private ObservableCollection<UIElement> ingredientInputs;
        private ObservableCollection<UIElement> stepInputs;
        public CreateRecipeViewModel(RecipeManagerClass recipeManager)
        {
            this.recipeManager = recipeManager;
            currentRecipe = new RecipeClass();
            ConfirmRecipeDetailsCommand = new RelayCommand(ConfirmRecipeDetails);
            AddIngredientCommand = new RelayCommand(AddIngredient);
            ConfirmStepsCommand = new RelayCommand(ConfirmSteps);
            AddStepCommand = new RelayCommand(AddStep);
            SubmitRecipeCommand = new RelayCommand(SubmitRecipe);
            InputPanelVisibility = Visibility.Collapsed;
            ingredientInputs = new ObservableCollection<UIElement>();
            stepInputs = new ObservableCollection<UIElement>();
            numberOfIngredients = null;
            numberOfSteps = null;
            currentIngredientIndex = 0;
            currentStepIndex = 0;
            isRecipeNameEnabled = true;
            isNumberOfIngredientsEnabled = true;
            isConfirmButtonEnabled = true;
        }

        public string RecipeName
        {
            get => recipeName;
            set
            {
                recipeName = value;
                OnPropertyChanged(nameof(RecipeName));
            }
        }

        public int? NumberOfIngredients
        {
            get => numberOfIngredients;
            set
            {
                numberOfIngredients = value;
                OnPropertyChanged(nameof(NumberOfIngredients));
            }
        }

        public int? NumberOfSteps
        {
            get => numberOfSteps;
            set
            {
                numberOfSteps = value;
                OnPropertyChanged(nameof(NumberOfSteps));
            }
        }

        public bool IsRecipeNameEnabled
        {
            get => isRecipeNameEnabled;
            set
            {
                isRecipeNameEnabled = value;
                OnPropertyChanged(nameof(IsRecipeNameEnabled));
            }
        }

        public bool IsNumberOfIngredientsEnabled
        {
            get => isNumberOfIngredientsEnabled;
            set
            {
                isNumberOfIngredientsEnabled = value;
                OnPropertyChanged(nameof(IsNumberOfIngredientsEnabled));
            }
        }

        public bool IsConfirmButtonEnabled
        {
            get => isConfirmButtonEnabled;
            set
            {
                isConfirmButtonEnabled = value;
                OnPropertyChanged(nameof(IsConfirmButtonEnabled));
            }
        }

        public Visibility InputPanelVisibility
        {
            get => inputPanelVisibility;
            set
            {
                inputPanelVisibility = value;
                OnPropertyChanged(nameof(InputPanelVisibility));
            }
        }

        public ObservableCollection<UIElement> IngredientInputs
        {
            get => ingredientInputs;
            set
            {
                ingredientInputs = value;
                OnPropertyChanged(nameof(IngredientInputs));
            }
        }

        public ObservableCollection<UIElement> StepInputs
        {
            get => stepInputs;
            set
            {
                stepInputs = value;
                OnPropertyChanged(nameof(StepInputs));
            }
        }

        public ICommand ConfirmRecipeDetailsCommand { get; }
        public ICommand AddIngredientCommand { get; }
        public ICommand ConfirmStepsCommand { get; }
        public ICommand AddStepCommand { get; }

        private void ConfirmRecipeDetails(object parameter)
        {
            if (!currentRecipe.validString(RecipeName))
            {
                MessageBox.Show("Please enter a valid recipe name.");
                return;
            }

            if (!NumberOfIngredients.HasValue || NumberOfIngredients.Value <=0)
            {
                MessageBox.Show("Please enter a valid number of ingredients.");
                return;
            }

            IsRecipeNameEnabled = false;
            IsNumberOfIngredientsEnabled = false;
            IsConfirmButtonEnabled = false;

            InputPanelVisibility = Visibility.Visible;

            // Clear previous input fields
            IngredientInputs.Clear();

            currentIngredientIndex = 0; // Reset the current ingredient index

            AddIngredientInput();

        }

        private void AddIngredientInput()
        {
            if (currentIngredientIndex < NumberOfIngredients)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };

                stackPanel.Children.Add(new Label { Content = $"Ingredient {currentIngredientIndex + 1} Name:", HorizontalAlignment = HorizontalAlignment.Center });
                stackPanel.Children.Add(new TextBox { Name = $"IngredientName_{currentIngredientIndex + 1}", Width = 200, HorizontalAlignment = HorizontalAlignment.Center });

                stackPanel.Children.Add(new Label { Content = "Quantity:", HorizontalAlignment = HorizontalAlignment.Center });
                stackPanel.Children.Add(new TextBox { Name = $"IngredientQuantity_{currentIngredientIndex + 1}", Width = 100, HorizontalAlignment = HorizontalAlignment.Center });

                stackPanel.Children.Add(new Label { Content = "Unit:", HorizontalAlignment = HorizontalAlignment.Center });
                stackPanel.Children.Add(new ComboBox
                {
                    Name = $"IngredientUnit_{currentIngredientIndex + 1}",
                    Width = 150,
                    ItemsSource = new[] { "Teaspoons (tsp)", "Tablespoons (tbsp)", "Cups (C)", "Grams (g)", "Kilograms (kg)" },
                    SelectedIndex = 0,
                    HorizontalAlignment = HorizontalAlignment.Center
                });

                stackPanel.Children.Add(new Label { Content = "Food Group:", HorizontalAlignment = HorizontalAlignment.Center });
                stackPanel.Children.Add(new ComboBox
                {
                    Name = $"IngredientFoodGroup_{currentIngredientIndex + 1}",
                    Width = 200,
                    ItemsSource = new[] { "Starchy foods", "Vegetables and fruits", "Dry beans, peas, lentils and soya", "Chicken, fish, meat and eggs", "Milk and dairy", "Fats and oil", "Water" },
                    SelectedIndex = 0,
                    HorizontalAlignment = HorizontalAlignment.Center
                });

                stackPanel.Children.Add(new Label { Content = "Calories:", HorizontalAlignment = HorizontalAlignment.Center });
                stackPanel.Children.Add(new TextBox { Name = $"IngredientCalories_{currentIngredientIndex + 1}", Width = 100, HorizontalAlignment = HorizontalAlignment.Center });

                // Add the "Add Ingredient" button dynamically
                var addButton = new Button
                {
                    Content = "Add Ingredient",
                    Command = AddIngredientCommand,
                    Width = 150,
                    Margin = new Thickness(0, 20, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                stackPanel.Children.Add(addButton);

                IngredientInputs.Add(stackPanel);
            }
        }

        private void AddIngredient(object parameter)
        {
            if (currentIngredientIndex >= NumberOfIngredients)
            {
                MessageBox.Show("All ingredients have been added.");
                return;
            }

            // Get the current input values
            var currentPanel = IngredientInputs[0] as StackPanel; // Always access the first item since we clear after adding

            if (currentPanel != null)
            {

                string inputName = (currentPanel.Children[1] as TextBox)?.Text;
                string inputQuantity = (currentPanel.Children[3] as TextBox)?.Text;
                string inputCalories = (currentPanel.Children[9] as TextBox)?.Text;
                string calorieFoodGroupCheck = (currentPanel.Children[7] as ComboBox)?.SelectedItem.ToString();

                if (!currentRecipe.validString(inputName))
                {
                    MessageBox.Show("Please enter a valid ingredient name with no digits");
                    return;
                }
                if (!currentRecipe.validDouble(inputQuantity))
                {
                    MessageBox.Show("Please enter a valid ingredient quantity above 0");
                    return;
                }
                if (!currentRecipe.validCalorieDouble(inputCalories))
                {
                    MessageBox.Show("Please enter a valid calorie count");
                    return;
                }
                else if (inputCalories == "0" && calorieFoodGroupCheck != "Water")
                {
                    MessageBox.Show("Please enter a valid calorie count for selected food group \n(Only water can have 0 calories)");
                    return;
                }
                else if (inputCalories != "0" && calorieFoodGroupCheck == "Water")
                {
                    MessageBox.Show("Please enter a valid calorie count for selected food group \n(Water can only have 0 calories)");
                    return;
                }

                var ingredient = new IngredientsClass
                {
                    ingredientName = (currentPanel.Children[1] as TextBox)?.Text,
                    ingredientQuantity = double.Parse((currentPanel.Children[3] as TextBox)?.Text ?? "0"),
                    measurementUnitName = (currentPanel.Children[5] as ComboBox)?.SelectedItem.ToString(),
                    ingredientFoodGroup = (currentPanel.Children[7] as ComboBox)?.SelectedItem.ToString(),
                    ingredientCalories = double.Parse((currentPanel.Children[9] as TextBox)?.Text ?? "0")
                    
                };

                ingredient.assignUnit(ingredient, ingredient.measurementUnitName);
                ingredient.saveOriginal();

                currentRecipe.ingredientList.Add(ingredient);
                currentIngredientIndex++;

                if (currentIngredientIndex < NumberOfIngredients)
                {
                    IngredientInputs.Clear();
                    StepInputs.Clear(); 
                    AddIngredientInput();
                }
                else
                {
                    MessageBox.Show("All ingredients have been added.");

                    InputPanelVisibility = Visibility.Collapsed;

                    // Add the dynamic controls for entering steps
                    AddStepControls();
                }
            }
        }

        private void AddStepControls()
        {
            IngredientInputs.Clear(); // Clear previous ingredient inputs

            var label = new Label { Content = "Enter Number of Steps:", FontSize = 16, HorizontalAlignment = HorizontalAlignment.Center };
            var textBox = new TextBox
            {
                Width = 50,
                Margin = new Thickness(0, 10, 0, 0),
                HorizontalAlignment = HorizontalAlignment.Center
            };
            // Set binding programmatically
            Binding binding = new Binding("NumberOfSteps")
            {
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            textBox.SetBinding(TextBox.TextProperty, binding);

            var button = new Button { Content = "Confirm Steps", Command = ConfirmStepsCommand, Width = 150, Margin = new Thickness(0, 20, 0, 0), HorizontalAlignment = HorizontalAlignment.Center };

            IngredientInputs.Add(label);
            IngredientInputs.Add(textBox);
            IngredientInputs.Add(button);

            InputPanelVisibility = Visibility.Visible;
        }

        private void ConfirmSteps(object parameter)
        {
            if (!NumberOfSteps.HasValue || NumberOfSteps.Value <= 0)
            {
                MessageBox.Show("Please enter a valid number of steps.");
                return;
            }

            InputPanelVisibility = Visibility.Collapsed;
            currentStepIndex = 0;

            // Clear previous input fields
            IngredientInputs.Clear();

            AddStepInput();
        }

        private void AddStepInput()
        {
            if (currentStepIndex < NumberOfSteps)
            {
                var stackPanel = new StackPanel { Orientation = Orientation.Vertical, Margin = new Thickness(5), HorizontalAlignment = HorizontalAlignment.Center };

                stackPanel.Children.Add(new Label { Content = $"Step {currentStepIndex + 1}:", HorizontalAlignment = HorizontalAlignment.Center });
                stackPanel.Children.Add(new TextBox { Name = $"Step_{currentStepIndex + 1}", Width = 350, Height = 150, AcceptsReturn = true, TextWrapping = TextWrapping.Wrap, HorizontalAlignment = HorizontalAlignment.Center });

                // Add the "Add Step" button dynamically
                var addButton = new Button
                {
                    Content = "Add Step",
                    Command = AddStepCommand,
                    Width = 150,
                    Margin = new Thickness(0, 20, 0, 0),
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                stackPanel.Children.Add(addButton);

                IngredientInputs.Add(stackPanel); // Add to IngredientInputs
                InputPanelVisibility = Visibility.Visible; // Ensure the InputPanel is visible
            }
        }

        private void AddStep(object parameter)
        {
            if (currentStepIndex >= NumberOfSteps)
            {
                MessageBox.Show("All steps have been added.");
                return;
            }

            // Get the current input values
            var currentPanel = IngredientInputs[0] as StackPanel; // Always access the first item since we clear after adding

            if (currentPanel != null)
            {
                var stepTextBox = currentPanel.Children[1] as TextBox;
                var stepDescription = stepTextBox?.Text;

                if (string.IsNullOrWhiteSpace(stepDescription))
                {
                    MessageBox.Show("Please enter a valid step description that is not empty.");
                    stepTextBox.Text = string.Empty;
                    return;
                }

                currentRecipe.stepList.Add(stepDescription);
                currentStepIndex++;

                if (currentStepIndex < NumberOfSteps)
                {
                    IngredientInputs.Clear(); // Clear the IngredientInputs
                    AddStepInput();
                }
                else
                {
                    MessageBox.Show("All steps have been added.");
                    InputPanelVisibility = Visibility.Collapsed;

                    // Add the submit button
                    var submitButton = new Button
                    {
                        Content = "Create Recipe",
                        Command = SubmitRecipeCommand,
                        Width = 200,
                        Height = 50,
                        Margin = new Thickness(0, 30, 0, 0),
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    IngredientInputs.Clear(); // Clear step inputs
                    IngredientInputs.Add(submitButton); // Add submit button to ingredient inputs to ensure visibility
                    InputPanelVisibility = Visibility.Visible;
                }
            }
        }

        public ICommand SubmitRecipeCommand { get; }

        public event EventHandler RecipeSubmitted;

        private void SubmitRecipe(object parameter)
        {
            currentRecipe.recipeName = RecipeName;
            double calories = 0;
            foreach(IngredientsClass ingredient in currentRecipe.ingredientList)
            {
                calories += ingredient.ingredientCalories;
            }
            currentRecipe.recipeCalorieTotal = calories;
            recipeManager.addRecipe(currentRecipe);

            // Delegate for calorie alert
            RecipeClass.calorieAlert alert = RecipeClass.caloriesExceeded;

            // Get the calorie explanation
            string calorieExplanation = RecipeClass.DisplayCalorieTotal(calories, alert);

            MessageBox.Show($"Recipe has been added successfully!\n{calorieExplanation}");

            // Notify that the recipe has been submitted
            RecipeSubmitted?.Invoke(this, EventArgs.Empty);

            ResetCreateRecipeViewModel();
        }

        private void ResetCreateRecipeViewModel()
        {
            currentRecipe = new RecipeClass();
            RecipeName = string.Empty;
            NumberOfIngredients = null;
            NumberOfSteps = null;
            IsRecipeNameEnabled = true;
            IsNumberOfIngredientsEnabled = true;
            IsConfirmButtonEnabled = true;
            IngredientInputs.Clear();
            StepInputs.Clear();
            InputPanelVisibility = Visibility.Collapsed;
            currentIngredientIndex = 0;
            currentStepIndex = 0;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}