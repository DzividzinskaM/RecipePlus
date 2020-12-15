using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipePlusApi.Data.Repositories;
using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.ManagerModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipePlusApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminRepository _adminRepository;

        public AdminController(RecipeContext context)
        {
            _adminRepository = new AdminRepository(context);
        }

        // GET: api/<AdminComtroller>
        [HttpGet("/api/admins")]
        public IEnumerable<AdminModel> GetAll()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Admin, AdminModel>()));

            var admins = mapper.Map<IEnumerable<Admin>, IEnumerable<AdminModel>>(_adminRepository.GetAll());

            return admins;
        }

        // GET api/<AdminComtroller>/5
        [HttpGet("/api/admins/{id}")]
        public AdminModel Get(int id)
        {
            Admin admin = _adminRepository.Get(id);
            AdminModel model = new AdminModel()
            {
                Id = admin.Id,
                Login = admin.Login,
                AdminName = admin.AdminName,
                Password = admin.Password
            };

            return model;
        }

        // POST api/<AdminComtroller>
        [HttpPost("/api/admins")]
        public void Post(AdminModel model)
        {
            _adminRepository.Create(model);
        }


        // GET api/<AdminComtroller>/5
        [HttpPost("/api/admins/isAuth")]
        public bool isAuth(AdminModel model)
        {
            return _adminRepository.isAuth(model);
        }



    }
}
