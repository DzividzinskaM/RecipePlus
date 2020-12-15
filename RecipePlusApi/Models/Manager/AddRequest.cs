using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Models.Manager
{
    public class AddRequest : BaseRequest
    {     
        public RecipeRequest RecipeRequest { get; set; }
    }
}
