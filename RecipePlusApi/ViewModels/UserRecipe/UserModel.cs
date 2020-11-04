using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.ViewModels.UserRecipe
{
    public class UserModel
    {
        public int UserId { get; set; }

        public List<UserRecipeModel> UserRecipes { get; set; }
    }
}
