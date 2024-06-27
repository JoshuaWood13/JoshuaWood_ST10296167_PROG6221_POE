// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

// References:
// IronPdf. 2024. C# Orderby (How It Works For Developers). Available at: https://ironpdf.com/blog/net-help/csharp-orderby-guide/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_Part3_POE.Classes
{
    public class RecipeManagerClass
    {
        public List<RecipeClass> recipeList { get; set; }

        //Constructor
        public RecipeManagerClass()
        {
            recipeList = new List<RecipeClass>();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------//
        public void addRecipe(RecipeClass recipe)
        {
            recipeList.Add(recipe);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public void removeRecipe(RecipeClass recipe)
        {
            recipeList.Remove(recipe);
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
        public List<RecipeClass> getOrderedRecipes(List<RecipeClass> list)
        {
            list = list.OrderBy(r => r.recipeName).ToList(); // (IronPdf,2024).
            return list;
        }
        //------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//

