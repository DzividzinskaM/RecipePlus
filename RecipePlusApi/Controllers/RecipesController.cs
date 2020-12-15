using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipePlusApi.Data;
using RecipePlusApi.Data.Interfaces;
using RecipePlusApi.Data.repositories;
using RecipePlusApi.Data.Repositories;
using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.UserModels;

namespace RecipePlusApi.Controllers
{
    
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeContext _context;
        private readonly RecipeRepository _recipeRepository;
        private readonly IngredientRepository _ingredientRepository;
        private readonly AddRequestRepository _requestRepository;
        private readonly RecipeRequestRepository _recipeRequestRepository;
        private readonly DeleteRequestRepository _deleteRequestRepository;
        private readonly UpdateRequestRepository _updateRequestRepository;
        private readonly Mapping _mapping;



        public RecipesController(RecipeContext context)
        {
            _context = context;
            _recipeRepository = new RecipeRepository(context);
            _ingredientRepository = new IngredientRepository(context);
            _requestRepository = new AddRequestRepository(context);
            _recipeRequestRepository = new RecipeRequestRepository(context);
            _deleteRequestRepository = new DeleteRequestRepository(context);
            _updateRequestRepository = new UpdateRequestRepository(context);
            _mapping = new Mapping(context);
        }


        [HttpGet("/api/recipes")]
        public IEnumerable<RecipeModel> Get()
        {
           
            //var ingredientMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Ingredient, IngredientModel>()));


             var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Recipe, RecipeModel>()));

             var recipes = mapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeModel>>(_recipeRepository.GetAll());

             return recipes;
        }

        [HttpGet("/api/recipes/{id}")]
        public RecipeDetailModel GetDetail(int id)
        {
            Recipe recipe = _recipeRepository.Get(id);

            if (recipe == null)
                return null;

            RecipeDetailModel result = new RecipeDetailModel()
            {
                RecipeId = recipe.RecipeId,
                RecipeName = recipe.RecipeName,
                Calories = recipe.Calories,
                Portion = recipe.Portion,
            };

            if (recipe.User != null)
                result.UserId = recipe.User.UserId;




            List<IngredientModel> ingredients = new List<IngredientModel>();
            

            foreach(var recipeIngr in recipe.RecipeIngredients)
            {
                var ingr = _ingredientRepository.Get(recipeIngr.IngredientId);
                ingredients.Add(new IngredientModel()
                {
                    IngredientId = ingr.IngredientId, 
                    IngredientName = ingr.IngredientName,
                    Number = recipeIngr.Numbers
                });
            }


            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Process, ProcessModel>()));

            result.Processes = mapper.Map<ICollection<Process>, ICollection<ProcessModel>>(recipe.Processes);

            result.Ingredients = ingredients;

            return result;

        }


        [HttpPost("api/recipes")]
        public void PostRecipe(RecipeAddingModel model, int UserId)
        {

            AddRequest request;


            List<IngredientRequest> ingredients = new List<IngredientRequest>();
            List<ProcessRequest> processes = new List<ProcessRequest>();

            foreach(var ingr in model.Ingredients)
            {
                ingredients.Add(new IngredientRequest
                {
                    IngredientName = ingr.IngredientName,
                    Number = ingr.Number
                });
            }

            foreach(var process in model.Processes)
            {
                processes.Add(new ProcessRequest()
                {
                    Description = process.Description
                });
            }


            RecipeRequest recipeRequest = new RecipeRequest()
            {
                RecipeName = model.RecipeName,
                Ingredients = ingredients,
                Processes = processes,
                Calories = model.Calories, 
                Portion = model.Portion
            };

           // _recipeRequestRepository.AddRecipe(recipeRequest);

            request = new AddRequest()
            {
             
               // RequestRecipeId = _recipeRequestRepository.GetLastNumber(),
                UserId = model.UserId,
                IsClose = false,
                RecipeRequest = recipeRequest
                
            };

            _requestRepository.Create(request);

            if (_context.Recipes.Any(r => r.RecipeName == request.RecipeRequest.RecipeName))
                _requestRepository.NotAcceptRequest(request.Id);
        }


        [HttpPut("/api/recipes/{id}")]
        public void UpdateRecipe(int id, RecipeDetailModel newRecipe)
        {
           

            Recipe recipe = _recipeRepository.Get(id);

            RecipeRequest newRecipeRequest = _mapping.MapRecipeDetailModelToRecipe(newRecipe);

            UpdateRequest request = new UpdateRequest
            {
                PreviousRecipeId = id,
                Recipe = recipe,
                RecipeRequest = newRecipeRequest,
            };


            _updateRequestRepository.Create(request);
        }


        [HttpDelete("/api/{userId}/recipe/{recipeId}")]
        public void DeleteRecipe(int userId, int recipeId)
        {
            DeleteRequest request = new DeleteRequest()
            {
                RecipeId = recipeId, 
                UserId = userId
            };

            _deleteRequestRepository.Create(request);
            
        }

    }
}
