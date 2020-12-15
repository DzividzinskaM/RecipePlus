using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.ManagerModels
{
    public class AdminModel
    {
        public int Id { get; set; }

        public string AdminName { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

    }
}
