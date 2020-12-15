using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models
{
    public class RecipeIngredient
    {
        public float Numbers { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public Recipe Recipe { get; set; }
        public Ingredient Ingredient {get;set;}
       
    }
}
