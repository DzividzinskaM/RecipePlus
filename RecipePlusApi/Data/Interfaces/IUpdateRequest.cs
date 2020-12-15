using RecipePlusApi.Models.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlusApi.Data.Interfaces
{
    public interface IUpdateRequest
    {
        public IEnumerable<UpdateRequest> GetAll();

        public UpdateRequest Get(int id);

        public void Create(UpdateRequest request);

        public void Accept(int id);

        public void NotAccept(int id);

    }
}
