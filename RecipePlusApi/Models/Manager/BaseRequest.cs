using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models.Manager
{
    public class BaseRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public bool IsClose { get; set; }

        public bool IsAccept { get; set; }

       
    }
}
