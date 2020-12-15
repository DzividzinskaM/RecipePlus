using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models.Manager
{
    public class IngredientRequest
    {
        public int IngredientRequestId { get; set; }

        public string IngredientName { get; set; }

        public float Number { get; set; }

        public int RecipeRequestId { get; set; }

        public RecipeRequest Recipe { get; set; }
    }
}
