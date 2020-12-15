using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Interfaces
{
    public class RecipeRequestRepository : IRecipeRequest
    {
        RecipeContext _context;

        public RecipeRequestRepository(RecipeContext context)
        {
            _context = context;
        }
        public void AddRecipe(RecipeRequest recipe)
        {
            _context.RecipesRequest.Add(recipe);

            _context.SaveChanges();
        }

        public int GetLastNumber()
        {
            return _context.RecipesRequest.Last().Id;
        }
    }
}
