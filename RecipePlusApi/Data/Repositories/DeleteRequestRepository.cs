using Microsoft.EntityFrameworkCore;
using RecipePlusApi.Data.Interfaces;
using RecipePlusApi.Models;
using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Repositories
{
    public class DeleteRequestRepository : IDeleteRequest
    {

        public RecipeContext _context;
       

        public DeleteRequestRepository(RecipeContext context)
        {
            _context = context;
        }
        public void Accept(int id)
        {
            DeleteRequest request = _context.DeleteRequests.Find(id);
            if (request == null )
                return;

            Recipe recipe = _context.Recipes.Find(request.RecipeId);
            if (recipe == null)
                return;

            var recipeIngredients = _context.RecipeIngredients.Where(r => r.RecipeId == id).ToList();

            foreach(var ingredient in recipeIngredients)
            {
                _context.RecipeIngredients.Remove(ingredient);
            }

            var processes = _context.Processes.Where(p => p.RecipeId == id).ToList();


            foreach (var process in processes)
            {
                _context.Processes.Remove(process);
            }

            _context.SaveChanges();

            _context.Recipes.Remove(recipe);

            /*request.RecipeId = 0;
            request.Recipe = null;
            request.IsAccept = true;
            request.IsClose = true;

            _context.DeleteRequests.Update(request);*/

            _context.SaveChanges();
        }

        public void Create(DeleteRequest request)
        {
            _context.DeleteRequests.Add(request);

            _context.SaveChanges();
        }

        public DeleteRequest Get(int id)
        {
            return _context.DeleteRequests.Find(id);
        }

        public IEnumerable<DeleteRequest> GetAll()
        {
            return _context.DeleteRequests
                .Include(r => r.Recipe)
                .ThenInclude(r => r.RecipeIngredients)
                .Include(r => r.Recipe)
                .ThenInclude(r => r.Processes);
        }

        public void NotAccept(int id)
        {
            DeleteRequest request = _context.DeleteRequests.Find(id);
            if (request == null)
                return;

            request.IsAccept = false;
            request.IsClose = true;

            _context.DeleteRequests.Update(request);

            _context.SaveChanges();
        }
    }
}
