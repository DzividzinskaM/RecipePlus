using RecipePlusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.interfaces
{
    public interface IRecipe
    {
        public IEnumerable<Recipe> GetAll();

        public Recipe Get(int id);

        public int GetRecipeId(string name);

        public IEnumerable<Recipe> GetUserRecipe();

        public void Create(Recipe recipe);

        public void UpdateRecipe(Recipe recipe);

        public void DeleteRecipe(int id);
    }
}
