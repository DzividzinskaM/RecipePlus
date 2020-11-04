using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }        

        public ICollection<Recipe> Recipes { get; set; }

        public User()
        {
            Recipes = new List<Recipe>();
        }

    }
}
