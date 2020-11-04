using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipePlus.Models;
using RecipePlus.ViewModels.UserRecipe;

namespace RecipePlus.Data
{
    public class UserRecipeHandler
    {
        private RecipeContext db;

        public UserRecipeHandler(RecipeContext context)
        {
            db = context;
        }

        public List<UserModel> GetAll()
        {
            var users = db.Users
                .Include(u => u.Recipes)
                    .ThenInclude(r => r.Processes)
                .Include(u => u.Recipes)
                    .ThenInclude(r => r.RecipeIngredients)
                        .ThenInclude(ri => ri.Ingredient);


            List<UserModel> userRecipes = new List<UserModel>();

            foreach(var user in users)
            {
                userRecipes.Add(new UserModel()
                {
                    UserId = user.UserId,
                    UserRecipes = getRecipes(user),
                });
            }

            return userRecipes;
        }

        public List<UserRecipeModel> getRecipes(User user)
        { 
            List<UserRecipeModel> userRecipes = new List<UserRecipeModel>(); 
            foreach(var recipe in user.Recipes)
            {
                userRecipes.Add(new UserRecipeModel()
                {
                    RecipeId = recipe.RecipeId,
                    RecipeName = recipe.RecipeName,
                    Ingredients = getIngredients(recipe),
                    Processes = getProcesses(recipe)
                });
            }
            return userRecipes;
        }

        private List<UserProcessModel> getProcesses(Recipe recipe)
        {
            List<UserProcessModel> processes = new List<UserProcessModel>();

            foreach (var process in recipe.Processes)
            {
                processes.Add(new UserProcessModel
                {
                    ProcessId = process.ProcessId,
                    Description = process.Description
                });
            }

            return processes;

        }




        private List<UserIngredientModel> getIngredients(Recipe recipe)
        {
            List<UserIngredientModel> ingredients = new List<UserIngredientModel>();
            foreach (var ingredient in recipe.RecipeIngredients)
            {
                ingredients.Add(new UserIngredientModel()
                {
                    IngredientId = ingredient.IngredientId,
                    IngredientName = ingredient.Ingredient.IngredientName,
                    Numbers = ingredient.Numbers,
                    Calories = ingredient.Ingredient.Calories.Value
                });
            }
            return ingredients;
        }
    }
}
