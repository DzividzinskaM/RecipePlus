using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models.Manager
{
    public class UpdateRequest : BaseRequest
    {
        public int PreviousRecipeId { get; set; }

        public Recipe Recipe { get; set; }

        public RecipeRequest RecipeRequest { get; set; }
    }
}
