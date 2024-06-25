using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_Part3_POE.Classes;

namespace WpfApp_Part3_POE.ViewModels
{
    public class MainWindowViewModel
    {
        public CreateRecipeViewModel CreateRecipeViewModel { get; private set; }
        //public CreateRecipeViewModel CreateRecipeViewModel { get; }
        public DisplayRecipeViewModel DisplayRecipeViewModel { get; private set; }

        private RecipeManagerClass recipeManager;
        public MainWindowViewModel()
        {
            recipeManager = new RecipeManagerClass();

            // Add some sample recipes for testing
            var sampleRecipe1 = new RecipeClass { recipeName = "Pasta", recipeCalorieTotal = 250 };
            var sampleRecipe2 = new RecipeClass { recipeName = "Salad", recipeCalorieTotal = 10 };

            recipeManager.addRecipe(sampleRecipe1);
            recipeManager.addRecipe(sampleRecipe2);
            //var recipeManager = new RecipeManagerClass();
            CreateRecipeViewModel = new CreateRecipeViewModel(recipeManager);
            DisplayRecipeViewModel = new DisplayRecipeViewModel(recipeManager);

            CreateRecipeViewModel.RecipeSubmitted += CreateRecipeViewModel_RecipeSubmitted;
        }

        private void CreateRecipeViewModel_RecipeSubmitted(object sender, EventArgs e)
        {
            ResetCreateRecipeViewModel();
            DisplayRecipeViewModel.RefreshRecipeList();
        }

        public void ResetCreateRecipeViewModel()
        {
            CreateRecipeViewModel = new CreateRecipeViewModel(recipeManager);
            CreateRecipeViewModel.RecipeSubmitted += CreateRecipeViewModel_RecipeSubmitted;
        }

        //public void ResetDisplayRecipeViewModel()
        //{
        //    DisplayRecipeViewModel = new DisplayRecipeViewModel(recipeManager);
        //}
    }
}