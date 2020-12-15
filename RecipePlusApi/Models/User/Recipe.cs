using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models
{
    public class Recipe 
    {
       
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        public float Calories { get; set; }

        public int Portion { get; set; }

        //public ICollection<Ingredient> Ingredients { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }

        public ICollection<Process> Processes { get; set; }

        public AddRequest AddRequest { get; set; }

        public DeleteRequest DeleteRequest { get; set; }

        public ICollection<UpdateRequest> UpdateRequests { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        
    }
}
