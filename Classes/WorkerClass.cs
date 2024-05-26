// Name: Joshua Wood
// Student number: ST10296167
// Group: 2

// References: 

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoshuaWood_ST10296167_PROG6221_POE.Classes
{
    public class WorkerClass
    {
        private RecipeClass recipee = new RecipeClass();
        private RecipeManagerClass recipeManager = new RecipeManagerClass();

//------------------------------------------------------------------------------------------------------------------------------------------//
        //This method calls and runs the buildRecipe method that allows for a user to create and make to changes to a recipe
        public void run()
        {
            recipee.buildRecipe(recipeManager);
        }
//------------------------------------------------------------------------------------------------------------------------------------------//
    }
}
//--------------------------------------------------------X END OF FILE X-------------------------------------------------------------------//