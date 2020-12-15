using Microsoft.EntityFrameworkCore;
using RecipePlusApi.Data.interfaces;
using RecipePlusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.repositories
{
    public class RecipeRepository : IRecipe
    {
        RecipeContext _context;

        public RecipeRepository(RecipeContext context)
        {
            _context = context;
        }

        public void Create(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            _context.SaveChanges();

        }

        public void DeleteRecipe(int id)
        {
            Recipe recipe = _context.Recipes.Find(id);
            if (recipe != null)
                _context.Recipes.Remove(recipe);
            _context.SaveChanges();

        }

        public Recipe Get(int id)
        {
            var recipes = _context.Recipes
                    .Include(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient)
                    .Include(r => r.Processes)
                    .Include(r => r.User);


            return recipes.Where(r => r.RecipeId == id).FirstOrDefault();
            
        }

        public IEnumerable<Recipe> GetAll()
        {
            return _context.Recipes;
        }

        public int GetRecipeId(string name)
        {
           return _context.Recipes.Where(r => r.RecipeName == name).FirstOrDefault().RecipeId;
        }

        public IEnumerable<Recipe> GetUserRecipe()
        {
            throw new NotImplementedException();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            _context.SaveChanges();
        }
    }
}
