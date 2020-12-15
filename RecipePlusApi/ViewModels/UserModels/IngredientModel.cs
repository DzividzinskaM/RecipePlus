using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.UserModels
{
    public class IngredientModel
    {
        public int IngredientId { get; set; }

        public string IngredientName { get; set; }

        public float Number { get; set; }
    }
}
