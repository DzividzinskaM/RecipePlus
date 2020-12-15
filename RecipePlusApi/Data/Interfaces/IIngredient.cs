using RecipePlusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Interfaces
{
    public interface IIngredient
    {
     //   public IEnumerable<RecipeIngredient> GetIngredientForRecipe(int id);

        public IEnumerable<Ingredient> GetAll();

        public Ingredient Get(int id);
        public Ingredient Get(string name);


        public void Create(Ingredient ingredient);

    }
}
