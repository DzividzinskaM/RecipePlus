using RecipePlusApi.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.ManagerModels
{
    public class UpdateRequestModel : BaseRequestModel
    {
        public RecipeDetailModel PreviousRecipe { get; set; }

        public RecipeDetailModel NewRecipe { get; set; }
    }
}
