using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.ViewModels.UserRecipe
{
    public class UserIngredientModel
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public float Calories { get; set; }
        public float Numbers { get; set; }
    }
}
