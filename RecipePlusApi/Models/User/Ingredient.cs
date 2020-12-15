using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public float? Calories { get; set; }

        //public ICollection<Recipe> Recipes { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }

    }
}
