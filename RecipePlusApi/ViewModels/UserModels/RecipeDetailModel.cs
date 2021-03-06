﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.UserModels
{
    public class RecipeDetailModel : RecipeModel
    {
       /* public int RecipeId { get; set; }

        public string RecipeName { get; set; }*/

        public int Portion { get; set; }

        public float Calories { get; set; }
        public ICollection<IngredientModel> Ingredients { get; set; }

        public ICollection<ProcessModel> Processes{ get; set; }

      //  public int? UserId { get; set; }
    }
}
