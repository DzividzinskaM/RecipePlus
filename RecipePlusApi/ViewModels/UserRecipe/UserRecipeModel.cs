using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.ViewModels.UserRecipe
{
    public class UserRecipeModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public List<UserIngredientModel> Ingredients { get; set; }

        public List<UserProcessModel> Processes { get; set; }

    }
}
