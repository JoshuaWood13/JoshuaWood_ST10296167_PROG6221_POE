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
        private RecipeClass recipe = new RecipeClass();

        public void run()
        {
            recipe.buildRecipe();
        }
    }
}
