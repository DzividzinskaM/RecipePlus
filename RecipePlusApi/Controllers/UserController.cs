using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipePlusApi.Data.Repositories;
using RecipePlusApi.Models;
using RecipePlusApi.ViewModels.UserModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipePlusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;
        

        public UserController(RecipeContext context)
        {
            _userRepository = new UserRepository(context);
        }

        // GET: api/<UserController>
        [HttpGet("/api/users")]
        public IEnumerable<UserModel> Get()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>()));

            var users = mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(_userRepository.GetAll());

            return users;

        }

        // GET api/<UserController>/5
        [HttpGet("/api/users/{id}")]
        public UserModel Get(int id)
        {
            User user = _userRepository.Get(id);

            UserModel model = new UserModel()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password.ToString(),
            };

            return model;
        }


        // GET api/<UserController>/5
        [HttpPost("/api/users/isAuth")]
        public bool IsAuth(LoginModel model)
        {
            return _userRepository.isAuth(model);
        }

        // POST api/<UserController>
        [HttpPost("/api/users")]
        public void Post(RegisterModel model)
        {
            _userRepository.Create(model);
        }

      
    }
}
