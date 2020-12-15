using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.ManagerModels
{
    public class BaseRequestModel
    {
        public int Id { get; set; }

        public bool IsClose { get; set; }

        public bool IsAccept { get; set; }

        public int UserId { get; set; }
    }
}
