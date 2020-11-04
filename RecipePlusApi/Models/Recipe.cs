using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.Models
{
    public class Recipe 
    {
       
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }

        //public ICollection<Ingredient> Ingredients { get; set; }
        public List<RecipeIngredient> RecipeIngredients { get; set; }

        public ICollection<Process> Processes { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
        
    }
}
