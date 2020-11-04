using Microsoft.EntityFrameworkCore;
using RecipePlus.Models;
using RecipePlus.ViewModels.StandartRecipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.Data
{
    public class RecipeHandler
    {
        private RecipeContext db;

        public RecipeHandler(RecipeContext context)
        {
            db = context;
        }

        public List<ProgramRecipeModel> GetAll()
        {
            var recipes = db.Recipes.Include(r => r.RecipeIngredients).ThenInclude(ri => ri.Ingredient).Include(r => r.Processes)
                .Where(r=> r.UserId == null);

            List<ProgramRecipeModel> programRecipes = new List<ProgramRecipeModel>();

            foreach(var recipe in recipes)
            {
                
                programRecipes.Add(new ProgramRecipeModel()
                {
                    RecipeId = recipe.RecipeId,
                    RecipeName = recipe.RecipeName,
                    Ingredients = getRecipeIngredients(recipe), 
                    Processes = getProcesses(recipe)
                });
            }
            return programRecipes;
        }

        private List<ProgramProcessModel> getProcesses(Recipe recipe)
        {
            List<ProgramProcessModel> processes = new List<ProgramProcessModel>();

            foreach(var process in recipe.Processes)
            {
                processes.Add(new ProgramProcessModel
                {
                    ProcessId = process.ProcessId,
                    Description = process.Description
                });
            }

            return processes;

        }

        private List<ProgramIngredientModel> getRecipeIngredients(Recipe recipe)
        {
            List<ProgramIngredientModel> ingredients = new List<ProgramIngredientModel>();
            foreach(var ingredient in recipe.RecipeIngredients)
            {
                ingredients.Add(new ProgramIngredientModel()
                {
                    IngredientId = ingredient.IngredientId,
                    IngredientName = ingredient.Ingredient.IngredientName,
                    Numbers = ingredient.Numbers
                });
            }
            return ingredients;
        }
    }
}

