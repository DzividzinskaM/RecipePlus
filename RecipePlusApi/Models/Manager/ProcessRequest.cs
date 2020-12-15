using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models.Manager
{
    public class ProcessRequest
    {
        public int ProcessRequestId { get; set; }

        public string Description { get; set; }

        public int RecipeRequestId { get; set; }

        public RecipeRequest Recipe { get; set; }
    }
}
