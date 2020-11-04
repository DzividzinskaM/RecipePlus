using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlus.Models
{
    public class Process 
    {
        public int ProcessId { get; set; }
        public string Description { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
