using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipePlus.Data;
using RecipePlus.Models;
using RecipePlus.ViewModels.UserRecipe;

namespace RecipePlus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRecipeController : ControllerBase
    {
        private readonly RecipeContext _context;
        private readonly UserRecipeHandler _handler;

        public UserRecipeController(RecipeContext context)
        {
            _context = context;
            _handler = new UserRecipeHandler(_context);
        }

        // GET: api/UserRecipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUserRecipes()
        {
            var userRecipes = _handler.GetAll();
            return userRecipes;
        }

        // GET: api/UserRecipe/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserRecipe(int id)
        {
            var user = _handler.GetAll().Where(u => u.UserId == id).FirstOrDefault();
            
            return user;
        }

        [HttpGet]
        [Route("/api/UserRecipe/{UserId}/recipe/{RecipeId}")]
        public async Task<ActionResult<UserRecipeModel>> GetUserRecipe(int UserId, int RecipeId)
        {
            var user = _handler.GetAll().Where(u => u.UserId == UserId).FirstOrDefault();

            var recipe = user.UserRecipes.Where(r => r.RecipeId == RecipeId).FirstOrDefault();

            return recipe;
        }

        // PUT: api/UserRecipe/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*   [HttpPut("{id}")]
           public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
           {
               if (id != recipe.RecipeId)
               {
                   return BadRequest();
               }

               _context.Entry(recipe).State = EntityState.Modified;

               try
               {
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!RecipeExists(id))
                   {
                       return NotFound();
                   }
                   else
                   {
                       throw;
                   }
               }

               return NoContent();
           }
   */
        // POST: api/UserRecipe
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /*        [HttpPost]
                public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
                {
                    _context.Recipes.Add(recipe);
                    await _context.SaveChangesAsync();

                    return CreatedAtAction("GetRecipe", new { id = recipe.RecipeId }, recipe);
                }
        */
        // DELETE: api/UserRecipe/5
        /*        [HttpDelete("{id}")]
                public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
                {
                    var recipe = await _context.Recipes.FindAsync(id);
                    if (recipe == null)
                    {
                        return NotFound();
                    }

                    _context.Recipes.Remove(recipe);
                    await _context.SaveChangesAsync();

                    return recipe;
                }

                private bool RecipeExists(int id)
                {
                    return _context.Recipes.Any(e => e.RecipeId == id);
                }*/
    }
}
