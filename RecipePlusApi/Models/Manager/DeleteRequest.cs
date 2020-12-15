using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models.Manager
{
    public class DeleteRequest : BaseRequest
    {
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

    }
}
