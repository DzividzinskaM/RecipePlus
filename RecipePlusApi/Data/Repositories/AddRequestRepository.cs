using Microsoft.EntityFrameworkCore;
using RecipePlusApi.Data.interfaces;
using RecipePlusApi.Data.Repositories;
using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.ManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.repositories
{
    public class AddRequestRepository : IAddRequest
    {
        RecipeContext _context;
        RecipeRepository _recipeRepository;
        IngredientRepository _ingredientRepository;

        public AddRequestRepository(RecipeContext context)
        {
            _context = context;
            _recipeRepository = new RecipeRepository(context);
            _ingredientRepository = new IngredientRepository(context);
        }


        public void AcceptRequest(int id)
        {
            AddRequest request = Get(id);
            request.IsAccept = true;
            request.IsClose = true;

            List<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>();
            List<Process> processes = new List<Process>();


            foreach (var ingredient in request.RecipeRequest.Ingredients)
            {
                Ingredient currIngr = new Ingredient()
                {
                    IngredientName = ingredient.IngredientName
                };
                if (!_context.Ingredients.Any(i => i.IngredientName == ingredient.IngredientName))
                {

                    _ingredientRepository.Create(currIngr);
                }

                recipeIngredients.Add(new RecipeIngredient()
                {
                    Numbers = ingredient.Number,
                    IngredientId = _ingredientRepository.Get(ingredient.IngredientName).IngredientId,

                });
            }

            foreach (var process in request.RecipeRequest.Processes)
            {
                processes.Add(new Process()
                {
                    Description = process.Description
                });
            }

            Recipe recipe;

            recipe = new Recipe()
            {
                RecipeName = request.RecipeRequest.RecipeName,
                Calories = request.RecipeRequest.Calories,
                Portion = request.RecipeRequest.Portion,
                UserId = request.UserId,
                RecipeIngredients = recipeIngredients,
                Processes = processes

            };


            _recipeRepository.Create(recipe);


            _context.Requests.Update(request);

            _context.SaveChanges();

        }

        public void Create(AddRequest request)
        {
            _context.Requests.Add(request);

            _context.SaveChanges();
        }

        public void DeleteRequest(int id)
        {
            AddRequest request = _context.Requests.Find(id);
            if (request != null)
                _context.Requests.Remove(request);
        }

        public AddRequest Get(int id)
        {
            return _context.Requests
                .Include(r => r.RecipeRequest)
                    .ThenInclude(rr => rr.Ingredients)
                .Include(r => r.RecipeRequest)
                    .ThenInclude(rr => rr.Processes)
                .Where(r => r.Id == id).FirstOrDefault();
        }

        public IEnumerable<AddRequest> GetAll()
        {
            return _context.Requests
                .Include(r => r.RecipeRequest)
                    .ThenInclude(rr => rr.Ingredients)
                .Include(r => r.RecipeRequest)
                    .ThenInclude(rr => rr.Processes);
        }

        /*  public IEnumerable<Request> GetAllNew()
          {
              return _context.Requests.Where(r => r.IsClose == false);
          }*/

        public IEnumerable<AddRequest> GetRequestByType(int requestTypeId)
        {
            return _context.Requests.Where(r => r.Id == requestTypeId);
        }

        public void NotAcceptRequest(int id)
        {
            AddRequest request = _context.Requests.Find(id);
            request.IsAccept = false;
            request.IsClose = true;

            _context.Requests.Update(request);
            _context.SaveChanges();
        }
    }
}
