using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.UserModels
{
    public class RecipeModel
    {
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        public int UserId { get; set; }

    }
}
