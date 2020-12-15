using Microsoft.EntityFrameworkCore;
using RecipePlusApi.Data.Interfaces;
using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Repositories
{
    public class UpdateRequestRepository : IUpdateRequest
    {
        RecipeContext _context;

        public UpdateRequestRepository(RecipeContext context)
        {
            _context = context;
        }
        public void Accept(int id)
        {
            UpdateRequest request = Get(id);
            if (request == null)
                return;

            Recipe recipe = _context.Recipes.Find(request.PreviousRecipeId);

            

            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();

            foreach(var ingr in request.RecipeRequest.Ingredients)
            {
                Ingredient currIngr = new Ingredient()
                {
                    IngredientName = ingr.IngredientName
                };

                if(!_context.Ingredients.Any(i => i.IngredientName == ingr.IngredientName))
                {
                    _context.Ingredients.Add(currIngr);
                    _context.SaveChanges();
                }
                recipeIngredients.Add(new RecipeIngredient()
                {
                    Numbers = ingr.Number,
                    Ingredient = currIngr,
                    RecipeId = request.PreviousRecipeId
                });

            }



            List<Process> processes = new List<Process>();

            foreach(var process in request.RecipeRequest.Processes)
            {
                processes.Add(new Process()
                {
                    //RecipeId = request.PreviousRecipeId,
                    Description = process.Description
                });
            }

            



            recipe.Portion = request.RecipeRequest.Portion;
            recipe.Calories = request.RecipeRequest.Calories;
            recipe.RecipeIngredients = recipeIngredients;
            recipe.Processes = processes;


            _context.Recipes.Update(recipe);

            request.IsAccept = true;
            request.IsClose = true;

            _context.UpdateRequests.Update(request);

            _context.SaveChanges();

        }

        public void Create(UpdateRequest request)
        {
            _context.UpdateRequests.Add(request);

            _context.SaveChanges();
        }

        public UpdateRequest Get(int id)
        {
            return _context.UpdateRequests
                .Include(r => r.Recipe)
                    .ThenInclude(r => r.RecipeIngredients)
                .Include(r => r.Recipe)
                    .ThenInclude(r => r.Processes)
                .Include(r => r.RecipeRequest)
                    .ThenInclude(rr => rr.Ingredients)
                .Include(r => r.RecipeRequest)
                    .ThenInclude(r => r.Processes)
                .Where(r => r.Id == id).FirstOrDefault();

        }

        public IEnumerable<UpdateRequest> GetAll()
        {
            return _context.UpdateRequests
                .Include(r => r.Recipe)
                    .ThenInclude(r => r.RecipeIngredients)
                .Include(r => r.Recipe)
                    .ThenInclude(r => r.Processes)
                .Include(r => r.RecipeRequest)
                    .ThenInclude(rr => rr.Ingredients)
                .Include(r => r.RecipeRequest)
                    .ThenInclude(r => r.Processes);
        }

        public void NotAccept(int id)
        {
            UpdateRequest request = _context.UpdateRequests.Find(id);

            request.IsAccept = false;
            request.IsClose = true;

            _context.UpdateRequests.Update(request);

            _context.SaveChanges();
           
        }
    }
}
