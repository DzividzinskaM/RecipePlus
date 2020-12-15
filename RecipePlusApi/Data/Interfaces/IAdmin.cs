using RecipePlusApi.Models.Manager;
using RecipePlusApi.ViewModels.ManagerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Interfaces
{
    public interface IAdmin
    {
        public void Create(AdminModel admin);

        public IEnumerable<Admin> GetAll();
        public Admin Get(int id);

        public bool isAuth(AdminModel admin);
    }
}
