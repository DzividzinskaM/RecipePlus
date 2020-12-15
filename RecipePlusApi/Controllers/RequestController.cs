using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipePlusApi.Data;
using RecipePlusApi.Data.repositories;
using RecipePlusApi.Data.Repositories;
using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.ManagerModels;
using RecipePlusApi.ViewModels.UserModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipePlusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        // GET: api/<RequestController>

        /*private readonly RecipeContext _context;

        private readonly RequestHandler _handler;
*/
        private readonly AddRequestRepository _addRequestRepository;
        private readonly DeleteRequestRepository _deleteRequestRepository;
        private readonly UpdateRequestRepository _updateRequestRepository;
        private readonly IngredientRepository _ingredientRepository;
        private readonly Mapping _mapping;

        public RequestController(RecipeContext context)
        {
            /*_context = context;
            _handler = new RequestHandler(context);*/
            _addRequestRepository = new AddRequestRepository(context);
            _deleteRequestRepository = new DeleteRequestRepository(context);
            _updateRequestRepository = new UpdateRequestRepository(context);
            _ingredientRepository = new IngredientRepository(context);
            _mapping = new Mapping(context);

        }
        /*
                [HttpGet("/api/requests/types")]
                public IEnumerable<TypeModel> GetRequestType()
                {
                    var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<RequestType, TypeModel>()));

                    var types = mapper.Map<IEnumerable<RequestType>, IEnumerable<TypeModel>>(_requestTypeRepository.GetAll());

                    return types;
                }*/

        [HttpGet("/api/add/requests")]
        public IEnumerable<RequestModel> GetAllAddRequest()
        {

            var requests = _addRequestRepository.GetAll();


            List<RequestModel> resultRequest = new List<RequestModel>();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<IngredientRequest, IngredientModel>()
                .ForMember("IngredientId", opt => opt.MapFrom(c => c.IngredientRequestId))));

            var processMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ProcessRequest, ProcessModel>()));



            foreach (var request in requests)
            {
                var ingredients = mapper.Map<ICollection<IngredientRequest>, ICollection<IngredientModel>>(request.RecipeRequest.Ingredients);
                var processes = processMapper.Map<ICollection<ProcessRequest>, ICollection<ProcessModel>>(request.RecipeRequest.Processes);


                resultRequest.Add(new RequestModel
                {
                    RequestID = request.Id,
                    // TypeID = request.TypeId,
                    // TypeName = request.RequestType.TypeName,
                    RecipeName = request.RecipeRequest.RecipeName,
                    Calories = request.RecipeRequest.Calories,
                    Portion = request.RecipeRequest.Portion,
                    Ingredients = ingredients,
                    Processes = processes,
                    IsClose = request.IsClose,
                    IsAccept = request.IsAccept,
                    UserId = request.UserId
                });
            }

            return resultRequest;


        }


        [HttpGet("/api/delete/requests")]
        public IEnumerable<RequestModel> GetAllDeleteRequests()
        {
            List<RequestModel> result = new List<RequestModel>();


            var processMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Process, ProcessModel>()));

            foreach (var request in _deleteRequestRepository.GetAll())
            {
                var processes = processMapper.Map<ICollection<Process>, ICollection<ProcessModel>>(request.Recipe.Processes);

                List<IngredientModel> ingredients = new List<IngredientModel>();

                foreach (var recipeIngr in request.Recipe.RecipeIngredients)
                {
                    var ingr = _ingredientRepository.Get(recipeIngr.IngredientId);
                    ingredients.Add(new IngredientModel()
                    {
                        IngredientId = recipeIngr.IngredientId,
                        IngredientName = ingr.IngredientName,
                        Number = recipeIngr.Numbers
                    });
                }


                result.Add(new RequestModel
                {
                    RequestID = request.Id,
                    RecipeId = request.Recipe.RecipeId,
                    RecipeName = request.Recipe.RecipeName,
                    Calories = request.Recipe.Calories,
                    Portion = request.Recipe.Portion,
                    Ingredients = ingredients,
                    Processes = processes,
                    IsClose = request.IsClose,
                    IsAccept = request.IsAccept,
                    UserId = request.UserId
                });
            }

            return result;
        }


        // GET api/<RequestController>/5
        [HttpGet("/api/request/{id}")]
        public RequestModel Get(int id)
        {
            var request = _addRequestRepository.Get(id);
            if (request == null)
                return null;


            RequestModel resultRequest;

            var ingedientMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<IngredientRequest, IngredientModel>()
                .ForMember("IngredientId", opt => opt.MapFrom(c => c.IngredientRequestId))
            ));

            var processMapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<ProcessRequest, ProcessModel>()));

            var ingredients = ingedientMapper.Map<ICollection<IngredientRequest>, ICollection<IngredientModel>>(request.RecipeRequest.Ingredients);
            var processes = processMapper.Map<ICollection<ProcessRequest>, ICollection<ProcessModel>>(request.RecipeRequest.Processes);


            resultRequest = new RequestModel()
            {
                RequestID = request.Id,
                //TypeID = request.TypeId,
                //TypeName = request.RequestType.TypeName,
                RecipeName = request.RecipeRequest.RecipeName,
                Ingredients = ingredients,
                Processes = processes,
                IsClose = request.IsClose,
                IsAccept = request.IsAccept,
                Calories = request.RecipeRequest.Calories,
                Portion = request.RecipeRequest.Portion
            };



            return resultRequest;

        }

        [HttpGet("/api/update/requests")]
        public IEnumerable<UpdateRequestModel> GetAllUpdateRequests()
        {
            var requests = _updateRequestRepository.GetAll();

            List<UpdateRequestModel> requestModels = new List<UpdateRequestModel>();

            foreach(var request in requests)
            {
                RecipeDetailModel newRecipe = _mapping.MapRecipeRequestToRecipeDetailModel(request.RecipeRequest);
                RecipeDetailModel previousRecipe = _mapping.MapRecipeToRecipeDetailModel(request.Recipe);
                requestModels.Add(new UpdateRequestModel()
                {
                    Id = request.Id,
                    PreviousRecipe = previousRecipe,
                    NewRecipe = newRecipe,
                    IsClose = request.IsClose,
                    IsAccept = request.IsAccept,
                    UserId = request.UserId
                    
                });
               
            }

            return requestModels;
        }

        // PUT api/<RequestController>/5
        [HttpPut("/api/add/requests/{id}/{accept}")]
        public void AcceptAddRequest(int id, bool accept)
        {

            if (accept)
                _addRequestRepository.AcceptRequest(id);
            else
                _addRequestRepository.NotAcceptRequest(id);
        }

        // PUT api/<RequestController>/5
        [HttpPut("/api/delete/requests/{id}/{accept}")]
        public void AcceptDeleteRequest(int id, bool accept)
        {
            if (accept)
                _deleteRequestRepository.Accept(id);
            else
                _deleteRequestRepository.NotAccept(id);
           
        }

        [HttpPut("/api/update/requests/{id}/{accept}")]
        public void AcceptUpdateRequest(int id, bool accept)
        {
            if (accept)
                _updateRequestRepository.Accept(id);
            else
                _updateRequestRepository.NotAccept(id);

        }


        // DELETE api/<RequestController>/5
        [HttpDelete("/api/request/{id}")]
        public void Delete(int id)
        {
            _addRequestRepository.DeleteRequest(id);
        }
    }
}
