using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.UserModels
{
    public class RecipeAddingModel
    {
        public string RecipeName { get; set; }

        public int Portion { get; set; }

        public float Calories { get; set; }
        public ICollection<IngredientAddingModel> Ingredients { get; set; }

        public ICollection<ProcessAddingModel> Processes { get; set; }

        public int UserId { get; set; }
    }
}
