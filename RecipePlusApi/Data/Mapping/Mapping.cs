using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data
{
    public class Mapping
    {
        RecipeContext _context;

        public Mapping(RecipeContext context)
        {
            _context = context;
        }
        public List<IngredientModel> MapIngredientRequestToIngredientModelList(IEnumerable<IngredientRequest> ingredientRequests)
        {
            List<IngredientModel> ingredients = new List<IngredientModel>();

            foreach(var ingredient in ingredientRequests)
            {
                ingredients.Add(new IngredientModel()
                {
                    IngredientId = ingredient.IngredientRequestId,
                    IngredientName = ingredient.IngredientName,
                    Number = ingredient.Number
                });
            }

            return ingredients;
        }

        public List<ProcessModel> MapProcessRequestToProcessModelList(IEnumerable<ProcessRequest> processRequests)
        {
            List<ProcessModel> processes = new List<ProcessModel>();

            foreach(var process in processRequests)
            {
                processes.Add(new ProcessModel() { 
                    ProcessId = process.ProcessRequestId,
                    Description = process.Description
                });

            }
            return processes;
        }

        internal RecipeDetailModel MapRecipeRequestToRecipeDetailModel(RecipeRequest recipeRequest)
        {
            RecipeDetailModel recipe = new RecipeDetailModel() {
                Calories = recipeRequest.Calories,
                Portion = recipeRequest.Portion,
                Ingredients = MapIngredientRequestToIngredientModelList(recipeRequest.Ingredients),
                Processes = MapProcessRequestToProcessModelList(recipeRequest.Processes)
            };
            return recipe;
        }

        internal RecipeDetailModel MapRecipeToRecipeDetailModel(Recipe recipe)
        {
            RecipeDetailModel newRecipe = new RecipeDetailModel()
            {
                RecipeId = recipe.RecipeId,
                RecipeName = recipe.RecipeName,
                Calories = recipe.Calories,
                Portion = recipe.Portion,
                Ingredients = MapRecipeIngredientToIngredientModelList(recipe.RecipeIngredients),
                Processes = MapProcessToProcessModelList(recipe.Processes),
                
            };
            return newRecipe;
        }

        private ICollection<ProcessModel> MapProcessToProcessModelList(ICollection<Process> processes)
        {
            List<ProcessModel> result = new List<ProcessModel>();

            foreach(var proces in processes)
            {
                result.Add(new ProcessModel { 
                    ProcessId = proces.ProcessId,
                    Description = proces.Description 
                });

            }
            return result;
        }

        private ICollection<IngredientModel> MapRecipeIngredientToIngredientModelList(List<RecipeIngredient> recipeIngredients)
        {
            List<IngredientModel> result = new List<IngredientModel>();

            foreach(var recipeIngr in recipeIngredients)
            {
                result.Add(new IngredientModel()
                {
                    IngredientId = recipeIngr.IngredientId,
                    IngredientName = _context.Ingredients.Where(i => i.IngredientId == recipeIngr.IngredientId).FirstOrDefault().IngredientName,
                    Number = recipeIngr.Numbers

                });
            }

            return result;
        }

        internal RecipeRequest MapRecipeDetailModelToRecipe(RecipeDetailModel newRecipe)
        {
            RecipeRequest result = new RecipeRequest
            {

                RecipeName = newRecipe.RecipeName,
                Calories = newRecipe.Calories,
                Portion = newRecipe.Portion,
                Ingredients = MapIngredientModelToIngredientRequestList(newRecipe.Ingredients),
                Processes = mapProcessModelToProcessRequestList(newRecipe.Processes)
            };
            return result;
        }

        private ICollection<ProcessRequest> mapProcessModelToProcessRequestList(ICollection<ProcessModel> processes)
        {
            List<ProcessRequest> result = new List<ProcessRequest>();

            foreach(var process in processes)
            {
                result.Add(new ProcessRequest { Description = process.Description });
            }
            return result;
        }

        private ICollection<IngredientRequest> MapIngredientModelToIngredientRequestList(ICollection<IngredientModel> ingredients)
        {
            List<IngredientRequest> result = new List<IngredientRequest>();

            foreach(var ingr in ingredients)
            {
                result.Add(new IngredientRequest() { 
                    IngredientName = ingr.IngredientName,
                    Number = ingr.Number
                });

            }

            return result;
        }
    }
}
