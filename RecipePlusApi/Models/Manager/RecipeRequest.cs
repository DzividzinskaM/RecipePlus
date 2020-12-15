using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models.Manager
{
    public class RecipeRequest
    {
        public int Id { get; set; }
        public string RecipeName { get; set; }

        public int Portion { get; set; }

        public float Calories { get; set; }

        public ICollection<IngredientRequest> Ingredients { get; set; }

        public ICollection<ProcessRequest> Processes { get; set; }
        
        public int? PreviousRecipeId { get; set; }

        //public UpdateRequest UpdateRequest { get; set; }

        //public int RequestId { get; set; }

        //public Request Request { get; set; }
    }
}
