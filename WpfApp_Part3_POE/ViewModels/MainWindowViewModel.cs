// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

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
        // Declaring variables
        public CreateRecipeViewModel CreateRecipeViewModel { get; private set; }
        public DisplayRecipeViewModel DisplayRecipeViewModel { get; private set; }
        public ScaleRecipeViewModel ScaleRecipeViewModel { get; private set; }
        public ResetRecipeViewModel ResetRecipeViewModel { get; private set; }
        public DeleteRecipeViewModel DeleteRecipeViewModel { get; private set; }

        private RecipeManagerClass recipeManager;

        //------------------------------------------------------------------------------------------------------------------------------------------//
        // Constructor
        public MainWindowViewModel()
        {
            recipeManager = new RecipeManagerClass();

            CreateRecipeViewModel = new CreateRecipeViewModel(recipeManager);
            DisplayRecipeViewModel = new DisplayRecipeViewModel(recipeManager);
            ScaleRecipeViewModel = new ScaleRecipeViewModel(recipeManager);
            ResetRecipeViewModel = new ResetRecipeViewModel(recipeManager);
            DeleteRecipeViewModel = new DeleteRecipeViewModel(recipeManager);

            CreateRecipeViewModel.RecipeSubmitted += CreateRecipeViewModel_RecipeSubmitted;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method refreshees the recipe list in all views to ensure it is updated with the new created recipe
        private void CreateRecipeViewModel_RecipeSubmitted(object sender, EventArgs e)
        {
            ResetCreateRecipeViewModel();
            DisplayRecipeViewModel.RefreshRecipeList();
            ScaleRecipeViewModel.RefreshRecipeList();
            ResetRecipeViewModel.RefreshRecipeList();
            DeleteRecipeViewModel.RefreshRecipeList();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method resets the create recipe view in order to add a new recipe after creation
        public void ResetCreateRecipeViewModel()
        {
            CreateRecipeViewModel = new CreateRecipeViewModel(recipeManager);
            CreateRecipeViewModel.RecipeSubmitted += CreateRecipeViewModel_RecipeSubmitted;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        // This method clears the displayed text of a recipe
        public void ClearDisplayedRecipe()
        {
            DisplayRecipeViewModel.ClearDisplayedRecipe();
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//