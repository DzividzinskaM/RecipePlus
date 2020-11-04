using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipePlus.ViewModels.StandartRecipe;

namespace RecipePlus.ViewModels.StandartRecipe
{
    public class ProgramRecipeModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public List<ProgramIngredientModel> Ingredients { get; set; }

        public List<ProgramProcessModel> Processes { get; set; }
    }
}
