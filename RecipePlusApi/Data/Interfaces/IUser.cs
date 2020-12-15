using RecipePlusApi.Models;
using RecipePlusApi.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Interfaces
{
    public interface IUser
    {
        public IEnumerable<User> GetAll();

        public User Get(int id);

        public void Create(RegisterModel model);

        public bool isAuth(LoginModel model);

    }
}
