using RecipePlusApi.Data.Interfaces;
using RecipePlusApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Repositories
{
    public class IngredientRepository : IIngredient
    {
        RecipeContext _context;

        public IngredientRepository(RecipeContext context)
        {
            _context = context;
        }
         
        public void Create(Ingredient ingredient)
        {
            _context.Ingredients.Add(ingredient);

            _context.SaveChanges();
        }

        public Ingredient Get(int id)
        {
            return _context.Ingredients.Find(id);
        }

        public Ingredient Get(string name)
        {
            return _context.Ingredients.Where(i => i.IngredientName == name).FirstOrDefault();
        }

        public IEnumerable<Ingredient> GetAll()
        {
            return _context.Ingredients;
        }

       /* public IEnumerable<RecipeIngredient> GetIngredientForRecipe(int id)
        {
            return _context.RecipeIngredients.Where(r => r.RecipeId == id).ToList();
        }*/
    }
}
