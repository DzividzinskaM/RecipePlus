using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Interfaces
{
    public interface IRecipeRequest
    {
        public void AddRecipe(RecipeRequest recipe);

        public int GetLastNumber();
    }
}
