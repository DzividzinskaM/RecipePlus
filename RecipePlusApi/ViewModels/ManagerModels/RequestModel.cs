using RecipePlusApi.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.ViewModels.ManagerModels
{
    public class RequestModel
    {
        public int RequestID { get; set; }

       /* public int TypeID { get; set; }

        public string TypeName { get; set; }*/

        public int RecipeId { get; set; }

        public string RecipeName { get; set; }

        public float Calories { get; set; }

        public int Portion { get; set; }

        public ICollection<IngredientModel> Ingredients { get; set; }

        public ICollection<ProcessModel> Processes { get; set; }

        public bool IsClose { get; set; }

        public bool IsAccept { get; set; }

        public int UserId { get; set; }
    }
}
